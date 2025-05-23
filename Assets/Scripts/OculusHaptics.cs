using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VibrationForce
{
    Light,
    Medium,
    Hard,
}


public class OculusHaptics : MonoBehaviour
{

    [SerializeField]
    OVRInput.Controller controllerMask;

    private OVRHapticsClip clipLight;
    private OVRHapticsClip clipMedium;
    private OVRHapticsClip clipHard;

    private bool forcedHaptic;

    public float lowViveHaptics { get; private set; }
    public float mediumViveHaptics { get; private set; }
    public float hardViveHaptics { get; private set; }


    private void Start()
    {
        InitializeOVRHaptics();
    }

    public void InitializeOVRHaptics()
    {

        int cnt = 10;
        clipLight = new OVRHapticsClip(cnt);
        Debug.Log(clipLight.Samples.Length);
        clipMedium = new OVRHapticsClip(cnt);
        clipHard = new OVRHapticsClip(cnt);
        for (int i = 0; i < cnt; i++)
        {
            clipLight.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)45;
            clipMedium.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)100;
            clipHard.Samples[i] = i % 2 == 0 ? (byte)0 : (byte)180;
        }

        clipLight = new OVRHapticsClip(clipLight.Samples, clipLight.Samples.Length);
        clipMedium = new OVRHapticsClip(clipMedium.Samples, clipMedium.Samples.Length);
        clipHard = new OVRHapticsClip(clipHard.Samples, clipHard.Samples.Length);
    }


    void OnEnable()
    {
        InitializeOVRHaptics();
    }

    public void Vibrate(VibrationForce vibrationForce)
    {
        var channel = OVRHaptics.RightChannel;
        if (controllerMask == OVRInput.Controller.LTouch)
            channel = OVRHaptics.LeftChannel;

        switch (vibrationForce)
        {
            case VibrationForce.Light:
                channel.Preempt(clipLight);
                break;
            case VibrationForce.Medium:
                channel.Preempt(clipMedium);
                break;
            case VibrationForce.Hard:
                channel.Preempt(clipHard);
                break;
        }
    }



    public IEnumerator VibrateTime(VibrationForce force, float time)
    {
        forcedHaptic = true;
        var channel = OVRHaptics.RightChannel;
        if (controllerMask == OVRInput.Controller.LTouch)
            channel = OVRHaptics.LeftChannel;

        for (float t = 0; t <= time; t += Time.deltaTime)
        {
            switch (force)
            {
                case VibrationForce.Light:
                    channel.Queue(clipLight);
                    break;
                case VibrationForce.Medium:
                    channel.Queue(clipMedium);
                    break;
                case VibrationForce.Hard:
                    channel.Queue(clipHard);
                    break;
            }
        }
        yield return new WaitForSeconds(time);
        channel.Clear();
        forcedHaptic = false;
        yield return null;

    }
}
