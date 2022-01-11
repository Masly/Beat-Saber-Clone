using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

/*all saber data will be here*/
public class SaberManager : MonoBehaviour
{
    public GameObject blade;

    VideoSettings videoSettings;
    
    public bool isLeft;
    private void OnValidate()
    {
        SettingsProvider settingsProvider = GameObject.FindObjectOfType<SettingsProvider>();
        Assert.IsNotNull(settingsProvider);
        if (isLeft)
            blade.GetComponent<MeshRenderer>().material = settingsProvider.leftMaterial;
        else
            blade.GetComponent<MeshRenderer>().material = settingsProvider.rightMaterial;
    }
}
