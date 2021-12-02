using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR;

public class Slicer : MonoBehaviour
{
    private SaberManager saber;

    private void Start()
    {
        Assert.IsNotNull(GetComponentInParent<SaberManager>());
        saber = GetComponentInParent<SaberManager>();
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        StartCoroutine("SliceRoutine", collision);

    }
    /*This WON'T be the way-to-go method. It might be moved to CubeManager, and it will be just a backup method (the slice direction will be determined with OnCollisionEnter-OnCollisionExit methods)*/
    IEnumerator SliceRoutine(Collision collision)
    {
        //saves the blade up vector on collision
        Vector3 startBladeRotation = saber.transform.up.normalized;
        GameObject sliceTarget = collision.collider.gameObject;
        yield return new WaitForSeconds(0.1f);
        //saves the new up vector of the blade after 0.1 seconds
        Vector3 endBladeRotation = saber.transform.up.normalized;
        // the cross product of the two vector gives the normal of the plane they reside in.
        Vector3 cuttingPlaneNormal = Vector3.Cross(startBladeRotation, endBladeRotation).normalized;
        if (sliceTarget.TryGetComponent(out CubeManager cube))
        {
            cube.Slice(sliceTarget, saber.transform.position, cuttingPlaneNormal);
        }       
    }

    
}
