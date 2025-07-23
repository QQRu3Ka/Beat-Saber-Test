using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BreakCube : MonoBehaviour, IBreak
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Rigidbody leftPart;
    [SerializeField] private Rigidbody rightPart;
    private float _force = 500f;
    public void Break(Side side)
    {
        rb.useGravity = true;
        leftPart.useGravity = true;
        rightPart.useGravity = true;
        if (side == Side.Left)
        {
            leftPart.AddForce(new Vector3(-1, 1, -0.5f) *  _force);
            rightPart.AddForce(new Vector3(-1, -1, -0.5f) *  _force);
            //rb.AddForce(new Vector3(-1, 0, -0.5f) * _force);
        }

        if (side == Side.Right)
        {
            leftPart.AddForce(new Vector3(1, 1, -0.5f) *  _force);
            rightPart.AddForce(new Vector3(1, -1, -0.5f) *  _force);
            //rb.AddForce(new Vector3(1, 0, -0.5f) * _force);
        }

        if (side == Side.Up)
        {
            Debug.Log("Ломаю вверх");
            leftPart.AddForce(new Vector3(-0.5f, 1.5f, -0.5f) *  _force);
            rightPart.AddForce(new Vector3(0.5f, 1.5f, -0.5f) *  _force);
            //rb.AddForce(new Vector3(0, 1, -0.5f) * _force);
        }

        if (side == Side.Down)
        {
            leftPart.AddForce(new Vector3(0.5f, -1.5f, -0.5f) *  _force);
            rightPart.AddForce(new Vector3(-0.5f, -1.5f, -0.5f) *  _force);
            //rb.AddForce(new Vector3(0, -1, -0.5f) * _force);
        }

        int[] array = new int[5] ;

        var sort = array.Select(i => i).OrderBy(i => i);

    }
}
