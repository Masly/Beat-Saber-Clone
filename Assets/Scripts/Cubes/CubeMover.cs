using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public int beatSpawned;
    //this will be injected on creation for now
    public Vector3 spawnPosition;
    //TODO: move it to the manager
    public int distanceInBeats = 10;
    public Vector3 destroyPosition;
   

    //float distanceToCover;
    private void Start()
    {
       
    }
    // Update is called once per frame
    void FixedUpdate()
    {
            SetPosition();
    }
    /*
     * ASSUMPTIONS:
     * Player position in (0,0,0)
     * This will move from CubeSpawner to PlayerPosition
     * This will only move on the x axis
     */
    void SetPosition()
    {
        Vector3 newPosition = spawnPosition;
        //float beatsSinceSpawned = AudioManager.singleton.currentBeat - beatSpawned;
        float positionInBeats = AudioManager.singleton.currentBeat - beatSpawned;
        float lerpValue;
        
        
        if (positionInBeats <= 0)
        {
            newPosition.y = -10;
        }
        else
        {
            lerpValue = positionInBeats / distanceInBeats;
            if(lerpValue <= 1)
            {
                newPosition.z = Mathf.Lerp(spawnPosition.z, destroyPosition.z, lerpValue);

            }
            if (lerpValue > 1)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
            /*
        if (beatsSinceSpawned < 0)
        {
            newPosition.y = -10;
        }
        else
        {
            newPosition.x = Mathf.Lerp(spawnPosition.x, 0, AudioManager.singleton.currentBeat);
        }
            */

        transform.SetPositionAndRotation(newPosition, transform.rotation);
    }
}
