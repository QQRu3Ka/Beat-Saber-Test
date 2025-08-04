using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTag : MonoBehaviour
{
    [SerializeField] private GameColor _color;

    public GameColor Color
    {
        get => _color;
        set => _color = value;
    }
}
