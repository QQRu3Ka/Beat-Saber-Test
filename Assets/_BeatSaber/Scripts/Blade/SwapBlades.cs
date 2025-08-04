using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapBlades : MonoBehaviour
{
    [SerializeField] private Transform _redSword;
    [SerializeField] private Transform _blueSword;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        var tmp = _redSword.position.x;
        _redSword.position = new Vector3(_blueSword.position.x, _redSword.position.y, _redSword.position.z);
        _blueSword.position = new Vector3(tmp, _blueSword.position.y, _blueSword.position.z);
    }
}
