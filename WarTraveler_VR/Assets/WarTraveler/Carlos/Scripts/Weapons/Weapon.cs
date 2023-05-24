using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Weapon_XR_GrabInteractableTwoHanded _weaponXRGrab;
    [SerializeField] private XR_InputDetector _xrInputDetector;
    [SerializeField] private WeaponMagazineDetector _weaponMagazineDetector;
    
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

    //GETTERS && SETTERS//
    public GameObject MagazinePrefab => _magazinePrefab;
    public XR_InputDetector XRInputDetector
    {
        get => _xrInputDetector;
        set => _xrInputDetector = value;
    }
    
    //////////////////////////////////////////////////////////

    public abstract void Shoot();

    private void Awake()
    {
        _weaponXRGrab = GetComponent<Weapon_XR_GrabInteractableTwoHanded>();
        _weaponMagazineDetector = GetComponentInChildren<WeaponMagazineDetector>();
    }

    private void Update()
    {
        if (_weaponXRGrab.isSelected && _magazine != null)
        {
            if (_xrInputDetector == null) return;

            if (_xrInputDetector.SecondaryButton.action.IsPressed())
            {
                DropMagazine();   
            }
        }
    }

    /// <summary>
    /// Método que se ejecuta cuando usas el (cerrojo o recamara) de un arma para introducir la primera bala que haya en el cargador
    /// O sacar la bala que haya en la recámara;
    /// </summary>
    [ContextMenu(nameof(BoltAction))]
    public virtual void BoltAction()
    {
        if (_hasBreechBullet)
        {
            _bulletShellExitForce = Random.Range(2f, 2.5f);
            GameObject bulletShell = Instantiate(_bulletShellPrefab, _bulletShellExit.position, _bulletShellExit.rotation);
            bulletShell.GetComponent<Rigidbody>().AddForce(_bulletShellExit.right * _bulletShellExitForce, ForceMode.Impulse);
        }
        
        if (_magazine != null && _magazine.CurrentAmmoInMagazine > 0)
        {
            _magazine.CurrentAmmoInMagazine--;
            _hasBreechBullet = true;
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
        _magazine.BoxCollider.isTrigger = false;
        _magazine.Rigidbody.isKinematic = false;
        _magazine.transform.parent = null;
        _weaponMagazineDetector.XRSlider.MHandle = null;
        _magazine = null;
    }
}
