using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardSlice : MonoBehaviour
{
    public Side SliceRed()
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

    public Side SliceBlue()
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
