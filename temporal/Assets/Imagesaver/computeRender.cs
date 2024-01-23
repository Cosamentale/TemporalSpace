using UnityEngine;
using System.Collections;
using TMPro;
using System;
//using RenderHeads.Media.AVProLiveCamera;

public class computeRender : MonoBehaviour
{
    public ComputeShader compute_shader;
    public Material preview;
    public Material final;
    RenderTexture A;
    RenderTexture B;
    int handle_main;
    public PoseEstimator script;
    public int resx = 128;
    public int resy = 128;
    /*public Vector4 pos0;
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
    public float pp;
    public float pr;  */
    private Vector2 a = new Vector2(0.5f, 0.5f);
    public SaveTextureToFile capture;
    public float ti;
    void Start()
    {
        // A = new RenderTexture(resx, resy, 0, RenderTextureFormat.ARGBFloat);
        A = new RenderTexture(resx, resy, 0);
        A.enableRandomWrite = true;
        A.filterMode = FilterMode.Point;
        A.Create();        
        B = new RenderTexture(resx, resy, 0);
        B.enableRandomWrite = true;
        B.filterMode = FilterMode.Point;
        B.Create();
        handle_main = compute_shader.FindKernel("CSMain");
        compute_shader.SetFloat("_resx", resx);
        compute_shader.SetFloat("_resy", resy);
        final.SetFloat("_resx2", resx);
        final.SetFloat("_resy2", resy);
    }

    void Update()
    {
       ti = Time.frameCount;
      /*  //"nose", "leftShoulder", "rightShoulder", "leftElbow", "rightElbow", "leftWrist", "rightWrist", "leftHip", "rightHip", "leftKnee", "rightKnee", "leftAnkle", "rightAnkle"
        pos7 = new Vector4((script.posePositions[11].x + script.posePositions[12].x) / script.imageDims.x * 0.5f, (script.posePositions[11].y + script.posePositions[12].y) / script.imageDims.y * 0.5f, (script.posePositions[11].z + script.posePositions[12].z) * 0.05f, 0);
        pr = 5 * Vector2.Distance(new Vector2(pos7.x, pos7.y), new Vector2((script.posePositions[5].x + script.posePositions[6].x) / script.imageDims.x, (script.posePositions[5].y + script.posePositions[6].y) / script.imageDims.y) * 0.5f);
        p0 = ((new Vector2(script.posePositions[0].x / script.imageDims.x, script.posePositions[0].y / script.imageDims.y) - new Vector2(pos7.x, pos7.y)) / pr + a);
        p1 = ((new Vector2(script.posePositions[5].x / script.imageDims.x, script.posePositions[5].y / script.imageDims.y) - new Vector2(pos7.x, pos7.y)) / pr + a);
        p2 = ((new Vector2(script.posePositions[6].x / script.imageDims.x, script.posePositions[6].y / script.imageDims.y) - new Vector2(pos7.x, pos7.y)) / pr + a);
        p3 = ((new Vector2(script.posePositions[7].x / script.imageDims.x, script.posePositions[7].y / script.imageDims.y) - new Vector2(pos7.x, pos7.y)) / pr + a);
        p4 = ((new Vector2(script.posePositions[8].x / script.imageDims.x, script.posePositions[8].y / script.imageDims.y) - new Vector2(pos7.x, pos7.y)) / pr + a);
        p5 = ((new Vector2(script.posePositions[9].x / script.imageDims.x, script.posePositions[9].y / script.imageDims.y) - new Vector2(pos7.x, pos7.y)) / pr + a);
        p6 = ((new Vector2(script.posePositions[10].x / script.imageDims.x, script.posePositions[10].y / script.imageDims.y) - new Vector2(pos7.x, pos7.y)) / pr + a);
        p8 = ((new Vector2(script.posePositions[13].x / script.imageDims.x, script.posePositions[13].y / script.imageDims.y) - new Vector2(pos7.x, pos7.y)) / pr + a);
        p9 = ((new Vector2(script.posePositions[14].x / script.imageDims.x, script.posePositions[14].y / script.imageDims.y) - new Vector2(pos7.x, pos7.y)) / pr + a);
        p10 = ((new Vector2(script.posePositions[15].x / script.imageDims.x, script.posePositions[15].y / script.imageDims.y) - new Vector2(pos7.x, pos7.y)) / pr + a);
        p11 = ((new Vector2(script.posePositions[16].x / script.imageDims.x, script.posePositions[16].y / script.imageDims.y) - new Vector2(pos7.x, pos7.y)) / pr + a);
        pos0 = new Vector4(p0.x, p0.y, script.posePositions[0].z * 0.1f, 0);
        pos1 = new Vector4(p1.x, p1.y, script.posePositions[5].z * 0.1f, 0);
        pos2 = new Vector4(p2.x, p2.y, script.posePositions[6].z * 0.1f, 0);
        pos3 = new Vector4(p3.x, p3.y, script.posePositions[7].z * 0.1f, 0);
        pos4 = new Vector4(p4.x, p4.y, script.posePositions[8].z * 0.1f, 0);
        pos5 = new Vector4(p5.x, p5.y, script.posePositions[9].z * 0.1f, 0);
        pos6 = new Vector4(p6.x, p6.y, script.posePositions[10].z * 0.1f, 0);
        pos8 = new Vector4(p8.x, p8.y, script.posePositions[13].z * 0.1f, 0);
        pos9 = new Vector4(p9.x, p9.y, script.posePositions[14].z * 0.1f, 0);
        pos10 = new Vector4(p10.x, p10.y, script.posePositions[15].z * 0.1f, 0);
        pos11 = new Vector4(p11.x, p11.y, script.posePositions[16].z * 0.1f, 0);     */

        
        compute_shader.SetTexture(handle_main, "reader", A);
        compute_shader.SetVector("_pos0", script.pos0);
        compute_shader.SetVector("_pos1", script.pos1);
        compute_shader.SetVector("_pos2", script.pos2);
        compute_shader.SetVector("_pos3", script.pos3);
        compute_shader.SetVector("_pos4", script.pos4);
        compute_shader.SetVector("_pos5", script.pos5);
        compute_shader.SetVector("_pos6", script.pos6);
        compute_shader.SetVector("_pos7", script.pos7);
        compute_shader.SetVector("_pos8", script.pos8);
        compute_shader.SetVector("_pos9", script.pos9);
        compute_shader.SetVector("_pos10", script.pos10);
        compute_shader.SetVector("_pos11", script.pos11);
        compute_shader.SetFloat("_time", ti);
       compute_shader.SetTexture(handle_main, "writer", B);
       compute_shader.Dispatch(handle_main, B.width / 8, B.height / 8, 1);
       compute_shader.SetTexture(handle_main, "reader", B);
       compute_shader.SetTexture(handle_main, "writer", A);
       compute_shader.Dispatch(handle_main, B.width / 8, B.height / 8, 1);
       capture.frame = (ti + 1) / (resx * Mathf.Floor(resy / 12));
        capture.Tex = B;
       preview.SetTexture("_MainTex", B);
       final.SetTexture("_PosTex", B);
       final.SetFloat("_time", ti);
       /* final.SetFloat("_pr", script.pr);
        final.SetFloat("_pp", script.pp);  */
    }

   private void CleanupResources()
   {
       // Destroy the RenderTexture
       if (A != null)
       {
           Destroy(A);
       }

   }
   private void OnEnable()
   {

       if (A == null)
       {
           A = new RenderTexture(resx, resy, 0);
           A.enableRandomWrite = true;
           A.Create();
       }

   }

   }
     