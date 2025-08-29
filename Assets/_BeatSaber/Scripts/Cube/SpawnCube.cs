using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Rng = System.Random;

public class SpawnCube : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cubes;
    
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _camera;
    
    [SerializeField] private List<PositionXData> _positionX;
    [SerializeField] private List<PositionYData> _positionY;
    [SerializeField] private List<ColorData> _colors;
    [SerializeField] private List<SideData> _sides;
    
    private LevelTest _levelTest;
    private Dictionary<int, float> _positionXDict;
    private Dictionary<int, float> _positionYDict;
    private Dictionary<int, GameColor> _colorDict;
    private Dictionary<int, Side> _sideDict;

    private float _latestCubeSpawn;
    private GameController _gameController;
    private Vector3 _startPosition;

    public SpawnerManager SpawnerManager { get; set; }

    private void Awake()
    {
        _positionXDict = new Dictionary<int, float>();
        foreach (var i in _positionX)
        {
            _positionXDict[i.X] = i.RealX;
        }
        _positionYDict = new Dictionary<int, float>();
        foreach (var i in _positionY)
        {
            _positionYDict[i.Y] = i.RealY;
        }
        _colorDict = new Dictionary<int, GameColor>();
        foreach (var i in _colors)
        {
            _colorDict[i.ColorID] = i.Color;
        }
        _sideDict = new Dictionary<int, Side>();
        foreach (var i in _sides)
        {
            _sideDict[i.SideID] = i.Side;
        }
        _levelTest = GetComponent<LevelTest>();
        _gameController = GetComponent<GameController>();
    }

    private void Start()
    {
        _startPosition = _camera.transform.position;
        StartCoroutine(StartSong());
    }

    private IEnumerator TestStartSong()
    {
        yield return new WaitUntil(() => _gameController.IsPlaying);

        var list = _levelTest.NotesList;

        var index = 0;

        while (index < list.Count)
        {
            var currentTime = AudioSettings.dspTime - _gameController.StartTime;
            var cube = list[index];
            var time = cube.b * 60f / SpawnerManager.Bpm;
            
            if (time - currentTime <= 0)
            {
                TestSummon(cube);
                index++;
            }
            
            yield return null;
        }
    }

    private void TestSummon(LevelTest.FullNoteData cubeData)
    {
        var position = new Vector3(_positionXDict[cubeData.x], _positionYDict[cubeData.y], 20*SpawnerManager.Speed+_startPosition.z);
        var cube = Instantiate(cubeData.d == 8 ? _cubes[1] : _cubes[0], position, Quaternion.identity);
        var setup = cube.GetComponent<SetupCube>();
        setup.SetColor(_colorDict[cubeData.c]);
        setup.SetRotation(_sideDict[cubeData.d]);
        setup.SetSpeed(SpawnerManager.Speed*-10);
    }
    
    public IEnumerator StartSong()
    {
        yield return new WaitUntil(() => _gameController.IsPlaying);
        var list = GetAllNotesSorted();

        var index = 0;

        while (index < list.Count)
        {
            if (!_gameController.IsPlaying)
            {
                yield return null;
                continue;
            }
            
            var currentTime = AudioSettings.dspTime - _gameController.StartTime;

            var cube = list[index];
            double time = cube.TimeInBeats * 60f / SpawnerManager.Bpm;

            if (time - currentTime <= 1.5f)
            {
                Summon(cube);
                index++;
            }
            
            yield return null;
        }
    }

    private void Summon(SpawnerManager.CubeData cubeData)
    {
        var position = new Vector3(cubeData.Position.x, cubeData.Position.y, 20*SpawnerManager.Speed+_startPosition.z);
        var cube = Instantiate(cubeData.Side == Side.Any ? _cubes[1] : _cubes[0], position, Quaternion.identity);
        var setup = cube.GetComponent<SetupCube>();
        setup.SetColor(cubeData.Color);
        setup.SetRotation(cubeData.Side);
        setup.SetSpeed(SpawnerManager.Speed*-10);
    }
    
    private List<SpawnerManager.CubeData> GetAllNotesSorted()
    {
        var list = new List<SpawnerManager.CubeData>();
        foreach (var l in SpawnerManager.ListCubes)
            list.AddRange(l.CubeData);

        list.Sort((a, b) => a.TimeInBeats.CompareTo(b.TimeInBeats));
        return list;
    }

    [Serializable]
    public class PositionXData
    {
        [field:SerializeField] public int X { get; set; }
        [field:SerializeField] public float RealX {get; set;}
    }

    [Serializable]
    public class PositionYData
    {
        [field:SerializeField] public int Y { get; set; }
        [field:SerializeField] public float RealY {get; set;}
    }

    [Serializable]
    public class ColorData
    {
        [field:SerializeField] public int ColorID { get; set; }
        [field:SerializeField] public GameColor Color { get; set; }
    }

    [Serializable]
    public class SideData
    {
        [field:SerializeField] public int SideID { get; set; }
        [field:SerializeField] public Side Side { get; set; }
    }
}
