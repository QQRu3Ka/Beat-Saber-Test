using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    private string _animatorVar = "slicing";
    private Animator _animator;
    private ISlice _slice;
    public Side side;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _slice = GetComponent<KeyboardSlice>();
    }
    void Update()
    {
        Side s = transform.CompareTag("RedSword") ? _slice.SliceRed() : _slice.SliceBlue();
        if (s != Side.None) side = s;
        if (s == Side.Down)
        {
            _animator.SetInteger(_animatorVar, 1);
            StartCoroutine(Slice());
        }

        if (s == Side.Up)
        {
            _animator.SetInteger(_animatorVar, 2);
            StartCoroutine(Slice());
        }

        if (s == Side.Right)
        {
            _animator.SetInteger(_animatorVar, 3);
            StartCoroutine(Slice());
        }

        if (s == Side.Left)
        {
            _animator.SetInteger(_animatorVar, 4);
            StartCoroutine(Slice());
        }
    }

    private IEnumerator Slice()
    {
        yield return new WaitForSeconds(0.5f);
        _animator.SetInteger(_animatorVar, 0);
    }
}
