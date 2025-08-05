using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * (Time.deltaTime * 10));
    }
}
