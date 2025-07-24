using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollisionCheck : MonoBehaviour
{
    const string CamTag = "MainCamera";
    private MoveCube MoveCube;
    private IBreak _cube;
    private ISlice _slice;
    public GameObject cam;
    public Side side;

    private void Awake()
    {
        MoveCube = GetComponent<MoveCube>();
        _cube = GetComponent<BreakCube>();
        _slice = GetComponent<KeyboardSlice>();
        cam = GameObject.FindGameObjectWithTag(CamTag);
        if (transform.rotation.eulerAngles.z == 0) side = Side.Up;
        if (transform.rotation.eulerAngles.z == 90) side = Side.Right;
        if (transform.rotation.eulerAngles.z == 180) side = Side.Down;
        if (transform.rotation.eulerAngles.z == 270) side = Side.Left;
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (!other.CompareTag("Wall")) return;
    //     Side s = Side.None;
    //     Debug.Log("Столкновение со стеной");
    //     if (gameObject.CompareTag("RedCube"))
    //     {
    //         s = _slice.SliceRed();
    //         
    //     }
    //     
    //     if (gameObject.CompareTag("BlueCube"))
    //     {
    //         s = _slice.SliceBlue();
    //     }
    //
    //     if (s != Side.None)
    //     {
    //         MoveCube.isMoving = false;
    //         _cube.Break(s);
    //     }
    //     
    //
    //     if (s == side)
    //     {
    //         cam.GetComponent<GameStats>().ChangePoint(1);
    //     }
    //     else cam.GetComponent<GameStats>().ChangePoint(-1);
    // }
}
