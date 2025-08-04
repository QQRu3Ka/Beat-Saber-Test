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
    [SerializeField] private SpawnerManager _spawnerManager;
    [SerializeField] private AudioSource _audioSource;

    private double _latestCubeSpawn;

    public SpawnerManager SpawnerManager => _spawnerManager;

    private void Start()
    {
        StartCoroutine(StartSong());
    }

    private IEnumerator StartSong()
    {
        yield return new WaitUntil(() => _audioSource.isPlaying);

        foreach (var list in _spawnerManager.ListCubes)
        {
            foreach (var cube in list.CubeData)
            {
                var timeUntilNext = cube.DelayTime - _latestCubeSpawn;
                _latestCubeSpawn = cube.DelayTime;
                if (timeUntilNext > 0)
                    yield return new WaitForSeconds((float)timeUntilNext);
            
                Summon(cube);
            }
        }
    }

    private void Summon(SpawnerManager.CubeData cubeData)
    {
        var position = new Vector3(cubeData.Position.x, cubeData.Position.y, 20*_spawnerManager.Speed);
        var cube = Instantiate(cubeData.Side == Side.Any ? _cubes[1] : _cubes[0], position, Quaternion.identity);
        var setup = cube.GetComponent<SetupCube>();
        setup.SetColor(cubeData.Color);
        setup.SetRotation(cubeData.Side);
        setup.SetSpeed(_spawnerManager.Speed*-10);
    }
}
