using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _plus;
    
    public static GameManager Instance {get; private set;}
    private GameStats _gameStats;

    public GameController GameController { get; private set; }


    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _gameStats = GetComponent<GameStats>();
        GameController = GetComponent<GameController>();
    }

    

    public void RightCut(int points)
    {
        print("Попал");
        _gameStats.RightCut(points);
    }

    public void WrongCut()
    {
        print("Не попал");
        _gameStats.WrongCut();
    }
}
