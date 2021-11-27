using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class Slicer : MonoBehaviour
{
    public Transform bladeTip;
    public Material crossMat;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    public void Slice(GameObject target, Vector3 startingPoint)
    {

        SlicedHull hull = target.Slice(startingPoint, Vector3.left, crossMat);
        if (hull != null)
        {
            hull.CreateLowerHull(target, crossMat);
            hull.CreateUpperHull(target, crossMat);

            target.SetActive(false);
        }
    }

}
