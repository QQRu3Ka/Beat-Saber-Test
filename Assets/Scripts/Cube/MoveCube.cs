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
    private CubeStats _cubeStats;

    private void Awake()
    {
        _breakCube = GetComponent<IBreak>();
        _setup = GetComponent<SetupCube>();
        _cubeStats = GetComponent<CubeStats>();
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
        var collisionNormal = _direction.position - point;
        _breakCube.Break(hitSide, collisionNormal);
        // if ((hitSide == _cubeStats.Side || _cubeStats.Side == Side.Any) && 
        //     transform.gameObject.GetComponent<ColorTag>().Color == other.gameObject.GetComponent<ColorTag>().Color)
        // {
        //     GameManager.Instance.RightCut();
        // }
        // else GameManager.Instance.WrongCut();
        _isSliced = true;
    }
}
