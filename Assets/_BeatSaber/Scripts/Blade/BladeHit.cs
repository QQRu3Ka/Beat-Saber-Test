using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeHit : MonoBehaviour
{
    private string _animatorVar = "slicing";
    private Animator _animator;
    private ISlice _slice;

    public Side Side { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _slice = GetComponent<ISlice>();
        Side = Side.Down;
    }
    private void Update()
    {
        var s = _slice.Slice();
        if (s == Side.None) return;
        Side = s;
        StartCoroutine(ResetSide());
        _animator.SetInteger(_animatorVar, (int)s+1);
        StartCoroutine(Slice());
    }

    private IEnumerator Slice()
    {
        yield return new WaitForSeconds(0.25f);
        _animator.SetInteger(_animatorVar, 0);
    }

    private IEnumerator ResetSide()
    {
        yield return new WaitForSeconds(0.25f);
        Side = Side.Down;
    }
}
