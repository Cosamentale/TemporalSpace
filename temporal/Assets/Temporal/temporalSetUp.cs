
using UnityEngine;
using RenderHeads.Media.AVProLiveCamera;

public class temporalSetUp : MonoBehaviour
{
    [Range(0f, 1f)]
    public float Gain = 1;
    void Start()
    // void Update()
    {


       // AVProLiveCameraDevice LiveCamera = AVProLiveCameraManager.Instance.GetDevice(0);
                                                                                              

    }
    void Update()
    {
        AVProLiveCameraDevice LiveCamera = AVProLiveCameraManager.Instance.GetDevice(0);
        AVProLiveCameraSettingBase gainSetting = LiveCamera.GetVideoSettingByIndex(6);
        AVProLiveCameraSettingFloat settingFloat = (AVProLiveCameraSettingFloat)gainSetting;
        settingFloat.CurrentValue = 70 * Gain;
       /* for (int j = 0; j < LiveCamera.NumSettings; j++)
        {
            AVProLiveCameraSettingBase settingBase = LiveCamera.GetVideoSettingByIndex(j);

            settingBase.IsAutomatic = false;
            settingBase.SetDefault();


        }  */


    }
}