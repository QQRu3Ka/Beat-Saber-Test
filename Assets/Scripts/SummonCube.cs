using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Rng = System.Random;

public class SummonCube : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private List<Material> materials;
    private List<string> _tags;
    private MeshRenderer _meshRenderer;
    private List<int> _sides;
    [SerializeField] private Side side;

    void Awake()
    {
        _sides = new List<int>() {-1, 1};
        _meshRenderer = cube.GetComponent<MeshRenderer>();
        _tags = new List<string>() {"RedCube", "BlueCube"};
        
        Array values = Enum.GetValues(typeof(Side));
        Rng random = new Rng();
        side = (Side)values.GetValue(random.Next(values.Length-1));
    }
    
    void Update()
    {
        Summon();
    }

    private void Summon()
    {
        int summonRng = Random.Range(0, 500);
        if (summonRng == 3)
        {
            int materialRng = Random.Range(0, 2);
            _meshRenderer.material = materials[materialRng];
            cube.tag = _tags[materialRng];
            GameObject cub = Instantiate(cube, new Vector3(_sides[Random.Range(0, _sides.Count)], 0, 20),
                Quaternion.Euler(0, 180, Random.Range(0, 4) * 90));
        }
    }
}
