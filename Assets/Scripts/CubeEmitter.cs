using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeEmitter : MonoBehaviour
{
    public GameObject cubePrefab;
    private void Update()
    {
        if(Input.GetButtonDown("Spawn")){
            Instantiate(cubePrefab, transform.position, transform.rotation);
        }
    }
}
