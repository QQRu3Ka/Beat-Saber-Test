using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 40), "Начать"))
        {
            if (!_audioSource.isPlaying)
                _audioSource.PlayScheduled(AudioSettings.dspTime + 1.25);
            Time.timeScale = 1;
        }
        
        if (GUI.Button(new Rect(10, 60, 150, 40), "Пауза"))
        {
            if (_audioSource.isPlaying)
                _audioSource.Pause();
            Time.timeScale = 0;
        }
        
        if (GUI.Button(new Rect(10, 110, 150, 40), "Перезапуск"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    private IEnumerator StartPlay()
    {
        yield return new WaitForSeconds(0.3f);
        _audioSource.Play();
    }
}
