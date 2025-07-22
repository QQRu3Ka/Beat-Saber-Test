using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform direction;
    void Update()
    {
        transform.Translate(direction.forward * (moveSpeed * Time.deltaTime));
    }
}
