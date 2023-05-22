using System;
using System.Collections.Generic;
using UnityEngine;

public class TakePouchAmmo : MonoBehaviour
{
    public static TakePouchAmmo instance;

    [SerializeField] private List<Weapon> _grabbedWeaponsList;

    //GETTERS && SETTERS//
    public List<Weapon> GrabbedWeaponsList => _grabbedWeaponsList;

    /////////////////////////////////////////////////

    private void Awake()
    {
        instance = this;
    }
}

