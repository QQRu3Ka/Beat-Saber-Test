using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearCube : MonoBehaviour, IBreak
{
    public void Break(Side side)
    {
        Destroy(gameObject);
    }
}
