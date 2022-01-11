using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * https://www.youtube.com/watch?v=Rz2q3JzOOjs gave me the general idea on how to use the Service Locator pattern in unity
 * 
 */

public class SettingsProvider : MonoBehaviour
{
    private IVideoSettings videoSettingsImplementation;
    //gameplay settings

    //code gets called in-editor too, so better put it in OnValidate (it executes also OnAwake)
    private void OnValidate()
    {
        videoSettingsImplementation = GetComponent<IVideoSettings>();
    }

    //TODO: change the implementation using an internal class to separate different services
    //I fear that forwarding so many times a simple value is very inefficient, but performance is not yet an issue
    public Material leftMaterial => videoSettingsImplementation.leftMaterial;
    public Material rightMaterial => videoSettingsImplementation.rightMaterial;
}
