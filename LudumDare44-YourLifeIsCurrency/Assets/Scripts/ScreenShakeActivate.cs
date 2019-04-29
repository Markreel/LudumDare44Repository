using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeActivate : MonoBehaviour
{
    public bool VolcanoShake;
    public bool WindShake;

    private void OnEnable()
    {
        if (VolcanoShake)
            ShakeAccordingToVolcanoLevel();

        if (WindShake)
            ShakeAccordingToVolcanoLevel();
    }

    void ShakeAccordingToVolcanoLevel()
    {
        switch (GodManager.Instance.VolcanoLevel)
        {
            default:
            case 1:
                ScreenShake.Instance.Shake(2, 0.1f, 0.015f);
                break;
            case 2:
                ScreenShake.Instance.Shake(2, 0.2f, 0.015f);
                break;
            case 3:
                ScreenShake.Instance.Shake(2, 0.4f, 0.01f);
                break;
            case 4:
                ScreenShake.Instance.Shake(2, 0.7f, 0.01f);
                break;
            case 5:
                ScreenShake.Instance.Shake(2, 0.8f, 0.01f);
                break;
        }
    }

    void ShakeAccordingToWindLevel()
    {
        switch (GodManager.Instance.WindLevel)
        {
            default:
            case 1:
                ScreenShake.Instance.Shake(2, 0.1f, 0.02f);
                break;
            case 2:
                ScreenShake.Instance.Shake(2, 0.2f, 0.02f);
                break;
            case 3:
                ScreenShake.Instance.Shake(2, 0.4f, 0.01f);
                break;
            case 4:
                ScreenShake.Instance.Shake(2, 0.7f, 0.01f);
                break;
            case 5:
                ScreenShake.Instance.Shake(2, 0.8f, 0.01f);
                break;
        }
    }

}
