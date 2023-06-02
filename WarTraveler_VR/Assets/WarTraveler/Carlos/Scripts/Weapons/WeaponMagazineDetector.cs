using System;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class WeaponMagazineDetector : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private XR_InputDetector _xrInputDetector;
    [SerializeField] private XR_Slider _xrSlider;
    [SerializeField] private XR_TriggerGrabbable _magazine;
    [SerializeField] private float _magazineX_RotationOffset;
    [SerializeField] private bool quieroSalir;

    public XR_Slider XRSlider => _xrSlider;

    private void Awake()
    {
        _weapon = GetComponentInParent<Weapon>();
        _xrSlider = GetComponent<XR_Slider>();
    }

    private void Update()
    {
        if (_xrSlider.value >= 1f && _magazine != null && _xrSlider.MHandle != null)
        {
            if (_xrSlider.isSelected)
            {
                _xrInputDetector.ReleaseSlider();
                _xrSlider.interactionManager.SelectExit(_xrSlider.firstInteractorSelecting, _xrSlider);
            }
            
            _weapon.InsertMagazine(_magazine.GetComponent<Magazine>());
        }

        if (transform.childCount == 0 && _xrSlider.MHandle != null)
        {
            quieroSalir = true;
            _xrSlider.MHandle = null;
            _magazine = null;
            _weapon.DropMagazine();
        }
    }

    private void DetectMagazineToInsert(Collider other)
    {
        _magazine = other.GetComponent<XR_TriggerGrabbable>();

        //Si el cargador y el detector de cargador no tienen la misma layer no podrémos usarlo;
        if (_xrSlider.interactionLayers.value != _magazine.interactionLayers.value)
        {
            _magazine = null;
            return;
        }
        
        Magazine magazine = _magazine.GetComponent<Magazine>();

        magazine.IsBeingInserted = true;
        
        _xrInputDetector = _magazine.firstInteractorSelecting.transform.GetComponent<XR_InputDetector>();
        _xrInputDetector.DropTriggeredObject();
        
        magazine.Collider.isTrigger = true;
        magazine.Rigidbody.isKinematic = true;

        _magazine.transform.parent = transform;
        _magazine.transform.localPosition = Vector3.zero;
            
        Quaternion magazineRotation = Quaternion.Euler(_magazineX_RotationOffset, 0f, 0f);
        _magazine.transform.localRotation = magazineRotation;

        _xrSlider.MHandle = _magazine.transform;
        _xrInputDetector.GrabSlider(_xrSlider);
    }

    private void NoQuererSalir()
    {
        quieroSalir = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (quieroSalir) return;

        if (other.CompareTag("WeaponMagazine") && _xrSlider.MHandle == null)
        {
            if (!other.GetComponent<XR_TriggerGrabbable>().isSelected) return;
            
            DetectMagazineToInsert(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("WeaponMagazine"))
        {
           Invoke(nameof(NoQuererSalir), 1f);
        }
    }
}
