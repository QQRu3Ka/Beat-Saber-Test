using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCube : MonoBehaviour, IBreak
{
    [SerializeField] private Rigidbody rb;
    private float force = 500f;
    public void Break(Side side)
    {
        rb.useGravity = true;
        if (side == Side.Left)
        {
            rb.AddForce(new Vector3(-1, 0, -0.5f) * force);
        }

        if (side == Side.Right)
        {
            rb.AddForce(new Vector3(1, 0, -0.5f) * force);
        }

        if (side == Side.Up)
        {
            rb.AddForce(new Vector3(0, 1, -0.5f) * force);
        }

        if (side == Side.Down)
        {
            rb.AddForce(new Vector3(0, -1, -0.5f) * force);
        }
    }
}
