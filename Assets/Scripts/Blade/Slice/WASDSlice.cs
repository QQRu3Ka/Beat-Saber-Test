using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDSlice : MonoBehaviour, ISlice
{
    public Side Slice()
    {
        if (Input.GetKey(KeyCode.W))
        {
            return Side.Up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            return Side.Left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            return Side.Down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            return Side.Right;
        }

        return Side.None;
    }
}
