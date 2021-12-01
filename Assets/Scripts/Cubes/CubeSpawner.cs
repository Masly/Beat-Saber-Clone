using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public float spawnInterval = 5;
    public Transform spawnPosition;
    public Transform destroyPosition;
    float timer =0;
    //this is horrible but it's just for testing
    int cubeCounter = 0;

    // Update is called once per frame
    void Update()
    {
        //this timer will get unreliable pretty fast, but it's just for testing
        timer += Time.deltaTime;
        if(timer >= spawnInterval)
        {
            GameObject cube =Instantiate(cubePrefab,transform.position,Quaternion.identity);
            CubeMover cubeMover;
            if(cube.TryGetComponent<CubeMover>(out cubeMover))
            {
                cubeMover.beatSpawned = cubeCounter * (int) spawnInterval;
                cubeMover.spawnPosition = spawnPosition.position;
                cubeMover.destroyPosition = destroyPosition.position;
                cubeCounter++;
            }
            
            timer = 0;
        }
    }
}
