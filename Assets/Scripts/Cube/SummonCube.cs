using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Rng = System.Random;

public class SummonCube : MonoBehaviour
{
    [SerializeField] private GameObject _cube;
    [SerializeField] private int _bpm;
    
    private List<int> _sides;

    private void Awake()
    {
        _sides = new List<int>() {-1, 1};
    }
    
    private void Start()
    {
        StartCoroutine(StartSong());
    }

    private IEnumerator StartSong()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f/_bpm);
            Summon();
        }
    }
    
    private void Summon()
    {
        var position = new Vector3(_sides[Random.Range(0, _sides.Count)], 0, 20);
        var cube = Instantiate(_cube, position, Quaternion.identity);
    }
}
