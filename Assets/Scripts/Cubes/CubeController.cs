
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public bool isLeft = false;
    [HideInInspector]
    public SettingsProvider settingsProvider;
    [HideInInspector]
    public ServiceProvider serviceProvider;

    protected void Awake()
    {
        //SETTING UP PROVIDERS

        //TODO: inject settings provider from spawner to optimize it
        settingsProvider = GameObject.FindObjectOfType<SettingsProvider>();
        UnityEngine.Assertions.Assert.IsNotNull(settingsProvider);
        serviceProvider = GameObject.FindObjectOfType<ServiceProvider>();
        UnityEngine.Assertions.Assert.IsNotNull(serviceProvider);

        //SETTING UP MATERIALS

        //get a random true or false
        isLeft = Random.value < 0.5f;

        VideoSettings videoSettings = settingsProvider.videoSettings;
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
            if (collision.gameObject.GetComponentInParent<SaberController>().isLeft == isLeft)
            {
                serviceProvider.scoreService.AddScore(5);
                serviceProvider.vibrationService.StandardVibrate(isLeft);
            }
            Destroy(gameObject, 2);

    }
    //I need to forward this from CubeManager to encapsulate the slicer
    public void Slice(GameObject target, Vector3 startingPoint, Vector3 planeNormal)
    {
        GetComponent<CubeSlicer>().Slice(target, startingPoint, planeNormal);
    }

}
