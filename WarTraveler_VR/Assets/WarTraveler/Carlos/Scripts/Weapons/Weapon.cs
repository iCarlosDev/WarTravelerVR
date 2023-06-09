using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Weapon_XR_GrabInteractableTwoHanded _weaponXRGrab;
    [SerializeField] private XR_InputDetector _xrInputDetector;
    [SerializeField] private WeaponBolt _weaponBolt;
    [SerializeField] private WeaponMagazineDetector _weaponMagazineDetector;
    
    [Header("--- WEAPON STATS ---")] 
    [Space(10)]
    [SerializeField] private GameObject _magazinePrefab;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _recoil;
    [SerializeField] protected float _shootForce;
    [SerializeField] protected float _bulletShellExitForce;
    [SerializeField] private bool _isSemiAutomatic;
    [SerializeField] private bool _isShooting;
    private float _fireRateTime;

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
    
    [Header("--- WEAPON SOUNDS ---")]
    [Space(10)]
    [SerializeField] protected List<AudioSource> _audioSourcesList;
    
    [Header("--- WEAPON PARTS ---")]
    [Space(10)]
    [SerializeField] protected Transform _canon;
    [SerializeField] protected Transform _bulletShellExit;

    private Coroutine _glowBack;

    //GETTERS && SETTERS//
    public GameObject MagazinePrefab => _magazinePrefab;
    public XR_InputDetector XRInputDetector
    {
        get => _xrInputDetector;
        set => _xrInputDetector = value;
    }
    public bool IsShooting
    {
        get => _isShooting;
        set => _isShooting = value;
    }

    //////////////////////////////////////////////////////////

    private void Awake()
    {
        _weaponXRGrab = GetComponent<Weapon_XR_GrabInteractableTwoHanded>();
        _weaponBolt = GetComponentInChildren<WeaponBolt>();
        _weaponMagazineDetector = GetComponentInChildren<WeaponMagazineDetector>();
        _audioSourcesList.AddRange(GetComponents<AudioSource>());
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

        if (_isSemiAutomatic) return;
        
        if (_xrInputDetector != null && _xrInputDetector.IsTriggering)
        {
            ShootAutomatic(); 
        }
    }

    public void ShootSemiAutomatic(ActivateEventArgs args)
    {
        if (!_xrInputDetector.CompareTag(args.interactorObject.transform.tag)) return;

        if (!_isSemiAutomatic) return;

        Shoot();
    }

    private void ShootAutomatic()
    {
        if (Time.time > _fireRateTime)
        {
            Shoot();
            _fireRateTime = Time.time + _fireRate;
        }
    }
    
    private void Shoot()
    {
        if (!_hasBreechBullet)
        {
            _audioSourcesList[1].PlayOneShot(_audioSourcesList[1].clip); 
            return;
        }

        if (_hasBreechBullet)
        {
            if (_glowBack != null)
            {
                StopCoroutine(_glowBack);
                _weaponBolt.XRSlider.value = 0f;
                _glowBack = null;
            }
            
            /*if (_weaponBolt.XRSlider.isSelected)
            {
                XRDirectInteractor xrDirectInteractor = _xrInputDetector.GetComponent<XRDirectInteractor>();
                
                xrDirectInteractor.interactionManager.SelectExit(xrDirectInteractor, _weaponBolt.XRSlider);
                _weaponBolt.XRSlider.interactionManager.SelectExit(xrDirectInteractor, _weaponBolt.XRSlider);
            }*/
            
            _glowBack = StartCoroutine(GlowBack_Coroutine());
        }

        GameObject bullet = Instantiate(_bulletPrefab, _canon.position, _canon.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(_canon.forward * _shootForce, ForceMode.Impulse);
        _particleSystem.Play();
        _audioSourcesList[0].PlayOneShot(_audioSourcesList[0].clip);

        _isShooting = true;
        
        _xrInputDetector.HapticFeedBack.ControllerVibration(1f, 0.1f);
    }

    private IEnumerator GlowBack_Coroutine()
    {
        float lerpTime = 20f;
        
        float timeBack = 0f;

        while (_weaponBolt.XRSlider.value != 0f)
        {
            _weaponBolt.XRSlider.value = Mathf.Lerp(_weaponBolt.XRSlider.value, 0f, timeBack);

            timeBack += lerpTime * Time.deltaTime;
            yield return null;
        }

        if (_magazine?.CurrentAmmoInMagazine == 0 || _magazine == null)
        {
            _weaponBolt.XRSlider.value = 0f;
            Invoke(nameof(RecolocateWeaponBolt), 0.1f);
            yield break;
        }

        float timeFront = 0f;
        
        while (Math.Abs(_weaponBolt.XRSlider.value - 1f) > 0f)
        {
            _weaponBolt.XRSlider.value = Mathf.Lerp(_weaponBolt.XRSlider.value, 1f, timeFront);
            
            timeFront += lerpTime * Time.deltaTime;
            yield return null;
        }
    }

    private void RecolocateWeaponBolt()
    {
        _weaponBolt.XRSlider.value = 0.1f;
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
        _weaponMagazineDetector.AudioSourcesList[1].PlayOneShot(_weaponMagazineDetector.AudioSourcesList[1].clip);
        
        _magazine.Collider.isTrigger = false;
        _magazine.Rigidbody.isKinematic = false;
        _magazine.transform.parent = null;
        _weaponMagazineDetector.XRSlider.MHandle = null;
        _magazine = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Untagged"))
        {
            _audioSourcesList[2].PlayOneShot(_audioSourcesList[2].clip);
        }
    }
}
