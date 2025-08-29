using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner", menuName = "Cube/SpawnerManager", order = 1)]
public class SpawnerManager : ScriptableObject
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public AudioClip Song { get; set; }
    
    [field: SerializeField] public double Delay { get; set; }
    [field: SerializeField] public int Bpm {get; set;}
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public List<string> Lines { get; set; }
    [field: SerializeField] public List<CubeData> Cubes { get; set; }
    [field: SerializeField] public List<ListCubeData> ListCubes { get; set; }

    [Serializable]
    public class ListCubeData
    {
        [field: SerializeField] public List<CubeData> CubeData { get; set; }
    }
    
    [Serializable]
    public class CubeData
    {
        [field: SerializeField] public GameColor Color { get; set; }
        [field: SerializeField] public Side Side { get; set; }
        [field: SerializeField] public Vector2 Position { get; set; }
        [field: SerializeField] public float TimeInBeats { get; set; }
    }
}

