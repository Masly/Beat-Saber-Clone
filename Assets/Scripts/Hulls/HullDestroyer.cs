using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullDestroyer : MonoBehaviour
{
    [SerializeField]
    private float timeAlive= 3;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeAlive);
    }

}
