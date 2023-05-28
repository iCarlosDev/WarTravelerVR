using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandData : MonoBehaviour
{
    private enum _HandModelType {Left, Right}
    [SerializeField] private _HandModelType _handType;
    [SerializeField] private Transform _root;
    [SerializeField] private Transform[] fingerBones;

    public Transform Root => _root;
    public Transform[] FingerBones => fingerBones;
}
