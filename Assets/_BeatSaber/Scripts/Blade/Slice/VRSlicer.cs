using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSlicer : MonoBehaviour, ISlice
{
    [SerializeField] private GameObject _startSword;
    [SerializeField] private GameObject _endSword;
    private Vector3 _prevStartPosition = Vector3.zero;
    private Vector3 _prevEndPosition = Vector3.zero;
    public Vector3 PrevSecondPosition { get; private set; } = Vector3.zero;
    public Vector3 StartSwordPosition => _startSword.transform.position;

    public Plane CutPlane {get; private set;}

    private void Start()
    {
        StartCoroutine(CheckPosition());
    }

    private void Update()
    {
        CutPlane = new Plane(_prevEndPosition, _prevStartPosition, _startSword.transform.position);
    }

    private void LateUpdate()
    {
        _prevStartPosition = _startSword.transform.position;
        _prevEndPosition = _endSword.transform.position;
    }

    private IEnumerator CheckPosition()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            PrevSecondPosition = transform.position;
        }
    }
    public Side Slice()
    {
        return Side.None;
    }
}
