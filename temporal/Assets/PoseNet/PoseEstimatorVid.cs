using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Unity.Barracuda;
using RenderHeads.Media.AVProLiveCamera;
public class PoseEstimatorVid : MonoBehaviour
{
    public RenderTexture Tex;
    public enum ModelType
    {
        MobileNet
    }
    public enum EstimationType
    {
        MultiPose,
        SinglePose
    }
    public Material material;
    //public AVProLiveCamera script2;
    [Tooltip("The ComputeShader that will perform the model-specific preprocessing")]
    public ComputeShader posenetShader;
    [Tooltip("Use GPU for preprocessing")]
    public bool useGPU = true;
    [Tooltip("The dimensions of the image being fed to the model")]
    public Vector2Int imageDims = new Vector2Int(256, 256);
    [Tooltip("The MobileNet model asset file to use when performing inference")]
    public NNModel mobileNetModelAsset;
    [Tooltip("The backend to use when performing inference")]
    public WorkerFactory.Type workerType = WorkerFactory.Type.Auto;
    [Tooltip("The type of pose estimation to be performed")]
    public EstimationType estimationType = EstimationType.SinglePose;
    [Tooltip("The maximum number of posees to estimate")]
    [Range(1, 20)]
    public int maxPoses = 20;
    [Tooltip("The score threshold for multipose estimation")]
    [Range(0, 1.0f)]
    public float scoreThreshold = 0.25f;
    [Tooltip("Non-maximum suppression part distance")]
    public int nmsRadius = 100;
    [Tooltip("The minimum confidence level required to display the key point")]
    [Range(0, 100)]
    public int minConfidence = 70;
    // The texture used to create input tensor
    private RenderTexture rTex;
    // The preprocessing function for the current model type
    private System.Action<float[]> preProcessFunction;
    // Stores the input data for the model
    private Tensor input;

    private struct Engine
    {
        public WorkerFactory.Type workerType;
        public IWorker worker;
        public ModelType modelType;
        public Engine(WorkerFactory.Type workerType, Model model, ModelType modelType)
        {
            this.workerType = workerType;
            worker = WorkerFactory.CreateWorker(workerType, model);
            this.modelType = modelType;
        }
    }
    // The interface used to execute the neural network
    private Engine engine;
    // The name for the heatmap layer in the model asset
    private string heatmapLayer;
    // The name for the offsets layer in the model asset
    private string offsetsLayer;
    // The name for the forwards displacement layer in the model asset
    private string displacementFWDLayer;
    // The name for the backwards displacement layer in the model asset
    private string displacementBWDLayer;
    // The name for the Sigmoid layer that returns the heatmap predictions
    private string predictionLayer = "heatmap_predictions";
    // Stores the current estimated 2D keypoint locations in videoTexture
    private Utils.Keypoint[][] poses;
    // Array of pose skeletons
    private PoseSkeleton[] skeletons;
    // public Vector3[] posePositions;
    //public Vector3[] posePositions2;
    private Vector3 smoothDampVelocity;
    public int closestSkeletonIndex = -1;

    private Vector2 p0;
    private Vector2 p1;
    private Vector2 p2;
    private Vector2 p3;
    private Vector2 p4;
    private Vector2 p5;
    private Vector2 p6;
    private Vector2 p8;
    private Vector2 p9;
    private Vector2 p10;
    private Vector2 p11;

    public Vector4 pos0;
    public Vector4 pos1;
    public Vector4 pos2;
    public Vector4 pos3;
    public Vector4 pos4;
    public Vector4 pos5;
    public Vector4 pos6;
    public Vector4 pos7;
    public Vector4 pos8;
    public Vector4 pos9;
    public Vector4 pos10;
    public Vector4 pos11;

    public float pr;
    public float pp;
    public Vector3 previousHipPosition;
    private Vector2 a = new Vector2(0.5f, 0.5f);
    //public Vector3[] pos;
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="mirrorScreen"></param>

    private void InitializeBarracuda()
    {
        Model m_RunTimeModel;
     
            preProcessFunction = Utils.PreprocessMobileNet;
            m_RunTimeModel = ModelLoader.Load(mobileNetModelAsset);
            displacementFWDLayer = m_RunTimeModel.outputs[2];
            displacementBWDLayer = m_RunTimeModel.outputs[3];
        heatmapLayer = m_RunTimeModel.outputs[0];
        offsetsLayer = m_RunTimeModel.outputs[1];
        ModelBuilder modelBuilder = new ModelBuilder(m_RunTimeModel);
        modelBuilder.Sigmoid(predictionLayer, heatmapLayer);
        workerType = WorkerFactory.ValidateType(workerType);
        engine = new Engine(workerType, modelBuilder.model, ModelType.MobileNet);
    }
    private void InitializeSkeletons()
    {
        skeletons = new PoseSkeleton[maxPoses];
        for (int i = 0; i < maxPoses; i++) skeletons[i] = new PoseSkeleton();
    }
    void Start()
    {     
        rTex = new RenderTexture(imageDims.x, imageDims.y, 24, RenderTextureFormat.ARGBHalf);
        InitializeBarracuda();
        InitializeSkeletons();
        material.SetFloat("_resx", imageDims.x);
        material.SetFloat("_resy", imageDims.y);     
    }
    /// <param name="image"></param>
    /// <param name="functionName"></param>
    /// <returns></returns>
    private void ProcessImageGPU(RenderTexture image, string functionName)
    {
        int numthreads = 8;
        int kernelHandle = posenetShader.FindKernel(functionName);
        // Define a temporary HDR RenderTexture
        RenderTexture result = RenderTexture.GetTemporary(image.width, image.height, 24, RenderTextureFormat.ARGBHalf);
        result.enableRandomWrite = true;
        result.Create();
        posenetShader.SetTexture(kernelHandle, "Result", result);
        posenetShader.SetTexture(kernelHandle, "InputImage", image);
        posenetShader.Dispatch(kernelHandle, result.width / numthreads, result.height / numthreads, 1);
        Graphics.Blit(result, image);
        RenderTexture.ReleaseTemporary(result);
    }

