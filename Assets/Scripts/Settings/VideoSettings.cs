using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

//MonoBehaviour lets me change stuff in editor
public class VideoSettings : MonoBehaviour
{
    //TODO: make Settings inherit from "Setting" class to have automatic field validation
    public Material leftMaterial;
    public Material rightMaterial;
    public Material crossCubeMaterial;
}
