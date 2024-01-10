using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamName : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        for (int i = 0; i < devices.Length; i++)
        {
            print("Webcam available: " + devices[i].name);
        }
    }

    // Update is called once per frame
    void Update()
    {

        // for debugging purposes, prints available devices to the console
           
    }
}
