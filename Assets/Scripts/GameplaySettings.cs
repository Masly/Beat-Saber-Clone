using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameplaySettings : MonoBehaviour
{
    public static GameplaySettings singleton;
    //how many beats will pass between spawning and reaching the player
    public int beatsInAdvance;
    //the cubes will be spawned in this position
    public Transform spawnPosition;
    //how far will the cube move before being deleted
    public float distanceBeforeDestroying;

    public GameObject player;
    private void Awake()
    {
        //Set up singleton
        if (singleton && singleton != this)
            Destroy(this);
        else
            singleton = this;
        
    }

    private void OnValidate()
    {
        Assert.IsNotNull(player);
        Assert.IsNotNull(spawnPosition);
    }
}
