using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class VibrationManager : MonoBehaviour, IVibrationService
{

    [SerializeField]
    private float defaultAmpitude = 0.5f;
    [SerializeField]
    private float defaultDuration = 0.2f;


    UnityEngine.XR.InputDevice leftController;
    UnityEngine.XR.InputDevice rightController;

    void Start()
    {

        //while playing the scene from the editor, the controller won't be initialized. They will be if the level is loaded from the game itself
        SetupDevices();

    }
    void SetupDevices()
    {
        //get left and right controllers
        InputDeviceCharacteristics trackedControllerFilter = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.TrackedDevice;
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(trackedControllerFilter, devices);
        foreach (var device in devices)
        {
            //check if they can give haptic feedback
            UnityEngine.XR.HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    //assign each device to the corresponding hand
                    if (device.characteristics.HasFlag(InputDeviceCharacteristics.Left))
                    {
                        leftController = device;
                    }
                    else if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right))
                    {
                        rightController = device;
                    }
                }
            }

        }
    }
    public void StandardVibrate()
    {
        SendHapticFeedback(defaultAmpitude, defaultDuration, bothControllers: true);
    }
    public void StandardVibrate(bool isLeft)
    {
        SendHapticFeedback(defaultAmpitude, defaultDuration, isLeft:isLeft);
    }
    public void CustomVibrate(float vibrationAmplitude, float vibrationDuration)
    {
        SendHapticFeedback(vibrationAmplitude, vibrationDuration, bothControllers: true);
    }
    public void CustomVibrate(float vibrationAmplitude, float vibrationDuration, bool isLeft)
    {
        SendHapticFeedback(vibrationAmplitude, vibrationDuration, isLeft:isLeft);
    }

    void SendHapticFeedback( float vibrationAmplitude, float vibrationDuration,  bool isLeft = false, uint channel = 0,bool bothControllers = false)
    {
        //I check here if the controllers are present because they might not be connected during Start()
        if (!leftController.isValid || !rightController.isValid)
        {
            SetupDevices();
        }
        UnityEngine.XR.InputDevice targetDevice;
        if (isLeft)
        {
            targetDevice = leftController;
        }
        else
        {
            targetDevice = rightController;
        }
        if (bothControllers)
        {
            leftController.SendHapticImpulse(channel, vibrationAmplitude, vibrationDuration);
            rightController.SendHapticImpulse(channel, vibrationAmplitude, vibrationDuration);
        }
        else
        {
            targetDevice.SendHapticImpulse(channel, vibrationAmplitude, vibrationDuration);
        }
        
    }
    

}
