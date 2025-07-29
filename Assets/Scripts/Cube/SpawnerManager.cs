using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner", menuName = "Cube/SpawnerManager", order = 1)]
public class SpawnerManager : ScriptableObject
{
    [field: SerializeField] public List<string> Lines { get; set; }
}

