using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalChecker : MonoBehaviour
{
    private float _time = 0;
    private float _interval = 0;
    void Update()
    {
        _time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            print(_time - _interval);
            _interval = _time;
        }
    }
}
