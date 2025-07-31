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
    private MeshRenderer _meshRenderer;

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
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    public void SetColor(string color)
    {
        Enum.TryParse(color, out Color col);
        _meshRenderer.material = _materials[col];
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
