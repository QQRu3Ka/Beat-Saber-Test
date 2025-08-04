using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    [SerializeField] private int _score = 0;

    public void RightCut(int points)
    {
        _score += points;
        Debug.Log("Попал");
    }

    public void WrongCut()
    {
        Debug.Log("Не попал");
    }
}
