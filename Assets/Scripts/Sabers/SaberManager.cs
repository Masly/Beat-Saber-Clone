using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*all saber data will be here*/
public class SaberManager : MonoBehaviour
{
    public GameObject blade;
    
    public bool isLeft;
    private void OnValidate()
    {
        if (isLeft)
            blade.GetComponent<MeshRenderer>().material = VideoSettings.singleton.leftMaterial;
        else
            blade.GetComponent<MeshRenderer>().material = VideoSettings.singleton.rightMaterial;
    }
}
