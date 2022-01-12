using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CubeMover : MonoBehaviour
{
    [HideInInspector]
    public int beatSpawned;
    //this will be injected on creation for now
    private Vector3 spawnPosition;
    private Vector3 targetPosition;
    //value of z needed before destroying the cube
    private float destroyValue;
    private int beatsToTravel;
    private CubeController cubeController;
    private GameplaySettings gameplaySettings;
    private void Awake()
    {
        cubeController = GetComponent<CubeController>();
        Assert.IsNotNull(cubeController);
    }

    private void Start()
    {
        //I'm decoupling the values from GameplaySettings so if I change them outside of this script I have to look only in one place
        gameplaySettings = cubeController.settingsProvider.gameplaySettings;
        spawnPosition = gameplaySettings.cubeSpawnPosition.position;
        Vector3 playerPosition = gameplaySettings.playerSpawnPosition.transform.position;
        targetPosition = playerPosition - spawnPosition;
        destroyValue = playerPosition.z + gameplaySettings.distanceBeforeDestroying;
        beatsToTravel = gameplaySettings.beatsInAdvance;

    }
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
        float BeatsSinceSpawned = cubeController.serviceProvider.audioService.currentBeat - beatSpawned;
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
        }

        transform.SetPositionAndRotation(newPosition, transform.rotation);
        if (transform.position.z > destroyValue)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
