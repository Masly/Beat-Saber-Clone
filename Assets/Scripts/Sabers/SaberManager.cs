using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*all saber data will be here*/
public class SaberManager : MonoBehaviour
{
    public GameObject blade;

    VideoSettings videoSettings;
    
    public bool isLeft;
    private void OnValidate()
    {
        
        videoSettings = GameObject.FindObjectOfType<VideoSettings>();
        if (isLeft)
            blade.GetComponent<MeshRenderer>().material = videoSettings.leftMaterial;
        else
            blade.GetComponent<MeshRenderer>().material = videoSettings.rightMaterial;
    }
}
