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
        CanonShoot();
    }

    [ContextMenu("Shoot")]
    public void MachineGunShoot(ActivateEventArgs args)
    {
        if (args.interactorObject.transform.tag.Equals(_turretXrGrabInteractableTwoHanded.LeftController.tag))
        {
            GameObject bullet = Instantiate(_bulletPrefab, _firstMachineGunCanon.position, _firstMachineGunCanon.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(_firstMachineGunCanon.forward * _bulletImpulseForce, ForceMode.Impulse);
            
            _firstMachineGunParticleSystem.Play();
        }
        else
        {
            GameObject bullet = Instantiate(_bulletPrefab, _secondMachineGunCanon.position, _secondMachineGunCanon.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(_secondMachineGunCanon.forward * _bulletImpulseForce, ForceMode.Impulse);
            
            _secondMachineGunParticleSystem.Play();
        }
    }

    private void CanonShoot()
    {
        if (_leftInputDetector.PrimaryButton.action.triggered && _turretXrGrabInteractableTwoHanded.IsLeftGrab)
        {
            GameObject bullet = Instantiate(_canonBulletPrefab, _firstCanon.position, _firstCanon.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(_firstCanon.forward * _canonBulletImpulseForce, ForceMode.Impulse);
            
            _firstCanonParticleSystem.Play();
        }
        else if (_rightInputDetector.PrimaryButton.action.triggered && _turretXrGrabInteractableTwoHanded.IsRightGrab)
        {
            GameObject bullet = Instantiate(_canonBulletPrefab, _secondCanon.position, _secondCanon.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(_firstCanon.forward * _canonBulletImpulseForce, ForceMode.Impulse);
            
            _secondCanonParticleSystem.Play();
        }
    }
}
