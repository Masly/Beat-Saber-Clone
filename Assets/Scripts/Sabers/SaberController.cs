using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/*all saber data will be here*/
public class SaberController : MonoBehaviour
{
    public GameObject blade;
    public bool isLeft;


    private void OnValidate()
    {
        VideoSettings videoSettings = GameObject.FindObjectOfType<SettingsProvider>().videoSettings;
        Assert.IsNotNull(videoSettings);
        if (isLeft)
            blade.GetComponent<MeshRenderer>().material = videoSettings.leftMaterial;
        else
            blade.GetComponent<MeshRenderer>().material = videoSettings.rightMaterial;
    }
}
