using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AntiAereoShoot : MonoBehaviour
{
    [SerializeField] private XR_GrabInteractableTwoHanded _xrGrabInteractableTwoHanded;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletImpulseForce;
    [SerializeField] private Transform _firstCanon;
    [SerializeField] private ParticleSystem _firstCanonParticleSystem;
    [SerializeField] private Transform _secondCanon;
    [SerializeField] private ParticleSystem _secondCanonParticleSystem;

    private void Awake()
    {
        _xrGrabInteractableTwoHanded = GetComponent<XR_GrabInteractableTwoHanded>();
        _firstCanonParticleSystem = _firstCanon.GetComponentInChildren<ParticleSystem>();
        _secondCanonParticleSystem = _secondCanon.GetComponentInChildren<ParticleSystem>();
    }

    [ContextMenu("Shoot")]
    public void Shoot(ActivateEventArgs args)
    {
        if (args.interactorObject.transform.tag.Equals(_xrGrabInteractableTwoHanded.attachTransform.GetChild(0).tag))
        {
            GameObject bullet = Instantiate(_bulletPrefab, _firstCanon.position, _firstCanon.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(_firstCanon.forward * _bulletImpulseForce, ForceMode.Impulse);
            
            _firstCanonParticleSystem.Play();
        }
        else if (args.interactorObject.transform.tag.Equals(_xrGrabInteractableTwoHanded.secondaryAttachTransform.GetChild(0).tag))
        {
            GameObject bullet = Instantiate(_bulletPrefab, _secondCanon.position, _firstCanon.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(_secondCanon.forward * _bulletImpulseForce, ForceMode.Impulse);
            
            _secondCanonParticleSystem.Play();
        }
    }
}
