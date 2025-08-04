using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCube : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.TryGetComponent(out MoveCube moveCube))
        {
            print("Куб пролетел");
        }
    }
}
