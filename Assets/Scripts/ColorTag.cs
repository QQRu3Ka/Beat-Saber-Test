using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTag : MonoBehaviour
{
    [SerializeField] private Color _color;

    public Color Color
    {
        get => _color;
        set => _color = value;
    }
}
