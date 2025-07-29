using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Rng = System.Random;

public class SummonCube : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private int _bpm;
    [SerializeField] private SpawnerManager _spawnerManager;

    private void Awake()
    {
    }
    
    private void Start()
    {
        StartCoroutine(StartSong());
    }

    private IEnumerator StartSong()
    {
        foreach (var line in _spawnerManager.Lines)
        {
            yield return new WaitForSeconds(60f/_bpm);
            Summon(line);
        }
    }
    
    private void Summon(string line)
    {
        if(line.Length == 0) return;
        var cubes = line.Split(' ');
        
        if (cubes[0] != "_")
        {
            var attribs = cubes[0].Split('_');
            var position = new Vector3(-1, 0, 20);
            var cube = Instantiate(_cube, position, Quaternion.identity);
            var setup = cube.GetComponent<SetupCube>();
            setup.SetColor(attribs[0]);
            setup.SetRotation(attribs[1]);
        }
        
        if (cubes[1] != "_")
        {
            var attribs = cubes[1].Split('_');
            var position = new Vector3(1, 0, 20);
            var cube = Instantiate(_cube, position, Quaternion.identity);
            var setup = cube.GetComponent<SetupCube>();
            setup.SetColor(attribs[0]);
            setup.SetRotation(attribs[1]);
        }
    }
}
