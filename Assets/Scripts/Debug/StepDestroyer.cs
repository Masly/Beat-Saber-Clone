using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepDestroyer : MonoBehaviour
{

    [SerializeField]
    private float secondsToLive = 5;
    private float timeAlive = 0;
    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= secondsToLive)
            Destroy(gameObject);
    }
}
