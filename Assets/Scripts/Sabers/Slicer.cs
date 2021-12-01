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
        saber = GetComponentInParent<SaberManager>();
        //TODO: I'm pretty sure there is a method that checks in the editor if the required object is there, something like a "required" flag. It's ok to leave it like that now that's just in testing
        Assert.IsNotNull(saber);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine("SliceRoutine", collision);
        if(VibrationManager.singleton!=null)
            VibrationManager.singleton.SendHapticFeedback(saber.isLeft);
    }
    /*IMPORTANT BUG: IT WORKS ONLY IF THE CUBES ARE IN FRONT OF THE PLAYER*/
    /*This WON'T be the way-to-go method. It might be moved to CubeManager, and it will be just a backup method (the slice direction will be determined with OnCollisionEnter-OnCollisionExit methods)*/
    IEnumerator SliceRoutine(Collision collision)
    {
        //saves the blade up vector on collision, and the entry point
        Vector3 startBladeRotation = saber.transform.up.normalized;
        ContactPoint contact = collision.contacts[0];
        Vector3 entryPoint = contact.point;
        GameObject sliceTarget = collision.collider.gameObject;
        yield return new WaitForSeconds(0.1f);
        //saves the new up vector of the blade after 0.1 seconds
        Vector3 endBladeRotation = saber.transform.up.normalized;
        // the cross product of the two vector gives the normal of the plane they reside in.
        Vector3 cuttingPlaneNormal = Vector3.Cross(startBladeRotation, endBladeRotation).normalized;
        if (collision.gameObject.TryGetComponent(out CubeManager cube))
        {
            cube.Slice(sliceTarget, saber.transform.position, cuttingPlaneNormal);
        }       
    }

    
}
