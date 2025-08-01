using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    private GameStats _gameStats;

    private void Awake()
    {
        Instance = this;
        _gameStats = GetComponent<GameStats>();
    }

    public void RightCut(int points)
    {
        _gameStats.RightCut(points);
    }

    public void WrongCut()
    {
        _gameStats.WrongCut();
    }
}
