using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    
    [SerializeField] private Transform _direction;
    [SerializeField] private GameObject _hitCheckObject;
    
    private bool _isMoving = true;
    private bool _isSliced;
    private IBreak _breakCube;
    private HitCheck _hitCheck;

    public float MoveSpeed { get; set; }

    private void Awake()
    {
        _hitCheck = _hitCheckObject.GetComponent<HitCheck>();
        _breakCube = GetComponent<IBreak>();
    }

    private void Update()
    {
        if (_isMoving)
        {
            transform.Translate(_direction.forward * (MoveSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.gameObject.TryGetComponent(out VRSlicer slice) || _isSliced) return;
        _isMoving = false;
        var preHitAngle = Vector3.Angle(slice.PrevSecondPosition - slice.StartSwordPosition, Vector3.forward);
        var plane = slice.CutPlane;
        var point = other.ClosestPoint(_direction.position);
        _breakCube.Break(Side.None, point, Vector3.zero, plane, other.gameObject);
        _breakCube.CheckSide(_hitCheck.IsHitRight, point, preHitAngle,  gameObject, other.gameObject);
        _isSliced = true;
    }

    private IEnumerator FollowSlice(Vector3 point, float preHitAngle, GameObject sword, VRSlicer slice)
    {
        yield return new WaitForSeconds(0.2f);
        
        var afterHitAngle = Vector3.Angle(slice.gameObject.transform.position - slice.StartSwordPosition, Vector3.forward);
        
        
    }
}
