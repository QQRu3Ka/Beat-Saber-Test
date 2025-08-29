using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    public bool IsHitRight {get; private set;}

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out VRSlicer slicer))
        {
            IsHitRight = true;
        }
    }
}
