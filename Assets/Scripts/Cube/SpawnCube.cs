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
    [SerializeField] private int _bpm;
    [SerializeField] private SpawnerManager _spawnerManager;
    [SerializeField] private AudioSource _audioSource;
    private float _time;
    
    private void Start()
    {
        StartCoroutine(StartSong());
    }

    private void Update()
    {
        _time += Time.deltaTime;
    }

    private IEnumerator StartSong()
    {
        yield return new WaitUntil(() => _audioSource.isPlaying);

        var startDspTime = AudioSettings.dspTime;

        var beatDuration = 60.0 / _bpm;

        for (var i = 0; i < _spawnerManager.Lines.Count; i++)
        {
            var targetDspTime = startDspTime + i * beatDuration;
            var timeUntilNext = targetDspTime - AudioSettings.dspTime;

            if (timeUntilNext > 0)
                yield return new WaitForSeconds((float)timeUntilNext);

            Summon(_spawnerManager.Lines[i]);
            print(_time);
        }
    }

    private void Summon(string line)
    {
        if(line.Length == 0) return;
        var cubes = line.Split(' ');

        foreach (var cubeInfo in cubes)
        {
            var attribs = cubeInfo.Split('_');
            GameObject cube;
            var position = new Vector3(Int32.Parse(attribs[2])*2, Int32.Parse(attribs[3]), 20);
            if (attribs[1] == "Any")
            {
                cube = Instantiate(_cubes[1], position, Quaternion.identity);
            }
            else
            {
                cube = Instantiate(_cubes[0], position, Quaternion.identity);
            } 
            var setup = cube.GetComponent<SetupCube>();
            setup.SetColor(attribs[0]);
            setup.SetRotation(attribs[1]);
        }
    }
}
