using UnityEngine;

public class AntiAereoShoot : MonoBehaviour
{
    [SerializeField] private Turret_XR_GrabInteractableTwoHanded _turretXrGrabInteractableTwoHanded;
    [SerializeField] private AntiAereoAnimations _antiAereoAnimations;

    [SerializeField] private XR_InputDetector _leftInputDetector;
    [SerializeField] private XR_InputDetector _rightInputDetector;

    [Header("--- CANON PARAMS ---")] 
    [Space(10)] 
    [SerializeField] private AudioSource _machineGunAudio;
    [SerializeField] private AudioSource _canonAudio;
    
    [Header("--- OVERHEATING PARAMS ---")] 
    [Space(10)]
    [SerializeField] private float _maxTimeShooting;
    [SerializeField] private float _currentLeftTimeShooting;
    [SerializeField] private float _currentRightTimeShooting;
    [SerializeField] private bool _isLeftOverheated;
    [SerializeField] private bool _isRightOverheated;
    
    [Header("--- MACHINE GUN PARAMS ---")]
    [Space(10)]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletImpulseForce;
    [SerializeField] private float _fireRate; 
    private float _leftFireRateTime;
    private float _rightFireRateTime;
    [SerializeField] private Transform _firstMachineGunCanon;
    [SerializeField] private ParticleSystem _firstMachineGunParticleSystem;
    [SerializeField] private Transform _secondMachineGunCanon;
    [SerializeField] private ParticleSystem _secondMachineGunParticleSystem;
    
    [Header("--- CANON PARAMS ---")]
    [Space(10)]
    [SerializeField] private GameObject _canonBulletPrefab;
    [SerializeField] private float _canonBulletImpulseForce;
    [SerializeField] private Transform _firstCanon;
    [SerializeField] private bool _readyToShootFirstCanon;
    [SerializeField] private ParticleSystem _firstCanonParticleSystem;
    [SerializeField] private Transform _secondCanon;
    [SerializeField] private bool _readyToShootSecondCanon;
    [SerializeField] private ParticleSystem _secondCanonParticleSystem;

    private void Awake()
    {
        _turretXrGrabInteractableTwoHanded = GetComponent<Turret_XR_GrabInteractableTwoHanded>();
        _antiAereoAnimations = GetComponent<AntiAereoAnimations>();
        
        _leftInputDetector = _turretXrGrabInteractableTwoHanded.LeftController.GetComponent<XR_InputDetector>();
        _rightInputDetector = _turretXrGrabInteractableTwoHanded.RightController.GetComponent<XR_InputDetector>();
        
        _firstCanonParticleSystem = _firstCanon.GetComponentInChildren<ParticleSystem>();
        _secondCanonParticleSystem = _secondCanon.GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        _readyToShootFirstCanon = true;
        _readyToShootSecondCanon = true;
    }

    private void Update()
    {
        LeftMachineGunShoot();
        RightMachineGunShoot();
        
        LeftCanonShoot();
        RightCanonShoot();
        
        CheckLeftMachineGunOverheat();
        CheckRightMachineGunOverheat();
    }

    private void CheckLeftMachineGunOverheat()
    {
        if (_leftInputDetector.IsTriggering && LeftMachineGunShoot())
        {
            _currentLeftTimeShooting += Time.deltaTime;
        }
        else
        {
            _currentLeftTimeShooting -= Time.deltaTime;
        }

        _currentLeftTimeShooting = Mathf.Clamp(_currentLeftTimeShooting, 0f, _maxTimeShooting);
        
        if (_currentLeftTimeShooting >= _maxTimeShooting)
        {
            _isLeftOverheated = true;
        }
        else if (_currentLeftTimeShooting <= _maxTimeShooting/2)
        {
            _isLeftOverheated = false;
        }
    }

    private void CheckRightMachineGunOverheat()
    {
        if (_rightInputDetector.IsTriggering && RightMachineGunShoot())
        {
            _currentRightTimeShooting += Time.deltaTime;
        }
        else
        {
            _currentRightTimeShooting -= Time.deltaTime;
        }

        _currentRightTimeShooting = Mathf.Clamp(_currentRightTimeShooting, 0f, _maxTimeShooting);

        if (_currentRightTimeShooting >= _maxTimeShooting)
        {
            _isRightOverheated = true;
        }
        else if (_currentRightTimeShooting <= _maxTimeShooting/2)
        {
            _isRightOverheated = false;
        }
    }
    
