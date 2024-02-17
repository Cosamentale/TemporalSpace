using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
using System.IO;

public class TextureComparator : MonoBehaviour
{
    public NNModel modelFile;
    private Model model;
    private IWorker worker;
    private List<float[]> textureFeatures;
    //public Texture2D inputTexture;
    public float result;
    public RenderTexture B;
    void OnEnable()
    {
        model = ModelLoader.Load(modelFile);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.CSharpBurst, model);
        textureFeatures = new List<float[]>();
        LoadTexturesFromStreamingAssets();
        FindClosestMatch();
    }

    void OnDisable()
    {
        worker.Dispose();
    }

    private void LoadTexturesFromStreamingAssets()
    {
        string streamingAssetsPath = Application.streamingAssetsPath;
        string captureFolderPath = Path.Combine(streamingAssetsPath, "Capture");
        if (Directory.Exists(captureFolderPath))
        {
            string[] imageFiles = Directory.GetFiles(captureFolderPath, "*.png");
            foreach (string imageFile in imageFiles)
            {
                Texture2D texture = LoadTextureFromFile(imageFile);
                if (texture != null)
                {
                    float[] features = ExtractFeatures(texture);
                    textureFeatures.Add(features);
                }
            }
        }
        else
        {
            Debug.LogError("Capture folder not found in StreamingAssets folder: " + captureFolderPath);
        }
    }

    private Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        if (texture.LoadImage(fileData))
        {
            return texture;
        }
        return null;
    }

    private float[] ExtractFeatures(Texture2D texture)
    {
        Tensor inputTensor = new Tensor(texture, channels: 3);
        worker.Execute(inputTensor);
        Tensor outputTensor = worker.PeekOutput();
        float[] features = outputTensor.ToReadOnlyArray();
        inputTensor.Dispose();
        outputTensor.Dispose();
        return features;
    }

    private void FindClosestMatch()
    {
        Texture2D inputTexture = RenderTextureToTexture2D(B);
        float[] inputFeatures = ExtractFeatures(inputTexture);
        float maxSimilarity = -1f;
        int closestTextureIndex = 0;

        for (int i = 0; i < textureFeatures.Count-1; i++)
        {
            float similarity = CosineSimilarity(inputFeatures, textureFeatures[i]);
            if (similarity > maxSimilarity)
            {
                maxSimilarity = similarity;
                closestTextureIndex = i;
            }
        }

        Debug.Log("Closest texture match: " + closestTextureIndex);
        Debug.Log("Similarity score: " + maxSimilarity);
        result = closestTextureIndex;
        enabled = false;
    }

    private float CosineSimilarity(float[] embedding1, float[] embedding2)
    {
        float dotProduct = 0f;
        float norm1 = 0f;
        float norm2 = 0f;
        for (int i = 0; i < embedding1.Length; i++)
        {
            dotProduct += embedding1[i] * embedding2[i];
            norm1 += Mathf.Pow(embedding1[i], 2);
            norm2 += Mathf.Pow(embedding2[i], 2);
        }
        norm1 = Mathf.Sqrt(norm1);
        norm2 = Mathf.Sqrt(norm2);
        float similarity = dotProduct / (norm1 * norm2);
        return similarity;
    }
    private Texture2D RenderTextureToTexture2D(RenderTexture renderTexture)
    {
        RenderTexture.active = renderTexture;
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height);
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
        RenderTexture.active = null;
        return texture;
    }
}
