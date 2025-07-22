using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class WallCollisionCheck : MonoBehaviour
{
    public IBreak cube;
    public ISlice slice;
    public Transform cam;

    private void Awake()
    {
        cube = GetComponent<BreakCube>();
        slice = GetComponent<KeyboardSlice>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!other.CompareTag("Wall")) return;
        if (gameObject.CompareTag("RedCube"))
        {
            RedSliceCheck();
        }
        
        if (gameObject.CompareTag("BlueCube"))
        {
            BlueSliceCheck();
        }
    }
    
    private void RedSliceCheck()
    {
        Side s = slice.SliceRed();
        if (gameObject.transform.rotation.eulerAngles.z == 0 && s == Side.Up)
        {
            cube.Break(Side.Up);
        }
        if (gameObject.transform.rotation.eulerAngles.z == 90 && s == Side.Right)
        {
            cube.Break(Side.Right);
        }
        if (gameObject.transform.rotation.eulerAngles.z == 180 && s == Side.Down)
        {
            cube.Break(Side.Down);
        }
        if (gameObject.transform.rotation.eulerAngles.z == 270 && s == Side.Left)
        {
            cube.Break(Side.Left);
        }
    }
    
    private void BlueSliceCheck()
    {
        Side s = slice.SliceBlue();
        if (gameObject.transform.rotation.eulerAngles.z == 0 && s == Side.Up)
        {
            cube.Break(Side.Up);
        }
        if (gameObject.transform.rotation.eulerAngles.z == 90 && s == Side.Right)
        {
            cube.Break(Side.Right);
        }
        if (gameObject.transform.rotation.eulerAngles.z == 180 && s == Side.Down)
        {
            cube.Break(Side.Down);
        }
        if (gameObject.transform.rotation.eulerAngles.z == 270 && s == Side.Left)
        {
            cube.Break(Side.Left);
        }
    }
}