    private bool LeftMachineGunShoot()
    {
        if (_isLeftOverheated)
        {
            _antiAereoAnimations.SetBoolLeftAmmoAnimation(false);
            _antiAereoAnimations.SetBoolLeftMachineGun(false);
            return false;
        }

        if (_leftInputDetector.IsTriggering && _turretXrGrabInteractableTwoHanded.IsLeftGrab)
        {
            if (Time.time > _leftFireRateTime)
            {
                GameObject bullet = Instantiate(_bulletPrefab, _firstMachineGunCanon.position, _firstMachineGunCanon.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(_firstMachineGunCanon.forward * _bulletImpulseForce, ForceMode.Impulse);
            
                _firstMachineGunParticleSystem.Play();
                _leftInputDetector.HapticFeedBack.ControllerVibration(1f, 0.1f);

                _leftFireRateTime = Time.time + _fireRate;
                
                _antiAereoAnimations.SetBoolLeftAmmoAnimation(true);
                _antiAereoAnimations.SetBoolLeftMachineGun(true);
                
                AudioManager.instance.PlayOneShot("MachineGunShoot");
            }
            return true;
        }
        
        _antiAereoAnimations.SetBoolLeftAmmoAnimation(false);
        _antiAereoAnimations.SetBoolLeftMachineGun(false);
        return false;
    }

    private bool RightMachineGunShoot()
    {
        if (_isRightOverheated)
        {
            _antiAereoAnimations.SetBoolRightAmmoAnimation(false);
            _antiAereoAnimations.SetBoolRightMachineGun(false);
            return false;
        }
        
        if (_rightInputDetector.IsTriggering && _turretXrGrabInteractableTwoHanded.IsRightGrab)
        {
            if (Time.time > _rightFireRateTime)
            {
                GameObject bullet = Instantiate(_bulletPrefab, _secondMachineGunCanon.position, _secondMachineGunCanon.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(_secondMachineGunCanon.forward * _bulletImpulseForce, ForceMode.Impulse);
            
                _secondMachineGunParticleSystem.Play();
                _rightInputDetector.HapticFeedBack.ControllerVibration(1f, 0.1f);

                _rightFireRateTime = Time.time + _fireRate;
                
                _antiAereoAnimations.SetBoolRightAmmoAnimation(true);
                _antiAereoAnimations.SetBoolRightMachineGun(true);

                if (!LeftMachineGunShoot())
                {
                    AudioManager.instance.PlayOneShot("MachineGunShoot");
                }
            }
            return true;
        }

        _antiAereoAnimations.SetBoolRightAmmoAnimation(false);
        _antiAereoAnimations.SetBoolRightMachineGun(false);
        return false;
    }

    private void LeftCanonShoot()
    {
        if (!_readyToShootFirstCanon) return;

        if (_leftInputDetector.PrimaryButton.action.triggered && _turretXrGrabInteractableTwoHanded.IsLeftGrab)
        {
            ChooseCanonShoot(_firstCanon, _firstCanonParticleSystem);
            _antiAereoAnimations.SetTriggerFirstCanon();
            _readyToShootFirstCanon = false;
            Invoke(nameof(SetReadyToShootFirstCanon), 3f);
        }
    }

    private void RightCanonShoot()
    {
        if (!_readyToShootSecondCanon) return;
        
        if (_rightInputDetector.PrimaryButton.action.triggered && _turretXrGrabInteractableTwoHanded.IsRightGrab)
        {
            ChooseCanonShoot(_secondCanon, _secondCanonParticleSystem);
            _antiAereoAnimations.SetTriggerSecondCanon();
            _readyToShootSecondCanon = false;
            Invoke(nameof(SetReadyToShootSecondCanon), 3f);
        }
    }

    private void ChooseCanonShoot(Transform canon, ParticleSystem canonParticleSystem)
    {
        GameObject bullet = Instantiate(_canonBulletPrefab, canon.position, canon.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(canon.forward * _canonBulletImpulseForce, ForceMode.Impulse);
            
        canonParticleSystem.Play();
    }

    private void SetReadyToShootFirstCanon()
    {
        _readyToShootFirstCanon = true;
    }

    private void SetReadyToShootSecondCanon()
    {
        _readyToShootSecondCanon = true;
    }
}
