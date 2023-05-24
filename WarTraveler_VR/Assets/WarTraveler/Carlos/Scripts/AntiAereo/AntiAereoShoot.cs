using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AntiAereoShoot : MonoBehaviour
{
    [SerializeField] private Turret_XR_GrabInteractableTwoHanded _turretXrGrabInteractableTwoHanded;

    [SerializeField] private XR_InputDetector _leftInputDetector;
    [SerializeField] private XR_InputDetector _rightInputDetector;
    
    [Header("--- MACHINE GUN PARAMS ---")]
    [Space(10)]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletImpulseForce;
    [SerializeField] private float _fireRate; 
    private float _fireRateTime;
    [SerializeField] private Transform _firstMachineGunCanon;
    [SerializeField] private ParticleSystem _firstMachineGunParticleSystem;
    [SerializeField] private Transform _secondMachineGunCanon;
    [SerializeField] private ParticleSystem _secondMachineGunParticleSystem;
    
    [Header("--- CANON PARAMS ---")]
    [Space(10)]
    [SerializeField] private GameObject _canonBulletPrefab;
    [SerializeField] private float _canonBulletImpulseForce;
    [SerializeField] private Transform _firstCanon;
    [SerializeField] private ParticleSystem _firstCanonParticleSystem;
    [SerializeField] private Transform _secondCanon;
    [SerializeField] private ParticleSystem _secondCanonParticleSystem;

    private void Awake()
    {
        _turretXrGrabInteractableTwoHanded = GetComponent<Turret_XR_GrabInteractableTwoHanded>();
        
        _leftInputDetector = _turretXrGrabInteractableTwoHanded.LeftController.GetComponent<XR_InputDetector>();
        _rightInputDetector = _turretXrGrabInteractableTwoHanded.RightController.GetComponent<XR_InputDetector>();
        
        _firstCanonParticleSystem = _firstCanon.GetComponentInChildren<ParticleSystem>();
        _secondCanonParticleSystem = _secondCanon.GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        LeftMachineGunShoot();
        RightMachineGunShoot();
        
        LeftCanonShoot();
        RightCanonShoot();
    }
    
    private void LeftMachineGunShoot()
    {
        if (_leftInputDetector.IsTriggering && _turretXrGrabInteractableTwoHanded.IsLeftGrab)
        {
            ChooseMachineGunShoot(_firstMachineGunCanon, _firstMachineGunParticleSystem);
        }
    }

    private void RightMachineGunShoot()
    {
        if (_rightInputDetector.IsTriggering && _turretXrGrabInteractableTwoHanded.IsRightGrab)
        {
            ChooseMachineGunShoot(_secondMachineGunCanon, _secondMachineGunParticleSystem);
        }
    }

    private void ChooseMachineGunShoot(Transform machineGunCanon, ParticleSystem machineGunParticleSystem)
    {
        if (Time.time > _fireRateTime)
        {
            GameObject bullet = Instantiate(_bulletPrefab, machineGunCanon.position, machineGunCanon.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(machineGunCanon.forward * _bulletImpulseForce, ForceMode.Impulse);
            
            machineGunParticleSystem.Play();

            _fireRateTime = Time.time + _fireRate;
        }
    }

    private void LeftCanonShoot()
    {
        if (_leftInputDetector.PrimaryButton.action.triggered && _turretXrGrabInteractableTwoHanded.IsLeftGrab)
        {
            ChooseMachineGunShoot(_firstCanon, _firstCanonParticleSystem);
        }
    }

    private void RightCanonShoot()
    {
        if (_rightInputDetector.PrimaryButton.action.triggered && _turretXrGrabInteractableTwoHanded.IsRightGrab)
        {
            ChooseCanonShoot(_secondCanon, _secondCanonParticleSystem);
        }
    }

    private void ChooseCanonShoot(Transform canon, ParticleSystem canonParticleSystem)
    {
        GameObject bullet = Instantiate(_canonBulletPrefab, canon.position, canon.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(canon.forward * _canonBulletImpulseForce, ForceMode.Impulse);
            
        canonParticleSystem.Play();
    }
}
