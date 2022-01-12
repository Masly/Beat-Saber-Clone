using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameplaySettings : MonoBehaviour
{
    //how many beats will pass between spawning and reaching the player
    public int beatsInAdvance;
    //the cubes will be spawned in this position
    public Transform cubeSpawnPosition;
    //the player starting position
    public Transform playerSpawnPosition;
    //how far will the cube move before being deleted
    public float distanceBeforeDestroying;
    public int scorePerCube = 5;


    private void OnValidate()
    {
        Assert.IsNotNull(cubeSpawnPosition);
        //TODO: make the player position themselve in the spawn position, and not this to look at the player position
        playerSpawnPosition = GameObject.FindGameObjectWithTag("Player").transform;
        Assert.IsNotNull(playerSpawnPosition);
    }
}
