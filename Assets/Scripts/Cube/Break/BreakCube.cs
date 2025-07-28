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
    }
    
    [Serializable]
    private class SideCutData
    {
        [field:SerializeField] public Side Side { get; set; }
        [field:SerializeField] public Vector3 LeftPartForce {get; set;}
        [field:SerializeField] public Vector3 RightPartForce {get; set;}
    }
    
    //Left  - -1      1 -0.5 |   -1   -1 -0.5
    //Right -  1      1 -0.5 |    1   -1 -0.5
    //Up    - -0.5  1.5 -0.5 |  0.5  1.5 -0.5
    //Down  -  0.5 -1.5 -0.5 | -0.5 -1.5 -0.5
}
