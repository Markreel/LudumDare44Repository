using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    float timePerStep = 0.02f;
    float curTimePerStep;

    Vector3 originalPos;

    void Awake()
    {
        Instance = this;

        if (camTransform == null)
        {
            camTransform = transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    Shake(2, 0.1f, 0.02f);
        //}

            //if (Input.GetKeyDown(KeyCode.UpArrow))
            //{
            //    Shake(2, 0.8f, 0.01f);
            //}

        if (shakeDuration > 0)
        {
            if (curTimePerStep <= 0)
            {
                curTimePerStep = timePerStep;
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            }
            else
                curTimePerStep -= Time.deltaTime;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    void ShakeAccordingToVolcanoLevel()
    {
        switch (GodManager.Instance.VolcanoLevel)
        {
            default:
            case 1:
                Shake(2, 0.1f, 0.02f);
                break;
            case 2:
                Shake(2, 0.2f, 0.02f);
                break;
            case 3:
                Shake(2, 0.4f, 0.01f);
                break;
            case 4:
                Shake(2, 0.7f, 0.01f);
                break;
            case 5:
                Shake(2, 0.8f, 0.01f);
                break;
        }
    }

    void ShakeAccordingToWindLevel()
    {
        switch (GodManager.Instance.VolcanoLevel)
        {
            default:
            case 1:
                Shake(2, 0.1f, 0.02f);
                break;
            case 2:
                Shake(2, 0.2f, 0.02f);
                break;
            case 3:
                Shake(2, 0.4f, 0.01f);
                break;
            case 4:
                Shake(2, 0.7f, 0.01f);
                break;
            case 5:
                Shake(2, 0.8f, 0.01f);
                break;
        }
    }


    public void Shake(float _duration, float _shakeAmount = 0.7f, float _timePerStep = 0.01f)
    {
        originalPos = camTransform.localPosition;
        shakeDuration = _duration;
        shakeAmount = _shakeAmount;

        timePerStep = _timePerStep;
        curTimePerStep = 0;
    }
}