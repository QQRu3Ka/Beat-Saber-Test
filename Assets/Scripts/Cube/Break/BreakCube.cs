using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BreakCube : MonoBehaviour, IBreak
{
    [SerializeField] private Rigidbody _leftPart;
    [SerializeField] private Rigidbody _rightPart;
    [SerializeField] private float _force = 500f;
    [SerializeField] private List<SideCutData> _cuts;
    private Dictionary<Side, List<Vector3>> _dict;
    //Создать List<SideCutData>, потом в Awake сделать из List -> Dictionary

    private void Awake()
    {
        _dict = new Dictionary<Side, List<Vector3>>();
        foreach (var i in _cuts)
        {
            _dict[i.Side] = new List<Vector3>(){i.LeftPartForce, i.RightPartForce};
        }
    }
    public void Break(Side side)
    {
        _leftPart.useGravity = true;
        _rightPart.useGravity = true;
        _leftPart.AddForce(_dict[side][0] * _force);
        _rightPart.AddForce(_dict[side][1] * _force);
        // switch (side)
        // {
        //     case Side.Left:
        //         _leftPart.AddForce(new Vector3(-1, 1, -0.5f) *  _force);
        //         _rightPart.AddForce(new Vector3(-1, -1, -0.5f) *  _force);
        //         break;
        //     case Side.Right:
        //         _leftPart.AddForce(new Vector3(1, 1, -0.5f) *  _force);
        //         _rightPart.AddForce(new Vector3(1, -1, -0.5f) *  _force);
        //         break;
        //     case Side.Up:
        //         _leftPart.AddForce(new Vector3(-0.5f, 1.5f, -0.5f) *  _force);
        //         _rightPart.AddForce(new Vector3(0.5f, 1.5f, -0.5f) *  _force);
        //         break;
        //     case Side.Down:
        //         _leftPart.AddForce(new Vector3(0.5f, -1.5f, -0.5f) *  _force);
        //         _rightPart.AddForce(new Vector3(-0.5f, -1.5f, -0.5f) *  _force);
        //         break;
        // }
    }
    
    [Serializable]
    private class SideCutData
    {
        [SerializeField] private Side _side;
        [SerializeField] private Vector3 _leftPartForce;
        [SerializeField] private Vector3 _rightPartForce;

        public Side Side
        {
            get => _side;
            set => _side = value;
        }

        public Vector3 LeftPartForce
        {
            get => _leftPartForce;
            set => _leftPartForce = value;
        }

        public Vector3 RightPartForce
        {
            get => _rightPartForce;
            set => _rightPartForce = value;
        }
    }
}
