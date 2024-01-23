using UnityEngine;
using UnityEditor;
//using Unity.Barracuda;
using System.IO;
//using RenderHeads.Media.AVProLiveCamera;
public class SaveTextureToFile : MonoBehaviour
{

    public RenderTexture Tex;
    public int captureCounter = 1;
    public float frame;
    private float previousFrame = 0.0f;
    void Start()
    {

    }
    private void Update()
    {
        if (frame > previousFrame && Mathf.Floor(frame) > Mathf.Floor(previousFrame))
        {
            SaveRTToFile(Tex, captureCounter);
            captureCounter++;
        }

        previousFrame = frame;
    }
    public static void SaveRTToFile(RenderTexture rt,int captureCounter)
    {

        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);

        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        RenderTexture.active = null;

        byte[] bytes;
        bytes = tex.EncodeToPNG();
        Object.Destroy(tex);

        // Define the path where you want to save the PNG file
        //string path = Application.persistentDataPath + "/capture" + captureCounter + ".png";
        string path = "D:/GIT/TemporalSpace/Captures" + "/capture" + + captureCounter + ".png";

        // Write the bytes to the file
        File.WriteAllBytes(path, bytes);
        //ebug.Log("File path: " + path);
        // Optional: You can import the asset if needed
        // UnityEditor.AssetDatabase.ImportAsset(path);
    }

}