using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SetupCube : MonoBehaviour
{
    [SerializeField] private List<ColorData> _colors;
    [SerializeField] private List<RotationData> _rotations;
    [SerializeField] private GameObject _cubeObject;
    private ColorTag _color;
    private CubeStats _cubeStats;
    private MoveCube _moveCube;
    private Dictionary<float, Side> _sides;
    private Dictionary<GameColor, Material> _materials;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _sides = new Dictionary<float, Side>();
        _materials = new Dictionary<GameColor, Material>();
        foreach (var rotationData in _rotations)
        {
            _sides[rotationData.Rotation] = rotationData.Side;
        }

        foreach (var colorData in _colors)
        {
            _materials[colorData.Color] = colorData.Material;
        }
        _color = GetComponent<ColorTag>();
        _cubeStats = GetComponent<CubeStats>();
        _moveCube = GetComponent<MoveCube>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetColor(GameColor color)
    {
        _meshRenderer.material = _materials[color];
        _color.Color = color;
        _cubeStats.Color = color;
    }

    public void SetRotation(Side side)
    {
        transform.Rotate(0,0,(int)side*45);
        _cubeStats.Side = side;
    }

    public void SetSpeed(float speed)
    {
        _moveCube.MoveSpeed = speed;
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
        [field: SerializeField] public GameColor Color { get; set; }
        [field: SerializeField] public Material Material { get; set; }
    }
}
