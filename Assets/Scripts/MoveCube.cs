using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform direction;
    public bool isMoving = true;
    
    [SerializeField] private Rigidbody leftPart;
    [SerializeField] private Rigidbody rightPart;

    private IBreak _breakCube;

    private void Awake()
    {
        _breakCube = GetComponent<BreakCube>();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(direction.forward * (moveSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            isMoving = false;
            leftPart.useGravity = true;
            rightPart.useGravity = true;
            Debug.Log(other.transform.parent.gameObject.GetComponent<SwordHit>().side);
            _breakCube.Break(other.transform.parent.gameObject.GetComponent<SwordHit>().side);
        }
    }
}
