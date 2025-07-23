using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform direction;
    public bool isMoving = true;
    void Update()
    {
        if (isMoving)
        {
            transform.Translate(direction.forward * (moveSpeed * Time.deltaTime));
        }
    }
}
