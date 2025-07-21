using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonCube : MonoBehaviour
{
    [SerializeField] private List<GameObject> cubes;
    private List<int> _sides;

    private List<int> Sides { get; set; }

    void Awake()
    {
        Sides = new List<int>() {-1, 1};
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
            Instantiate(cubes[Random.Range(0, cubes.Count)], new Vector3(Sides[Random.Range(0, Sides.Count)], 0, 20),
                transform.rotation);
        }
    }
}
