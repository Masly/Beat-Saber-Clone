using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public Material crossMat;
    //no reason to move them to HullManager because the hulls will receive the color from CubeManager anyway
    public Material upperMaterial;
    public Material lowerMaterial;
    //TODO: change to explosion
    public float forceMultiplier = 20;
    public GameObject hullPrefab;
    public bool isLeft = false;
    protected void Start()
    {
        //Debug.Log("base start called");
        //get a random true or false
        isLeft = Random.value < 0.5f;
        //Debug: inject videoSettings from spawner to optimize it
        VideoSettings videoSettings = GameObject.FindObjectOfType<SettingsProvider>().videoSettings;
        UnityEngine.Assertions.Assert.IsNotNull(videoSettings);

        if (isLeft)
        {
            GetComponent<MeshRenderer>().material = videoSettings.leftMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = videoSettings.rightMaterial;
        }
        
        
    }
    
    protected void OnCollisionEnter(Collision collision)
    {
            //cubes can only collide with sabers and will never collide with anything else
            if (collision.gameObject.GetComponentInParent<SaberManager>().isLeft == isLeft)
            {
                GameManager.singleton.AddScore(5);
            if (VibrationManager.singleton != null)
                VibrationManager.singleton.StandardVibrate(isLeft);
        }
            Destroy(gameObject, 2);

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
            upperHull.GetComponent<MeshRenderer>().materials = new Material[] { upperMaterial, crossMat };
            //TODO: change to explosion
            upperHull.GetComponent<Rigidbody>().AddForce(planeNormal * forceMultiplier);
            GameObject lowerHull = Instantiate(hullPrefab, transform.position, Quaternion.identity);

            lowerHull.GetComponent<MeshFilter>().mesh = hull.lowerHull;
            lowerHull.GetComponent<MeshRenderer>().materials = new Material[] { lowerMaterial, crossMat };
            //I'm just rotating the plane normal to get a force in the opposite direction
            planeNormal = Quaternion.AngleAxis(180, Vector3.up) * planeNormal;
            //TODO: change to explosion
            lowerHull.GetComponent<Rigidbody>().AddForce(planeNormal * forceMultiplier);
            
            //disable the cube to not have it in the way
            target.SetActive(false);
        }
    }

}
