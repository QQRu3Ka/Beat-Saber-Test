using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _direction;

    private SetupCube _setup;
    private bool _isMoving = true;
    private bool _isSliced;
    private IBreak _breakCube;

    private void Awake()
    {
        _breakCube = GetComponent<IBreak>();
        _setup = GetComponent<SetupCube>();
    }

    private void Update()
    {
        if (_isMoving)
        {
            transform.Translate(_direction.forward * (_moveSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.gameObject.TryGetComponent(out BladeHit sh) || _isSliced) return;
        _isMoving = false;
        var hitSide = sh.Side;
        var point = other.ClosestPoint(_direction.position);
        _breakCube.Break(hitSide, point, other.gameObject);
        _isSliced = true;
    }
}
