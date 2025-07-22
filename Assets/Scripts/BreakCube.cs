using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCube : MonoBehaviour, IBreak
{
    [SerializeField] private Rigidbody rb;
    private float force = 100f;
    public void Break(Side side)
    {
        if (side == Side.Left)
        {
            rb.AddForce(Vector3.left * force);
        }

        if (side == Side.Right)
        {
            rb.AddForce(Vector3.right * force);
        }

        if (side == Side.Up)
        {
            rb.AddForce(Vector3.up * force);
        }

        if (side == Side.Down)
        {
            rb.AddForce(Vector3.down * force);
        }
    }
}
