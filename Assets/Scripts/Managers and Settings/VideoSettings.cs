using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class VideoSettings : MonoBehaviour, IVideoSettings
{
    public static VideoSettings singleton;
    [SerializeField]
    private Material _leftMaterial;
    [SerializeField]
    private Material _rightMaterial;

    Material IVideoSettings.leftMaterial =>  _leftMaterial;

    Material IVideoSettings.rightMaterial => _rightMaterial;

}
