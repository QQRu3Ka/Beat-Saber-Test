using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicer : MonoBehaviour, ISlice
{
    [SerializeField] private List<KeyCodeSideData> _keyCodeSideData;
    private Dictionary<KeyCode, Side> _keySideDict;
    
    private void Awake()
    {
        _keySideDict = new Dictionary<KeyCode, Side>();
        foreach (var keyCodeSideData in _keyCodeSideData)
        {
            _keySideDict[keyCodeSideData.Key] = keyCodeSideData.Side;
        }
    }
    public Side Slice()
    {
        foreach (var pair in _keySideDict)
        {
            if (Input.GetKey(pair.Key))
            {
                return pair.Value;
            }
        }
        return Side.None;
    }
    
    [Serializable]
    private class KeyCodeSideData
    {
        [field:SerializeField] public KeyCode Key { get; set; }
        [field:SerializeField] public Side Side { get; set; }
    }
}
