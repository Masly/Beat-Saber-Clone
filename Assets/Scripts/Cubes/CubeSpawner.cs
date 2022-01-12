using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * JUST FOR TESTING! 
 */
public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject cubePrefab;
    [SerializeField]
    private float spawnInterval = 5;
    
    float timer =0;
    int cubeCounter = 0;
    private void Start()
    {
        //this needs to be activated by TrackService
        gameObject.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnInterval)
        {
            GameObject cube =Instantiate(cubePrefab,transform.position,Quaternion.identity);
            CubeMover cubeMover;
            if(cube.TryGetComponent<CubeMover>(out cubeMover))
            {
                cubeMover.beatSpawned = cubeCounter * (int) spawnInterval;
                cubeCounter++;
            }
            
            timer = 0;
        }
    }
}
