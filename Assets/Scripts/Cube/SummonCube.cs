using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Rng = System.Random;

public class SummonCube : MonoBehaviour
{
    private const string LeftPartName = "LeftPart";
    private const string RightPartName = "RightPart";
    [SerializeField] private GameObject _cube;
    [SerializeField] private List<Material> _materials;
    [SerializeField] private int _bpm;
    private List<string> _tags;
    private MeshRenderer _meshRendererLeft;
    private MeshRenderer _meshRendererRight;
    
    private List<int> _sides;

    private void Awake()
    {
        _sides = new List<int>() {-1, 1};
        _meshRendererLeft = _cube.transform.Find(LeftPartName).gameObject.GetComponent<MeshRenderer>();
        _meshRendererRight = _cube.transform.Find(RightPartName).gameObject.GetComponent<MeshRenderer>();
        _tags = new List<string>() {"RedCube", "BlueCube"};
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
        var materialRng = Random.Range(0, _materials.Count);
        _meshRendererLeft.material = _materials[materialRng];
        _meshRendererRight.material = _materials[materialRng];
        _cube.tag = _tags[materialRng];
        var position = new Vector3(_sides[Random.Range(0, _sides.Count)], 0, 20);
        var rotation = Quaternion.Euler(0, 0, Random.Range(0, 4) * 90);
        var newCube = Instantiate(_cube, position, rotation);
        //Не менять префаб, менять копию
        //Создать скрипт с настройкой куба: вид куба, цвет куба
        
        
    }
}
