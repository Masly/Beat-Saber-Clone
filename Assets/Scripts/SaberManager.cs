using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SaberManager : MonoBehaviour
{

    public Transform bladeTip;
    public Material crossMat;

    Vector3[] tipPositions = new Vector3[10];
    Vector3 swingDirection;

    //testing variables
    public Material upperMaterial;
    public Material lowerMaterial;

    GameObject upperHull;
    GameObject lowerHull;
    private void Start()
    {
        /*
        int[] test = { 1, 2, 3, 4, 5 };

        for(int i = 0; i < 6; i++)
        {
            System.Array.Copy(test, 1, test, 0, test.Length-1);
            test[test.Length-1] = 8;
            string message = "[";
            for(int j = 0; j< test.Length; j++)
            {
                message += test[j].ToString();
            }
            message += "]";
            Debug.Log(message);
        }
        */

    }
    
    private void Update()
    {
        UpdateSwingDirection();
        if (Input.GetButtonDown("Destroy"))
            DestroyHulls();

        
    }
    /*each update, I compare the position of the tip of the blade to the position it had five updates ago, to get the swing direction*/
    void UpdateSwingDirection()
    {
        //shifts the array left (tidier code, also faster)
        System.Array.Copy(tipPositions, 1, tipPositions, 0, tipPositions.Length - 1);

        tipPositions[tipPositions.Length - 1] = bladeTip.position;
        swingDirection = (tipPositions[tipPositions.Length - 1] - tipPositions[0]).normalized;
        if (tipPositions[0] != null)
        {
            bladeTip.LookAt(tipPositions[0]);
            bladeTip.Rotate(0, 180, 0);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        ContactPoint contact= collision.contacts[0];
        GameObject objectHit = collision.collider.gameObject;
        if (objectHit.tag=="Cube")
            Slice(objectHit, contact.point);

    }

    public void Slice(GameObject target, Vector3 startingPoint)
    {
        swingDirection = bladeTip.up;
        SlicedHull hull = target.Slice(startingPoint, swingDirection, crossMat);
        if (hull != null)
        {
            lowerHull = hull.CreateLowerHull(target, crossMat);
            upperHull = hull.CreateUpperHull(target, crossMat);
            lowerHull.GetComponent<Renderer>().material = lowerMaterial;
            upperHull.GetComponent<Renderer>().material = upperMaterial;
            //upperHull.AddComponent<Rigidbody>();
            //upperHull.GetComponent<Rigidbody>().AddForce(Vector3.left);
            //lowerHull.AddComponent<Rigidbody>();
            //lowerHull.GetComponent<Rigidbody>().AddForce(Vector3.right);

            target.SetActive(false);
        }
    }
    void DestroyHulls()
    {
        Destroy(upperHull);
        Destroy(lowerHull);
    }

}
