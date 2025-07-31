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

    public SpawnerManager SpawnerManager => _spawnerManager;

    private void Start()
    {
        StartCoroutine(StartSong());
    }

    private IEnumerator StartSong()
    {
        yield return new WaitUntil(() => _audioSource.isPlaying);

        var startDspTime = AudioSettings.dspTime;

        var beatDuration = 60.0 / _spawnerManager.Bpm;

        for (var i = 0; i < _spawnerManager.Lines.Count; i++)
        {
            var targetDspTime = startDspTime + i * beatDuration;
            var timeUntilNext = targetDspTime - AudioSettings.dspTime;

            if (timeUntilNext > 0)
                yield return new WaitForSeconds((float)timeUntilNext);

            Summon(_spawnerManager.Lines[i]);
        }
    }

    private void Summon(string line)
    {
        if(line.Length == 0) return;
        var cubes = line.Split(' ');

        foreach (var cubeInfo in cubes)
        {
            var attribs = cubeInfo.Split('_');
            var position = new Vector3(int.Parse(attribs[2])*2, int.Parse(attribs[3]), 20);
            var cube = Instantiate(attribs[1] == "Any" ? _cubes[1] : _cubes[0], position, Quaternion.identity);
            var setup = cube.GetComponent<SetupCube>();
            setup.SetColor(attribs[0]);
            setup.SetRotation(attribs[1]);
        }
    }
}
