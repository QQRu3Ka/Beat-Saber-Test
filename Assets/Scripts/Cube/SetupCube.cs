using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SetupCube : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;
    [SerializeField] private List<RotationData> _rotations;
    private ColorTag _color;
    private Dictionary<float, Side> _sides;

    public Dictionary<float, Side> Sides
    {
        get => _sides;
        set => _sides = value;
    }

    private void Awake()
    {
        _sides = new Dictionary<float, Side>();
        foreach (var rotationData in _rotations)
        {
            _sides[rotationData.Rotation] = rotationData.Side;
        }
        _color = transform.gameObject.GetComponent<ColorTag>();
    }

    private void Start()
    {
        transform.Rotate(0, 0, Random.Range(0, 4)*90);
        var materialRng = Random.Range(0, _materials.Count);
        _color.Color = (Color)materialRng;
        foreach (Transform part in transform)
        {
            part.gameObject.GetComponent<MeshRenderer>().material = _materials[materialRng];
        }
    }

    [Serializable]
    private class RotationData
    {
        [field:SerializeField] public float Rotation { get; set; }
        [field:SerializeField] public Side Side { get; set; }
    }
}
