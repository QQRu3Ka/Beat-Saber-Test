using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSlice : MonoBehaviour, ISlice
{
    public Side Slice()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            return Side.Up;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            return Side.Left;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            return Side.Down;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            return Side.Right;
        }
        return Side.None;
    }
}
