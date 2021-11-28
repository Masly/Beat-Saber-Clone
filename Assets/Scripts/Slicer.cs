using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Slicer : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine("SliceRoutine", collision);
    }
    
    IEnumerator SliceRoutine(Collision collision)
    {
        Vector3 entryRotation = transform.up.normalized;
        ContactPoint contact = collision.contacts[0];
        Vector3 entryPoint = contact.point;
        GameObject sliceTarget = collision.collider.gameObject;
        yield return new WaitForSeconds(0.1f);
        Vector3 exitDirection = transform.up.normalized;

        Vector3 cuttingPlaneNormal = Vector3.Cross(entryRotation, exitDirection).normalized;
        if (collision.gameObject.TryGetComponent(out CubeManager cube))
        {
            cube.Slice(sliceTarget, entryPoint, cuttingPlaneNormal);
        }       
    }
}
