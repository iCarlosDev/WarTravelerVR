using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_InputDetector : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor _interactor;
    [SerializeField] private InputActionReference _triggerInput;
    [SerializeField] private InputActionReference _gripInput;
    [SerializeField] private bool _isTriggering;
    
    [Header("--- MAGAZINE ---")]
    [Space(10)]
    [SerializeField] private XRGrabInteractable _objectGrabbed;
    
    [Header("--- SLIDER ---")]
    [Space(10)]
    [SerializeField] private XR_Slider _weaponSliderGrabbed;

    //GETTERS && SETTERS//

    /////////////////////////////////////////////////////

    private void Awake()
    {
        _interactor = GetComponent<XRBaseInteractor>();
    }

    private void Update()
    {
        DropMagazine();
        ReleaseSlider();
    }

    #region - MAGAZINE -
    
    /// <summary>
    /// Método para Instanciar y Agarrar automaticamente un cargador cuando pulsemos el trigger en nuestro "Pouch";
    /// </summary>
    private void InstantiateAndGrabMagazine()
    {
        if (TakePouchAmmo.instance.GrabbedWeaponsList.Count == 0) return;
        
        
        GameObject instantiatedObject = Instantiate(TakePouchAmmo.instance.GrabbedWeaponsList[0].MagazinePrefab, transform.position, transform.rotation);
        XRGrabInteractable grabInteractable = instantiatedObject.GetComponent<XRGrabInteractable>();
        GrabMagazine(grabInteractable);
    }

    /// <summary>
    /// Método para Agarrar los cargadores que hayan en la escena con el Trigger;
    /// </summary>
    /// <param name="grabInteractable"></param>
    private void GrabMagazine(XRGrabInteractable grabInteractable)
    {
        if (grabInteractable == null) return;

        _objectGrabbed = grabInteractable;
        
        _interactor.IsSelecting(grabInteractable);
        _interactor.interactionManager.SelectEnter(_interactor, grabInteractable);
        _isTriggering = true;
    }

    /// <summary>
    /// Método para soltar el cargador que tengamos agarrado con el trigger;
    /// </summary>
    private void DropMagazine()
    {
        if (_objectGrabbed != null && _triggerInput.action.ReadValue<float>() <= 0.1f && _isTriggering)
        {
            _objectGrabbed.interactionManager.SelectExit(_interactor, _objectGrabbed);
            _isTriggering = false;
            _objectGrabbed = null;
        }
    }
    
    #endregion

    #region - SLIDER -

    /// <summary>
    /// Método para Agarrar el Slider de las Armas que hayan en la escena con el Trigger;
    /// </summary>
    /// <param name="grabInteractable"></param>
    private void GrabSlider(XR_Slider weaponSlider)
    {
        if (weaponSlider == null) return;

        _weaponSliderGrabbed = weaponSlider;
        
        _interactor.IsSelecting(_weaponSliderGrabbed);
        _interactor.interactionManager.SelectEnter(_interactor, _weaponSliderGrabbed);
        _isTriggering = true;
    }

    private void ReleaseSlider()
    {
        if (_weaponSliderGrabbed == null) return;

        if (_weaponSliderGrabbed.value == 1f && !_isTriggering) _weaponSliderGrabbed = null;

        if (_triggerInput.action.ReadValue<float>() <= 0.1f)
        {
            _weaponSliderGrabbed.value = Mathf.Lerp(_weaponSliderGrabbed.value, 1f, 20f * Time.deltaTime);
            Debug.Log(_triggerInput.action.ReadValue<float>());
            
            if (_isTriggering)
            {
                _weaponSliderGrabbed.interactionManager.SelectExit(_interactor, _weaponSliderGrabbed);
                _isTriggering = false;
            }
        }
    }

    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (_isTriggering) return;
        
        if (_triggerInput.action.ReadValue<float>() > 0.3f)
        {
            if (!_isTriggering)
            {
                if (other.CompareTag("AmmoPouch"))
                {
                    InstantiateAndGrabMagazine();
                }

                if (other.CompareTag("WeaponMagazine"))
                {
                    GrabMagazine(other.GetComponent<XRGrabInteractable>());
                }

                if (other.CompareTag("WeaponSlide"))
                {
                    GrabSlider(other.GetComponent<XR_Slider>());
                }
            }

            if (other.CompareTag("WeaponInsertMagazineZone") && _objectGrabbed != null)
            {
                if (_weaponSliderGrabbed == null)
                {
                    _weaponSliderGrabbed = other.GetComponent<XR_Slider>();
                }

                if (_weaponSliderGrabbed.MHandle == null) return;
                
                DropMagazine();
                GrabSlider(_weaponSliderGrabbed);
            }
        }
    }
}