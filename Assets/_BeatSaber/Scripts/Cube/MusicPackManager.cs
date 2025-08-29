using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Music Packs", menuName = "Game/Music Packs", order = 1)]
public class MusicPackManager : ScriptableObject
{
    [field:SerializeField] public List<MusicPackData> MusicPacks { get; set; }

    [Serializable]
    public class MusicPackData
    {
        [field:SerializeField] public string Name { get; set; }
        [field:SerializeField] public List<SpawnerManager> MusicPack { get; set; }
    }
}
