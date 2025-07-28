using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    [SerializeField] private int _score = 0;

    public void PrintScore()
    {
        Debug.Log("+ очко");
    }

    public void RightCut()
    {
        _score += 1;
        Debug.Log("Попал");
    }

    public void WrongCut()
    {
        _score -= 1;
        Debug.Log("Не попал");
    }
}
