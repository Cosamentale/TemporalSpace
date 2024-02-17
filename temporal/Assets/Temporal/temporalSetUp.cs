
using UnityEngine;
using RenderHeads.Media.AVProLiveCamera;

public class temporalSetUp : MonoBehaviour
{
    [Range(0f, 1f)]
    public float Gain = 1;
    public int cam1;
    public int cam2;
    void Start()
    // void Update()
    {


       // AVProLiveCameraDevice LiveCamera = AVProLiveCameraManager.Instance.GetDevice(0);
                                                                                              

    }
    void Update()
    {
        AVProLiveCameraDevice LiveCamera = AVProLiveCameraManager.Instance.GetDevice(cam1);      
       for (int j = 0; j < LiveCamera.NumSettings; j++)
        {
            AVProLiveCameraSettingBase settingBase = LiveCamera.GetVideoSettingByIndex(j);

            settingBase.IsAutomatic = false;
            settingBase.SetDefault();


        }
        AVProLiveCameraSettingBase gainSetting = LiveCamera.GetVideoSettingByIndex(6);
        AVProLiveCameraSettingFloat settingFloat = (AVProLiveCameraSettingFloat)gainSetting;
        settingFloat.CurrentValue = 255 * Gain;
        AVProLiveCameraSettingBase exSetting = LiveCamera.GetVideoSettingByIndex(10);
        AVProLiveCameraSettingFloat settingFloatb = (AVProLiveCameraSettingFloat)exSetting;
        settingFloatb.CurrentValue = -5;

        AVProLiveCameraDevice LiveCamera2 = AVProLiveCameraManager.Instance.GetDevice(cam2);      
        for (int j = 0; j < LiveCamera.NumSettings; j++)
        {
            AVProLiveCameraSettingBase settingBase2 = LiveCamera2.GetVideoSettingByIndex(j);

            settingBase2.IsAutomatic = false;
            settingBase2.SetDefault();


        }
        AVProLiveCameraSettingBase gainSetting2 = LiveCamera2.GetVideoSettingByIndex(6);
        AVProLiveCameraSettingFloat settingFloat2 = (AVProLiveCameraSettingFloat)gainSetting2;
        settingFloat2.CurrentValue = 255 * Gain;
        AVProLiveCameraSettingBase exSetting2 = LiveCamera2.GetVideoSettingByIndex(10);
        AVProLiveCameraSettingFloat settingFloat2b = (AVProLiveCameraSettingFloat)exSetting2;
        settingFloat2b.CurrentValue = -5;
    }
}