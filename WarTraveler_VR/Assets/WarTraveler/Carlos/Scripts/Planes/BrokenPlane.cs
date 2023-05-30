using System.Collections.Generic;
using UnityEngine;

public class BrokenPlane : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> _rigidbodiesList;
    [SerializeField] private float _impulseForce;
    [SerializeField] private float _torqueForce;

    private void Awake()
    {
        _rigidbodiesList.AddRange(GetComponentsInChildren<Rigidbody>());
    }

    private void Start()
    {
        foreach (Rigidbody rigidbody in _rigidbodiesList)
        {
            rigidbody.AddForce(Vector3.up * _impulseForce, ForceMode.Impulse);
            rigidbody.AddTorque(Vector3.forward * _torqueForce, ForceMode.Impulse);
        }
    }
}
