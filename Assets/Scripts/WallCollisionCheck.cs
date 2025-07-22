using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisionCheck : MonoBehaviour
{
    const string CAM_TAG = "MainCamera";
    public IBreak cube;
    public ISlice slice;
    public GameObject cam;
    public Side side;

    private void Awake()
    {
        cube = GetComponent<BreakCube>();
        slice = GetComponent<KeyboardSlice>();
        cam = GameObject.FindGameObjectWithTag(CAM_TAG);
        if (transform.rotation.eulerAngles.z == 0) side = Side.Up;
        if (transform.rotation.eulerAngles.z == 90) side = Side.Right;
        if (transform.rotation.eulerAngles.z == 180) side = Side.Down;
        if (transform.rotation.eulerAngles.z == 270) side = Side.Left;
    }

    private void OnTriggerEnter(Collider other)
    {
        Side s = Side.None;
        Debug.Log(side.ToString());
        if (!other.CompareTag("Wall")) return;
        if (gameObject.CompareTag("RedCube"))
        {
            s = slice.SliceRed();
        }
        
        if (gameObject.CompareTag("BlueCube"))
        {
            s = slice.SliceBlue();
        }
        
        if (s == side)
        {
            cube.Break(side);
            cam.GetComponent<GameStats>().ChangePoint(1);
        }
        else cam.GetComponent<GameStats>().ChangePoint(-1);
    }
}
