using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Menu : MonoBehaviour
{
    [SerializeField] private MusicPackManager _musicPackManager;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _packPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _mapPanel;
    [SerializeField] private GameObject _comboPanel;
    [SerializeField] private GameObject _packButtonPrefab;
    [SerializeField] private GameObject _mapButtonPrefab;

    [SerializeField] private GameObject _leftController;
    [SerializeField] private GameObject _rightController;
    
    [SerializeField] private SpawnCube _spawnCube;
    [SerializeField] private GameController _gameController;
    public bool IsPaused { get; set; }
    
    private void Start()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 5f).SetEase(Ease.OutBack);
        LoadMusicPacks();
    }

    private void LoadMusicPacks()
    {
        ClearContainer(_packPanel);

        for (var i = 0; i < _musicPackManager.MusicPacks.Count; i++)
        {
            var index = i;
            var button = Instantiate(_packButtonPrefab, _packPanel.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = _musicPackManager.MusicPacks[i].Name;
            button.GetComponent<Button>().onClick.AddListener(() => { ShowMaps(index); });
        }
        
        _packPanel.SetActive(false);
    }

    private void ShowMaps(int packIndex)
    {
        _mapPanel.SetActive(true);
        ClearContainer(_mapPanel);

        var selectedPack = _musicPackManager.MusicPacks[packIndex];

        foreach (var pack in selectedPack.MusicPack)
        {
            var button = Instantiate(_mapButtonPrefab, _mapPanel.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = pack.Name;
            button.GetComponent<Button>().onClick.AddListener(() => {StartMap(pack);});
        }
    }

    private void StartMap(SpawnerManager map)
    {
        _startPanel.SetActive(false);
        _packPanel.SetActive(false);
        _mapPanel.SetActive(false);
        _comboPanel.SetActive(true);
        
        _leftController.GetComponent<XRRayInteractor>().enabled = false;
        _rightController.GetComponent<XRRayInteractor>().enabled = false;
        
        _spawnCube.SpawnerManager = map;
        _gameController.AudioSource.clip = map.Song;
        _gameController.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _gameController.IsPlaying)
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        _startPanel.SetActive(false);
        _packPanel.SetActive(true);
    }

    private void ClearContainer(GameObject container)
    {
        foreach (GameObject child in container.transform)
        {
            Destroy(child);
        }
    }

    public void PauseGame()
    {
        IsPaused = true;
        _gameController.Pause();
        _gameController.IsPlaying = false;
        _leftController.GetComponent<XRRayInteractor>().enabled = true;
        _rightController.GetComponent<XRRayInteractor>().enabled = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
        _pausePanel.SetActive(true);
    }
    
    public void ContinueGame()
    {
        IsPaused = false;
        _gameController.Continue();
        _gameController.IsPlaying = true;
        _leftController.GetComponent<XRRayInteractor>().enabled = false;
        _rightController.GetComponent<XRRayInteractor>().enabled = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        _pausePanel.SetActive(false);
    }
    
    public void RestartGame()
    {
        ContinueGame();
        _gameController.AudioSource.Stop();
        foreach (var cube in FindObjectsByType<SetupCube>(FindObjectsSortMode.None).Select(c => c.gameObject).ToList())
        {
            Destroy(cube);
        }
        
        StopCoroutine(_spawnCube.StartSong());
        StartCoroutine(_spawnCube.StartSong());
        
        _gameController.Play();
    }
    
    public void QuitGame()
    {
        ContinueGame();
        _leftController.GetComponent<XRRayInteractor>().enabled = true;
        _rightController.GetComponent<XRRayInteractor>().enabled = true;
        _gameController.Restart();
    }
}
