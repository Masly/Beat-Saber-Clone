using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager singleton;

    //I might want to have them as parameters for the vibration method later
    public float vibrationAmplitude = 0.5f;
    public float vibrationDuration = 0.2f;


    UnityEngine.XR.InputDevice leftController;
    UnityEngine.XR.InputDevice rightController;
    bool initialized = false;

    void Start()
    {
        //Set up singleton
        if (singleton && singleton != this)
            Destroy(this);
        else
            singleton = this;

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
    public void SendHapticFeedback(bool isLeft)
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
        uint channel = 0;
        targetDevice.SendHapticImpulse(channel, vibrationAmplitude, vibrationDuration);
    }

}
