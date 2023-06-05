using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weaponsList;

    public List<Weapon> WeaponsList => _weaponsList;
}
