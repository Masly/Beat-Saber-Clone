using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSlicer : MonoBehaviour
{
    //TODO: change to explosion
    [SerializeField]
    private float forceMultiplier = 20;
    [SerializeField]
    private GameObject hullPrefab;
    private Material hullMaterial;
    private Material crossMat;
    private void Start()
    {
        hullMaterial = gameObject.GetComponent<MeshRenderer>().material;
        crossMat = gameObject.GetComponent<CubeController>().settingsProvider.videoSettings.crossCubeMaterial;
    }

    /*this slices the cube mesh, instantiates two hulls prefabs, gives them the appropriate mesh and materials, applies a force to separate them, then hides the cube prefab*/
    public void Slice(GameObject target, Vector3 startingPoint, Vector3 planeNormal)
    {
        // I'm using this plugin: https://github.com/DavidArayan/ezy-slice
        SlicedHull hull = target.Slice(startingPoint, planeNormal, crossMat);
        //it's tidier to instantiate a prefab and then change its mesh and materials than instantiate a new object with EzySlice.SlicedHull.CreateLowerHull and then add rigidbodies and stuff
        if (hull != null)
        {
            GameObject upperHull = Instantiate(hullPrefab, transform.position, Quaternion.identity);
            upperHull.GetComponent<MeshFilter>().mesh = hull.upperHull;
            upperHull.GetComponent<MeshRenderer>().materials = new Material[] { hullMaterial, crossMat };
            //TODO: change to explosion
            upperHull.GetComponent<Rigidbody>().AddForce(planeNormal * forceMultiplier);
            GameObject lowerHull = Instantiate(hullPrefab, transform.position, Quaternion.identity);

            lowerHull.GetComponent<MeshFilter>().mesh = hull.lowerHull;
            lowerHull.GetComponent<MeshRenderer>().materials = new Material[] { hullMaterial, crossMat };
            //I'm just rotating the plane normal to get a force in the opposite direction
            planeNormal = Quaternion.AngleAxis(180, Vector3.up) * planeNormal;
            //TODO: change to explosion
            lowerHull.GetComponent<Rigidbody>().AddForce(planeNormal * forceMultiplier);

            //disable the cube to not have it in the way
            target.SetActive(false);
        }
    }
}
