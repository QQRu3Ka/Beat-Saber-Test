using System;
using System.Collections;
using System.Collections.Generic;
using EzySlice;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Timeline;
using Plane = UnityEngine.Plane;

public class VRBreakCube : MonoBehaviour, IBreak
{
    [SerializeField] private float _force = 500f;
    [SerializeField] private List<ColorParticleData> _particleData;
    private Dictionary<GameColor, GameObject> _colorParticleDict;
    private CubeStats _cubeStats;
    
    private const int MaxSlicePoints = 15;
    private const int MaxFollowPoints = 70;
    private const int MaxFollowAngle = 100;
    
    private void Awake()
    {
        _colorParticleDict = new Dictionary<GameColor, GameObject>();
        foreach (var i in _particleData)
        {
            _colorParticleDict[i.Color] = i.Particle;
        }
        _cubeStats = GetComponent<CubeStats>();
    }
    public void Break(Side side, Vector3 point, Vector3 normal, Plane plane, GameObject sword)
    {
        var particle = Instantiate(_colorParticleDict[_cubeStats.Color], transform.position, Quaternion.identity);
        Destroy(particle, 1f);
        BreakCube(side, point, plane);
    }

    public void CheckSide(bool isRightSide, Vector3 point, float preHitAngle, GameObject cube, GameObject sword)
    {
        var hitSide = SideCalculator(point);
        
        if (isRightSide && cube.GetComponent<ColorTag>().Color == sword.GetComponent<ColorTag>().Color)
        {
            GameManager.Instance.RightCut(PointCalculator(hitSide, point, preHitAngle));
        }
        else
        {
            GameManager.Instance.WrongCut();
        }
    }

    private Side SideCalculator(Vector3 point)
    {
        var localHitPoint = transform.InverseTransformPoint(point).normalized;
        
        var absX = Mathf.Abs(localHitPoint.x);
        var absY = Mathf.Abs(localHitPoint.y);
        
        if (absX > absY)
        {
            return localHitPoint.x > 0 ? Side.Right : Side.Left;
        }
        return localHitPoint.y > 0 ? Side.Up : Side.Down;
    }
    
    private int PointCalculator(Side side, Vector3 point, float preHitAngle)
    {
        if (side is Side.Left or Side.Right)
        {
            return (int)Mathf.Min((0.5f - Math.Abs(point.y - transform.position.y)) * (MaxSlicePoints*2), MaxSlicePoints) 
                   + (int)Mathf.Min(MaxFollowPoints, preHitAngle*MaxFollowPoints/MaxFollowAngle);
        }

         
        return (int)Mathf.Min((0.5f - Math.Abs(point.x - transform.position.x)) * (MaxSlicePoints*2), MaxSlicePoints) 
               + (int)Mathf.Min(MaxFollowPoints, preHitAngle*MaxFollowPoints/MaxFollowAngle);
    }
    
    private void BreakCube(Side side, Vector3 point, Plane plane)
    {
        var hull = gameObject.Slice(point, plane.normal);
        if (hull == null)
        {
            hull = gameObject.Slice(point, transform.position - point);
        }
        var upperHull = hull.CreateUpperHull(gameObject, GetComponent<MeshRenderer>().material);
        var lowerHull = hull.CreateLowerHull(gameObject, GetComponent<MeshRenderer>().material);
            
        AddComponents(upperHull);
        AddComponents(lowerHull);
            
        upperHull.GetComponent<Rigidbody>().AddForce((plane.normal + Vector3.forward) * _force);
        lowerHull.GetComponent<Rigidbody>().AddForce((-plane.normal + Vector3.forward) * _force);
        
        Destroy(upperHull, 10f);
        Destroy(lowerHull, 10f);
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
    public class ColorParticleData
    {
        [field: SerializeField] public GameColor Color { get; set; }
        [field: SerializeField] public GameObject Particle { get; set; }
    }
}
