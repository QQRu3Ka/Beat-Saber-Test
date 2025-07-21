using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    void Update()
    {
        transform.Translate(new Vector3(0,0,-10) * Time.deltaTime);
    }
}
