using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public float speed=1;
    public Material crossMat;


    public Material upperMaterial;
    public Material lowerMaterial;
    public float forceMultiplier = 20;
    public GameObject hullPrefab;
   // public GameObject lowerHull;
    private void FixedUpdate()
    {
        transform.Translate(-Vector3.forward*speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 3);
    }
    public void Slice(GameObject target, Vector3 startingPoint, Vector3 planeNormal)
    {

        SlicedHull hull = target.Slice(startingPoint, planeNormal, crossMat);
        if (hull != null)
        {

            // GameObject tempLowerHull = hull.CreateLowerHull(target, crossMat);
            //  GameObject tempUpperHull = hull.CreateUpperHull(target, crossMat);
            //lowerMaterial will be set based on the cube color
            GameObject upperHull = Instantiate(hullPrefab, transform.position, Quaternion.identity);
            upperHull.GetComponent<MeshFilter>().mesh = hull.upperHull;
            upperHull.GetComponent<MeshRenderer>().materials = new Material[] { upperMaterial, crossMat };

            upperHull.GetComponent<Rigidbody>().AddForce(planeNormal * forceMultiplier);
            GameObject lowerHull = Instantiate(hullPrefab, transform.position, Quaternion.identity);
            //lowerMaterial will be set based on the cube color
            lowerHull.GetComponent<MeshFilter>().mesh = hull.lowerHull;
            lowerHull.GetComponent<MeshRenderer>().materials = new Material[] { lowerMaterial, crossMat };
            
            planeNormal = Quaternion.AngleAxis(180, Vector3.up) * planeNormal;

            lowerHull.GetComponent<Rigidbody>().AddForce(planeNormal * forceMultiplier);
    
            target.SetActive(false);
        }
    }

    public void CopyVisuals()
    {

    }
}

namespace MyGameObjectExtension
{
    public static class CubeExtension
    {/*
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }
        */
        public static void CopyVisuals(this GameObject obj, GameObject other)
        {
            
        }
    }
}