using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearCube : MonoBehaviour
{
    public void Break(Side side)
    {
        Destroy(gameObject);
    }
}
