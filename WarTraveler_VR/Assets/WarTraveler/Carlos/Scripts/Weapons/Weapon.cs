using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour
{
    [Header("--- WEAPON STATS ---")] 
    [Space(10)] 
    [SerializeField] private GameObject _magazinePrefab;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _recoil;
    [SerializeField] protected float _shootForce;
    [SerializeField] protected float _bulletShellExitForce;
    [SerializeField] protected bool _hasMagazineIn;
    
    [Header("--- WEAPON AMMO ---")]
    [Space(10)]
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected GameObject _bulletShellPrefab;
    [SerializeField] protected Magazine _magazine;
    [Tooltip("Indica si tienes una bala en la recámara")]
    [SerializeField] protected bool _hasBreechBullet;
    
    [Header("--- WEAPON PARTICLES ---")]
    [Space(10)]
    [SerializeField] protected ParticleSystem _particleSystem;
    
    [Header("--- WEAPON PARTS ---")]
    [Space(10)]
    [SerializeField] protected Transform _canon;
    [SerializeField] protected Transform _bulletShellExit;

    [Header("--- WEAPON GRABS POSITIONS ---")] 
    [Space(10)] 
    [SerializeField] private Transform _secondGrab;
    [SerializeField] private Transform _magazineGrab;
    [SerializeField] private Transform _breech;
    
    //GETTERS && SETTERS//
    public GameObject MagazinePrefab => _magazinePrefab;

    public abstract void Shoot();

    /// <summary>
    /// Método que se ejecuta cuando usas el (cerrojo o recamara) de un arma para introducir la primera bala que haya en el cargador
    /// O sacar la bala que haya en la recámara;
    /// </summary>
    [ContextMenu(nameof(BoltAction))]
    public virtual void BoltAction()
    {
        if (_magazine != null && _magazine.CurrentAmmoInMagazine > 0)
        {
            _hasBreechBullet = true;
            _bulletShellExitForce = Random.Range(2f, 2.5f);
            GameObject bulletShell = Instantiate(_bulletShellPrefab, _bulletShellExit.position, _bulletShellPrefab.transform.rotation);
            bulletShell.GetComponent<Rigidbody>().AddForce(_bulletShellExit.forward * _bulletShellExitForce, ForceMode.Impulse);
            _magazine.CurrentAmmoInMagazine--;
        }
        else
        {
            _hasBreechBullet = false;
        }
    }

    [ContextMenu(nameof(InsertMagazine))]
    public void InsertMagazine(Magazine magazine)
    {
        _magazine = magazine;
        _magazine.IsBeingInserted = false;
        _magazine.IsInserted = true;
    }

    public virtual void DropMagazine()
    {
        _magazine = null;
    }
}
