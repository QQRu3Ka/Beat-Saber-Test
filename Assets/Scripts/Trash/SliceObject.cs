using System.Collections;
using System.Collections.Generic;
using EzySlice;
using UnityEngine;
using Plane = EzySlice.Plane;

public class SliceObject : MonoBehaviour
{
    [SerializeField] private GameObject _sliceableObject;
    [SerializeField] private Material _crossSectionMat;
    private void Start()
    {
        StartCoroutine(DoSlice());
    }

    private IEnumerator DoSlice()
    {
        yield return new WaitForSeconds(2f);
        var hull = _sliceableObject.Slice(_sliceableObject.transform.position, new Vector3(0, 1, 0.5f));
        
        if (hull == null) yield break;
        var upperHull = hull.CreateUpperHull(_sliceableObject, _crossSectionMat);
        var lowerHull = hull.CreateLowerHull(_sliceableObject, _crossSectionMat);
        
        upperHull.AddComponent<MeshCollider>().convex = true;
        lowerHull.AddComponent<MeshCollider>().convex = true;
        
        upperHull.AddComponent<Rigidbody>().useGravity = false;
        lowerHull.AddComponent<Rigidbody>().useGravity = false;
        
        upperHull.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0.5f) * 100);
        lowerHull.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, -0.5f) * 100);
            
        Destroy(_sliceableObject);
    }
}
