using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    private const string CamTag = "MainCamera";

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _direction;

    private bool _isMoving = true;
    private bool _isHitted = false;
    private IBreak _breakCube;
    [SerializeField] private Side _side;
    private GameObject _cam;

    private void Awake()
    {
        _breakCube = GetComponent<IBreak>();
        _cam = GameObject.FindGameObjectWithTag(CamTag);
        var sideNum = (int)transform.rotation.eulerAngles.z / 90;
        _side = (Side)sideNum;
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
        //Проверять компонент вместо проверки тега
        if (!other.CompareTag("Sword")) return;
        _isMoving = false;
        var hitSide = other.transform.parent.gameObject.GetComponent<SwordHit>().Side;
        Debug.Log(hitSide);
        _breakCube.Break(hitSide);
        if (_isHitted) return;
        //Поменять теги на enum
        if (hitSide == _side && (transform.gameObject.CompareTag("RedCube") && other.transform.parent.gameObject.CompareTag("RedSword") ||
            transform.gameObject.CompareTag("BlueCube") && other.transform.parent.gameObject.CompareTag("BlueSword")))
        {
            _cam.GetComponent<GameStats>().RightCut();
        }
        else _cam.GetComponent<GameStats>().WrongCut();
        _isHitted = true;
    }
}
