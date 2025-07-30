using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SetupCube : MonoBehaviour
{
    [SerializeField] private List<ColorData> _colors;
    [SerializeField] private List<RotationData> _rotations;
    private ColorTag _color;
    private CubeStats _cubeStats;
    private Dictionary<float, Side> _sides;
    private Dictionary<Color, Material> _materials;

    public Dictionary<float, Side> Sides
    {
        get => _sides;
        set => _sides = value;
    }

    public Dictionary<Color, Material> Materials
    {
        get => _materials;
        set => _materials = value;
    }

    private void Awake()
    {
        _sides = new Dictionary<float, Side>();
        _materials = new Dictionary<Color, Material>();
        foreach (var rotationData in _rotations)
        {
            _sides[rotationData.Rotation] = rotationData.Side;
        }

        foreach (var colorData in _colors)
        {
            _materials[colorData.Color] = colorData.Material;
        }
        _color = transform.gameObject.GetComponent<ColorTag>();
        _cubeStats = transform.gameObject.GetComponent<CubeStats>();
    }
    
    // private void Start()
    // {
    //     transform.Rotate(0, 0, Random.Range(0, 4)*90);
    //     var materialRng = Random.Range(0, _materials.Count);
    //     _color.Color = (Color)materialRng;
    //     foreach (Transform part in transform)
    //     {
    //         part.gameObject.GetComponent<MeshRenderer>().material = _materials[materialRng];
    //     }
    // }

    public void SetColor(string color)
    {
        Enum.TryParse(color, out Color col);
        gameObject.GetComponent<MeshRenderer>().material = _materials[col];
        _color.Color = col;
        _cubeStats.Color = col;
    }

    public void SetRotation(string rotation)
    {
        Enum.TryParse(rotation, out Side side);
        transform.Rotate(0,0,(int)side*45);
        _cubeStats.Side = side;
    }

    [Serializable]
    private class RotationData
    {
        [field:SerializeField] public float Rotation { get; set; }
        [field:SerializeField] public Side Side { get; set; }
    }

    [Serializable]
    private class ColorData
    {
        [field: SerializeField] public Color Color { get; set; }
        [field: SerializeField] public Material Material { get; set; }
    }
}
