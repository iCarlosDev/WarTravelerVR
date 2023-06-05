using System.Collections.Generic;
using UnityEngine;

public class BrokenPlane : MonoBehaviour
{
    [Header("--- RIGIDBODIES ---")]
    [Space(10)]
    [SerializeField] private List<Rigidbody> _rigidbodiesList;
    
    [Header("--- FORCES ---")]
    [Space(10)]
    [Tooltip("Fuerza con la que el avión irá")]
    [SerializeField] private float _impulseForce;
    [Tooltip("Fuerza rotatoría que tendrá el avión")]
    [SerializeField] private float _torqueForce;

    private void Awake()
    {
        _rigidbodiesList.AddRange(GetComponentsInChildren<Rigidbody>());
    }

    private void Start()
    {
       SetForces();
    }

    /// <summary>
    /// Método para setear las fuerzas con las que serán impulsados los rigidbodies que tenga el avión una vez esté roto;
    /// </summary>
    private void SetForces()
    {
        foreach (Rigidbody rigidbody in _rigidbodiesList)
        {
            rigidbody.AddForce(Vector3.up * _impulseForce, ForceMode.Impulse);
            rigidbody.AddTorque(Vector3.forward * _torqueForce, ForceMode.Impulse);
        }
    }
}