    /// <param name="image"></param>
    private void ProcessImage(RenderTexture image)
    {
        if (useGPU)
        {
            // Apply preprocessing steps
            ProcessImageGPU(image, preProcessFunction.Method.Name);
            // Create a Tensor of shape [1, image.height, image.width, 3]
            input = new Tensor(image, channels: 3);
        }
        else
        {
            // Create a Tensor of shape [1, image.height, image.width, 3]
            input = new Tensor(image, channels: 3);
            // Download the tensor data to an array
            float[] tensor_array = input.data.Download(input.shape);
            // Apply preprocessing steps
            preProcessFunction(tensor_array);
            // Update input tensor with new color data
            input = new Tensor(input.shape.batch,
                               input.shape.height,
                               input.shape.width,
                               input.shape.channels,
                               tensor_array);
        }
    }

    /// <param name="engine"></param>
    private void ProcessOutput(IWorker engine)
    {
        // Get the model output
        Tensor heatmaps = engine.PeekOutput(predictionLayer);
        Tensor offsets = engine.PeekOutput(offsetsLayer);
        Tensor displacementFWD = engine.PeekOutput(displacementFWDLayer);
        Tensor displacementBWD = engine.PeekOutput(displacementBWDLayer);
        // Calculate the stride used to scale down the inputImage
        int stride = (imageDims.y - 1) / (heatmaps.shape.height - 1);
        stride -= (stride % 8);
        if (estimationType == EstimationType.SinglePose)
        {
            // Initialize the array of Keypoint arrays
            poses = new Utils.Keypoint[1][];
            // Determine the key point locations
            poses[0] = Utils.DecodeSinglePose(heatmaps, offsets, stride);
        }
        else
        {
            // Determine the key point locations
            poses = Utils.DecodeMultiplePoses(
                heatmaps, offsets,
                displacementFWD, displacementBWD,
                stride: stride, maxPoseDetections: maxPoses,
                scoreThreshold: scoreThreshold,
                nmsRadius: nmsRadius);
        }
        // Release the resources allocated for the output Tensors
        heatmaps.Dispose();
        offsets.Dispose();
        displacementFWD.Dispose();
        displacementBWD.Dispose();
    }
    void Update()
    {
        Graphics.Blit(Tex, rTex);
        ProcessImage(rTex);
        engine.worker.Execute(input);
        input.Dispose();
        ProcessOutput(engine.worker);

        Vector4[] posePositionsArray = new Vector4[maxPoses * 17];
        Vector4[] posePositionsArray2 = new Vector4[maxPoses * 17];
        //int closestSkeletonIndex = -1;
        float closestDistance = float.MaxValue;
        float closestDistance2 = float.MaxValue;
        for (int i = 0; i < skeletons.Length; i++)
        {
            if (i <= poses.Length - 1)
            {
                skeletons[i].UpdateKeyPointPositions(poses[i], minConfidence, imageDims);
                Vector3[] keyPoints = skeletons[i].GetKeyPoints();
                material.SetFloat("_pos1", skeletons[0].GetKeyPoints()[0].x / imageDims.x);
                material.SetFloat("_pos2a", skeletons[1].GetKeyPoints()[0].x / imageDims.x);
                material.SetFloat("_pos3", skeletons[2].GetKeyPoints()[0].x / imageDims.x);
                float hipX = keyPoints[0].x;

                float distanceToHip = Vector3.Distance((new Vector3(keyPoints[0].x, keyPoints[0].y, keyPoints[0].z) + new Vector3(keyPoints[5].x, keyPoints[5].y, keyPoints[5].z)
                    + new Vector3(keyPoints[12].x, keyPoints[12].y, keyPoints[12].z)) / 3, previousHipPosition);

                if (distanceToHip < closestDistance)
                {
                    closestDistance = distanceToHip;
                    closestSkeletonIndex = i;

                    // Set the first set of pose positions
                    for (int j = 0; j < 17; j++)
                    {
                        int index = j;
                        posePositionsArray[index] = new Vector4(keyPoints[j].x, keyPoints[j].y, keyPoints[j].z, 1);
                        // posePositions[index] = keyPoints[j];
                    }
                }
               
            }
        }
        previousHipPosition = SmoothValue((new Vector3(skeletons[closestSkeletonIndex].GetKeyPoints()[0].x, skeletons[closestSkeletonIndex].GetKeyPoints()[0].y, skeletons[closestSkeletonIndex].GetKeyPoints()[0].z) +
            new Vector3(skeletons[closestSkeletonIndex].GetKeyPoints()[5].x, skeletons[closestSkeletonIndex].GetKeyPoints()[5].y, skeletons[closestSkeletonIndex].GetKeyPoints()[5].z) +
            new Vector3(skeletons[closestSkeletonIndex].GetKeyPoints()[12].x, skeletons[closestSkeletonIndex].GetKeyPoints()[12].y, skeletons[closestSkeletonIndex].GetKeyPoints()[12].z)) / 3);
        material.SetVectorArray("_pos", posePositionsArray);
        material.SetVectorArray("_pos2", posePositionsArray2);
        //"nose", "leftShoulder", "rightShoulder", "leftElbow", "rightElbow", "leftWrist", "rightWrist", "leftHip", "rightHip", "leftKnee", "rightKnee", "leftAnkle", "rightAnkle"
        pos7 = new Vector4((posePositionsArray[11].x + posePositionsArray[12].x) / imageDims.x * 0.5f, (posePositionsArray[11].y + posePositionsArray[12].y) / imageDims.y * 0.5f, (posePositionsArray[11].z + posePositionsArray[12].z) * 0.05f, 0);
        Vector2 p72 = new Vector2(pos7.x, pos7.y);
        pr = 5 * Vector2.Distance(p72, new Vector2((posePositionsArray[5].x + posePositionsArray[6].x) / imageDims.x, (posePositionsArray[5].y + posePositionsArray[6].y) / imageDims.y) * 0.5f);
        p0 = ((new Vector2(posePositionsArray[0].x / imageDims.x, posePositionsArray[0].y / imageDims.y) - p72) / pr + a);
        p1 = ((new Vector2(posePositionsArray[5].x / imageDims.x, posePositionsArray[5].y / imageDims.y) - p72) / pr + a);
        p2 = ((new Vector2(posePositionsArray[6].x / imageDims.x, posePositionsArray[6].y / imageDims.y) - p72) / pr + a);
        p3 = ((new Vector2(posePositionsArray[7].x / imageDims.x, posePositionsArray[7].y / imageDims.y) - p72) / pr + a);
        p4 = ((new Vector2(posePositionsArray[8].x / imageDims.x, posePositionsArray[8].y / imageDims.y) - p72) / pr + a);
        p5 = ((new Vector2(posePositionsArray[9].x / imageDims.x, posePositionsArray[9].y / imageDims.y) - p72) / pr + a);
        p6 = ((new Vector2(posePositionsArray[10].x / imageDims.x, posePositionsArray[10].y / imageDims.y) - p72) / pr + a);
        p8 = ((new Vector2(posePositionsArray[13].x / imageDims.x, posePositionsArray[13].y / imageDims.y) - p72) / pr + a);
        p9 = ((new Vector2(posePositionsArray[14].x / imageDims.x, posePositionsArray[14].y / imageDims.y) - p72) / pr + a);
        p10 = ((new Vector2(posePositionsArray[15].x / imageDims.x, posePositionsArray[15].y / imageDims.y) - p72) / pr + a);
        p11 = ((new Vector2(posePositionsArray[16].x / imageDims.x, posePositionsArray[16].y / imageDims.y) - p72) / pr + a);
        pos0 = new Vector4(p0.x, p0.y, posePositionsArray[0].z, 0);
        pos1 = new Vector4(p1.x, p1.y, posePositionsArray[5].z, 0);
        pos2 = new Vector4(p2.x, p2.y, posePositionsArray[6].z, 0);
        pos3 = new Vector4(p3.x, p3.y, posePositionsArray[7].z, 0);
        pos4 = new Vector4(p4.x, p4.y, posePositionsArray[8].z, 0);
        pos5 = new Vector4(p5.x, p5.y, posePositionsArray[9].z, 0);
        pos6 = new Vector4(p6.x, p6.y, posePositionsArray[10].z, 0);
        pos8 = new Vector4(p8.x, p8.y, posePositionsArray[13].z, 0);
        pos9 = new Vector4(p9.x, p9.y, posePositionsArray[14].z, 0);
        pos10 = new Vector4(p10.x, p10.y, posePositionsArray[15].z, 0);
        pos11 = new Vector4(p11.x, p11.y, posePositionsArray[16].z, 0);
        pp = (posePositionsArray[15].y / imageDims.y + posePositionsArray[16].y / imageDims.y) / 2;

        material.SetFloat("_pr", pr);
        material.SetFloat("_pp", pp);
    }
    Vector3 SmoothValue(Vector3 inputValue)
    {
        float smoothingTime = 2.5f;
        return Vector3.SmoothDamp(previousHipPosition, inputValue, ref smoothDampVelocity, smoothingTime);
    }
    private void OnDisable()
    {
        engine.worker.Dispose();
    }
}
