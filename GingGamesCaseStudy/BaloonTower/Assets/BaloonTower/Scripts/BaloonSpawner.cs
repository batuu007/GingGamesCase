using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonSpawner : MonoBehaviour
{
    public GameObject baloon;
    void Start()
    {
        InvokeRepeating("Spawn", 2f, 2f);
    }

    void Spawn()
    {
        Instantiate(baloon,new Vector3(0f,0.85f,-0.33f),gameObject.transform.rotation);
    }
}
