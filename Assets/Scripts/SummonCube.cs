using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCube : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private List<Material> materials;
    private List<string> _tags;
    private MeshRenderer _meshRenderer;
    private List<int> _sides;

    private List<int> Sides { get; set; }

    private MeshRenderer MeshRenderer
    {
        get => _meshRenderer;
        set => _meshRenderer = value;
    }

    public List<string> Tags
    {
        get => _tags;
        set => _tags = value;
    }

    void Awake()
    {
        Sides = new List<int>() {-1, 1};
        MeshRenderer = cube.GetComponent<MeshRenderer>();
        Tags = new List<string>() {"RedCube", "BlueCube"};
    }
    
    void Update()
    {
        
        Summon();
    }

    private void Summon()
    {
        int r = Random.Range(0, 500);
        if (r == 3)
        {
            int rng = Random.Range(0, 2);
            MeshRenderer.material = materials[rng];
            cube.tag = Tags[rng];
            Instantiate(cube, new Vector3(Sides[Random.Range(0, Sides.Count)], 0, 20),
                Quaternion.identity);
        }
    }
}
