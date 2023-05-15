using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("--- WEAPON STATS ---")] 
    [Space(10)] 
    [SerializeField] private float _fireRate;
    [SerializeField] private float _recoil;
    
    [Header("--- WEAPON AMMO ---")]
    [Space(10)]
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected int _maxAmmo;
    [SerializeField] protected int _currentAmmoInMagazine;
    [SerializeField] protected int _magazineCapacity;
    [SerializeField] protected bool _hasBreechBullet;
    
    [Header("--- WEAPON PARTICLES ---")]
    [Space(10)]
    [SerializeField] protected ParticleSystem _particleSystem;

    [Header("--- WEAPON GRABS POSITIONS ---")] 
    [Space(10)] 
    [SerializeField] private Transform _secondGrab;
    [SerializeField] private Transform _magazineGrab;
    [SerializeField] private Transform _breech;

    public abstract void Shoot();

    public virtual void Reload()
    {
        
    }

    public virtual void DropMagazine()
    {
        
    }
}
