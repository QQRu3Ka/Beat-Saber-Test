using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    [SerializeField] private int _score = 0;
    
    public delegate void SliceHandler();
    public event SliceHandler OnSlice;

    private void Awake()
    {
        OnSlice += PrintScore;
    }

    public void ChangePoint(int num)
    {
        _score+=num;
        OnSlice?.Invoke();
    }

    public void PrintScore()
    {
        Debug.Log("+ очко");
    }
}
