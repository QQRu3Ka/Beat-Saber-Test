using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    private void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Коллизия");
    }
}
