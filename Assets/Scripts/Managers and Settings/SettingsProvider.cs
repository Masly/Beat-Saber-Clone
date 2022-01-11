using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
/*
* I got the general idea from this https://www.youtube.com/watch?v=Rz2q3JzOOjs video but I don't really need to implement the whole pattern since it's just data 
*/
public class SettingsProvider : MonoBehaviour
{
    [HideInInspector]
    public VideoSettings videoSettings;

    private void OnValidate()
    {
        videoSettings = GetComponent<VideoSettings>();
        Assert.IsNotNull(videoSettings);
    }
}
