using System;
using UnityEngine;

public class Weapon_Rifle : Weapon
{
    [Header("--- RIFLE PARAMS ---")] 
    [Space(10)] 
    [SerializeField] private bool _isSemiAutomatic;

    private void Start()
    {
        
    }

    public override void Shoot()
    {
        if (!_hasBreechBullet) return;

        if (_currentAmmoInMagazine == 0) _hasBreechBullet = false;

        if (_hasBreechBullet) BoltAction();
        
        GameObject bullet = Instantiate(_bulletPrefab, _canon.position, _canon.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(_canon.forward * _shootForce, ForceMode.Impulse);
        _particleSystem.Play();
    }
}
