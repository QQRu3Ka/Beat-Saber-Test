using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    [SerializeField] private int _score;
    
    
    private int _combo = 1;
    private int _comboIncreaser;
    private StatsUI _statsUI;

    private void Awake()
    {
        _statsUI = GetComponent<StatsUI>();
    }
    public void RightCut(int points)
    {
        _score += points * _combo;
        _statsUI.ScoreChange(_score);

        if (_comboIncreaser < 10)
        {
            _comboIncreaser++;
            _statsUI.SliderChange(_comboIncreaser);
        }
        if (_comboIncreaser == 10 && _combo != 8)
        {
            _combo *= 2;
            _statsUI.ComboChange(_combo);
            _comboIncreaser = 0;
        }
    }

    public void WrongCut()
    {
        if (_combo != 1)
        {
            _combo /= 2;
        }
        _comboIncreaser = 0;
        _statsUI.ComboChange(_combo);
        _statsUI.SliderChange(_comboIncreaser);
    }
}
