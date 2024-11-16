using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpeed : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Update()
    {
        _animator.SetFloat();
    }
}
