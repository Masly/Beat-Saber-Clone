using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class VideoSettings : MonoBehaviour
{
    public static VideoSettings singleton;
    public Material leftMaterial;
    public Material rightMaterial;
    
    //maybe I'm not using the singleton pattern correctly(look at trello for detail)
    private void Awake()
    {
        //Set up singleton
        if (singleton && singleton != this)
            Destroy(this);
        else
            singleton = this;

    }
    private void OnValidate()
    {
        Assert.IsNotNull(leftMaterial);
        Assert.IsNotNull(rightMaterial);
        //Set up singleton
        if (singleton && singleton != this)
            Destroy(this);
        else
            singleton = this;
    }

}
