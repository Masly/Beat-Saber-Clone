using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * JUST FOR TESTING! 
 */
public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public float spawnInterval = 5;
    public Transform destroyPosition;
    float timer =0;
    int cubeCounter = 0;
    private void Start()
    {
        //this needs to be activated by AudioManager
        gameObject.SetActive(false);
    }

    // Update is called once per frame
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
