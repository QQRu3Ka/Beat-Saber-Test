using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private SpawnCube _spawnCube;

    private void Awake()
    {
        _spawnCube = GetComponent<SpawnCube>();
    }

    private void Start()
    {
        _audioSource.clip = _spawnCube.SpawnerManager.Song;
        Time.timeScale = 0;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 40), "Начать"))
        {
            if (!_audioSource.isPlaying)
                _audioSource.PlayScheduled(AudioSettings.dspTime + _spawnCube.SpawnerManager.Delay);
            Time.timeScale = 1;
        }
        
        if (GUI.Button(new Rect(10, 60, 150, 40), "Пауза"))
        {
            Time.timeScale = 0;
            if (_audioSource.isPlaying)
                _audioSource.Pause();
            
        }
        
        if (GUI.Button(new Rect(10, 110, 150, 40), "Перезапуск"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
