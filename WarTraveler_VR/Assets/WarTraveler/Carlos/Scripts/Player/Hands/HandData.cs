using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandData : MonoBehaviour
{
    private enum _HandModelType {Left, Right}
    [SerializeField] private _HandModelType _handType;
    [SerializeField] private Transform _root;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform[] fingerBones;

    public Animator Animator => _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
