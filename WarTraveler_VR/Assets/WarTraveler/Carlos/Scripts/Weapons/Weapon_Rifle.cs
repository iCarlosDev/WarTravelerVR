using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Rifle : Weapon
{
    [Header("--- RIFLE PARAMS ---")] 
    [Space(10)] 
    [SerializeField] private bool _isSemiAutomatic;

    public override void Shoot()
    {
        if (!_hasBreechBullet) return;

        if (_currentAmmoInMagazine == 0) _hasBreechBullet = false;

        if (_hasBreechBullet) _currentAmmoInMagazine--;
        
        
        //_particleSystem.Play();
    }
}
