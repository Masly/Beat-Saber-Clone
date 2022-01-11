using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CubeMover : MonoBehaviour
{
    public int beatSpawned;
    //this will be injected on creation for now
    private Vector3 spawnPosition;
    private Vector3 targetPosition;
    //value of z needed before destroying the cube
    public float destroyValue;
    
    private int beatsToTravel;
    public bool debug = false;
    private CubeManager cubeManager;
    private GameplaySettings gameplaySettings;
    private void Awake()
    {
        cubeManager = GetComponent<CubeManager>();
        Assert.IsNotNull(cubeManager);
    }

    private void Start()
    {
        //I'm decoupling the values from GameplaySettings so if I change them outside of this script I have to look only in one place
        gameplaySettings = cubeManager.settingsProvider.gameplaySettings;
        spawnPosition = gameplaySettings.cubeSpawnPosition.position;
        Vector3 playerPosition = gameplaySettings.playerSpawnPosition.transform.position;
        targetPosition = playerPosition - spawnPosition;
        destroyValue = playerPosition.z + gameplaySettings.distanceBeforeDestroying;
        beatsToTravel = gameplaySettings.beatsInAdvance;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
            SetPosition();
    }
    /*
     * ASSUMPTIONS:
     * This will only move on the z axis
     */
    void SetPosition()
    {
        Vector3 newPosition = spawnPosition;
        float BeatsSinceSpawned = AudioManager.singleton.currentBeat - beatSpawned;
        float lerpValue;
        
        
        if (BeatsSinceSpawned <= 0)
        {
            newPosition.y = -10;
        }
        else
        {
            lerpValue = BeatsSinceSpawned / beatsToTravel;
            if(lerpValue <= 1)
            {
                newPosition.z = Mathf.Lerp(spawnPosition.z, targetPosition.z, lerpValue);

            }
            //vibrate when on center if debugging
            if (debug)
            {
                if (Mathf.Abs(lerpValue - 0.5f) <= 0.01)
                {
                    VibrationManager.singleton.CustomVibrate(0.5f, 0.01f);
                }
            }
        }

        transform.SetPositionAndRotation(newPosition, transform.rotation);
        if (transform.position.z > destroyValue)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
