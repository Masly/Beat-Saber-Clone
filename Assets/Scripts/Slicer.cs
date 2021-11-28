using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Slicer : MonoBehaviour
{
    public Material crossMat;


    //testing variables
    public Material upperMaterial;
    public Material lowerMaterial;
    public float forceMultiplier = 1;
    GameObject upperHull;
    GameObject lowerHull;

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine("SliceRoutine", collision);
    }
    public void Slice(GameObject target, Vector3 startingPoint, Vector3 planeNormal)
    {
        
        SlicedHull hull = target.Slice(startingPoint, planeNormal, crossMat);
        if (hull != null)
        {
            lowerHull = hull.CreateLowerHull(target, crossMat);
            upperHull = hull.CreateUpperHull(target, crossMat);
            lowerHull.GetComponent<Renderer>().material = lowerMaterial;
            upperHull.GetComponent<Renderer>().material = upperMaterial;
            upperHull.AddComponent<Rigidbody>();
            upperHull.GetComponent<Rigidbody>().AddForce(planeNormal*forceMultiplier);
            lowerHull.AddComponent<Rigidbody>();
            planeNormal = Quaternion.AngleAxis(180, Vector3.up) * planeNormal;
            
            lowerHull.GetComponent<Rigidbody>().AddForce(planeNormal*forceMultiplier);

            target.SetActive(false);
        }
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
        Slice(sliceTarget, entryPoint, cuttingPlaneNormal);
    }
}
