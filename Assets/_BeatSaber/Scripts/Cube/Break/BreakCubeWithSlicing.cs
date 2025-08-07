using System;
using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;

public class BreakCubeWithSlicing : MonoBehaviour, IBreak
{
    [SerializeField] private float _force = 500f;
    [SerializeField] private List<SideCutData> _cuts;
    [SerializeField] private List<SideVectorData> _sides;
    [SerializeField] private GameObject[] _particles;
    [SerializeField] private GameObject _cubeObject;
    private Dictionary<Side, List<Vector3>> _dict;
    private Dictionary<Side, Vector3> _cutsDict;
    private CubeStats _cubeStats;
    
    private void Awake()
    {
        _cubeStats = GetComponent<CubeStats>();
        _dict = new Dictionary<Side, List<Vector3>>();
        foreach (var i in _cuts)
        {
            _dict[i.Side] = new List<Vector3>{i.LeftPartForce, i.RightPartForce};
        }
        _cutsDict = new Dictionary<Side, Vector3>();
        foreach (var i in _sides)
        {
            _cutsDict[i.Side] = i.Vector;
        }
    }
    public void Break(Side side, Vector3 point, GameObject sword)
    {
        var particle = Instantiate(_particles[_cubeStats.Color == GameColor.BLUE ? 0 : 1], transform.position, Quaternion.identity);
        Destroy(particle, 1f);
        CutCheck(side, point, sword);
        BreakCube(side, point);
    }

    private void CutCheck(Side side, Vector3 point, GameObject sword)
    {
        if ((side == _cubeStats.Side || _cubeStats.Side == Side.Any) && 
            transform.gameObject.GetComponent<ColorTag>().Color == sword.GetComponent<ColorTag>().Color)
        {
            if (side is Side.Left or Side.Right)
            {
                GameManager.Instance.RightCut((int)((0.5-Math.Abs(point.y - transform.position.y)) * 30));
            }
            else
            {
                GameManager.Instance.RightCut((int)((0.5-Math.Abs(point.x - transform.position.x)) * 30));
            }
        }
        else GameManager.Instance.WrongCut();
    }

    private void BreakCube(Side side, Vector3 point)
    {
        var hull = gameObject.Slice(gameObject.transform.position, gameObject.transform.position - point + _cutsDict[side]);
        //var hull = _cubeObject.Slice(_cubeObject.transform.position, Vector3.left);
        if (hull == null)
        {
            print(_cubeObject.transform.position);
            print(point);
            return;
        }
        var upperHull = hull.CreateUpperHull(gameObject, GetComponent<MeshRenderer>().material);
        var lowerHull = hull.CreateLowerHull(gameObject, GetComponent<MeshRenderer>().material);
            
        AddComponents(upperHull);
        AddComponents(lowerHull);
            
        upperHull.GetComponent<Rigidbody>().AddForce(_dict[side][0] * _force);
        lowerHull.GetComponent<Rigidbody>().AddForce(_dict[side][1] * _force);
        
        Destroy(upperHull, 0.5f);
        Destroy(lowerHull, 0.5f);
        Destroy(gameObject);
    }

    private void AddComponents(GameObject hull)
    {
        hull.transform.position = transform.position;
        hull.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        hull.AddComponent<MeshCollider>().convex = true;
        hull.AddComponent<Rigidbody>();
        hull.layer = LayerMask.NameToLayer("Debris");
    }
    
    [Serializable]
    private class SideCutData
    {
        [field:SerializeField] public Side Side { get; set; }
        [field:SerializeField] public Vector3 LeftPartForce {get; set;}
        [field:SerializeField] public Vector3 RightPartForce {get; set;}
    }

    [Serializable]
    private class SideVectorData
    {
        [field: SerializeField] public Side Side { get; set; }
        [field: SerializeField] public Vector3 Vector { get; set; }
    }
    
    //Left  - -1      1 -0.5 |   -1   -1 -0.5
    //Right -  1      1 -0.5 |    1   -1 -0.5
    //Up    - -0.5  1.5 -0.5 |  0.5  1.5 -0.5
    //Down  -  0.5 -1.5 -0.5 | -0.5 -1.5 -0.5
}
