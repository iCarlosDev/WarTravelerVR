using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("--- WEAPON STATS ---")] 
    [Space(10)] 
    [SerializeField] private float _fireRate;
    [SerializeField] private float _recoil;
    [SerializeField] protected float _shootForce;
    [SerializeField] protected float _bulletShellExitForce;
    
    [Header("--- WEAPON AMMO ---")]
    [Space(10)]
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected GameObject _bulletShellPrefab;
    [SerializeField] protected int _maxAmmo;
    [SerializeField] protected int _currentAmmoInMagazine;
    [SerializeField] protected int _magazineCapacity;
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

    public abstract void Shoot();

    /// <summary>
    /// Método que se ejecuta cuando usas el (cerrojo o recamara) de un arma para introducir la primera bala que haya en el cargador
    /// O sacar la bala que haya en la recámara;
    /// </summary>
    [ContextMenu(nameof(BoltAction))]
    public virtual void BoltAction()
    {
        if (_currentAmmoInMagazine > 0)
        {
            _hasBreechBullet = true;
            _bulletShellExitForce = Random.Range(2f, 2.5f);
            GameObject bulletShell = Instantiate(_bulletShellPrefab, _bulletShellExit.position, _bulletShellExit.rotation);
            bulletShell.GetComponent<Rigidbody>().AddForce(_bulletShellExit.forward * _bulletShellExitForce, ForceMode.Impulse);
            _currentAmmoInMagazine--;
        }
        else
        {
            _hasBreechBullet = false;
        }
    }

    [ContextMenu(nameof(InsertMagazine))]
    private void InsertMagazine()
    {
        _currentAmmoInMagazine = 10;
    }

    public virtual void DropMagazine()
    {
        _currentAmmoInMagazine = 0;
    }
}
