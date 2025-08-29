using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private SpawnCube _spawnCube;
    public double StartTime {get; private set;}
    public bool IsPlaying {get; set;}

    [field:SerializeField] public AudioSource AudioSource { get; set; }

    private void Awake()
    {
        _spawnCube = GetComponent<SpawnCube>();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 40), "Начать"))
        {
            Play();
        }
        
        if (GUI.Button(new Rect(10, 60, 150, 40), "Пауза"))
        {
            Pause();
        }
        
        if (GUI.Button(new Rect(10, 110, 150, 40), "Перезапуск"))
        {
            Restart();
        }
    }

    public void Play()
    {
        if (!AudioSource.isPlaying)
        {
            StartTime = AudioSettings.dspTime + _spawnCube.SpawnerManager.Delay;
            IsPlaying = true;
            AudioSource.PlayScheduled(StartTime);
        }
    }

    public void Pause()
    {
        if (AudioSource.isPlaying)
            AudioSource.Pause();
    }

    public void Continue()
    {
        if (!AudioSource.isPlaying)
            AudioSource.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
