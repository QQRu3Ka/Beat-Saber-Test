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
    private Dictionary<Side, List<Vector3>> _dict;
    private Dictionary<Side, Vector3> _cutsDict;
    private CubeStats _cubeStats;
    
    private void Awake()
    {
        _cubeStats = gameObject.GetComponent<CubeStats>();
        _dict = new Dictionary<Side, List<Vector3>>();
        foreach (var i in _cuts)
        {
            _dict[i.Side] = new List<Vector3>(){i.LeftPartForce, i.RightPartForce};
        }
        _cutsDict = new Dictionary<Side, Vector3>();
        foreach (var i in _sides)
        {
            _cutsDict[i.Side] = i.Vector;
        }
    }
    public void Break(Side side, Vector3 point, GameObject sword)
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
        
        var hull = gameObject.Slice(gameObject.transform.position, transform.position - point + _cutsDict[side]);

        if (hull == null) return;
        var upperHull = hull.CreateUpperHull(gameObject, gameObject.GetComponent<MeshRenderer>().material);
        var lowerHull = hull.CreateLowerHull(gameObject, gameObject.GetComponent<MeshRenderer>().material);
            
        upperHull.AddComponent<MeshCollider>().convex = true;
        lowerHull.AddComponent<MeshCollider>().convex = true;
            
        upperHull.AddComponent<Rigidbody>();
        lowerHull.AddComponent<Rigidbody>();
            
        upperHull.GetComponent<Rigidbody>().AddForce(_dict[side][0] * _force);
        lowerHull.GetComponent<Rigidbody>().AddForce(_dict[side][1] * _force);
        
        gameObject.transform.position = new Vector3(0f, 1000f, 0f);
        StartCoroutine(DeleteHulls(upperHull, lowerHull));
        
    }

    private IEnumerator DeleteHulls(GameObject upperHull, GameObject lowerHull)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(upperHull);
        Destroy(lowerHull);
        Destroy(gameObject);
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
