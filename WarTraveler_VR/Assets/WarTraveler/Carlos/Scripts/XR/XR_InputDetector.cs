using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_InputDetector : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor _interactor;
    
    [Header("--- TRIGGER INPUT ---")]
    [Space(10)]
    [SerializeField] private InputActionReference _triggerInput;
    [SerializeField] private bool _isTriggering;
    
    [Header("--- PRIMARY BUTTON ---")]
    [Space(10)]
    [SerializeField] private InputActionReference _primaryButton;
    [SerializeField] private bool _primaryButtonWasPressed;

    [Header("--- SECONDARY BUTTON ---")]
    [Space(10)]
    [SerializeField] private InputActionReference _secondaryButton;

    [Header("--- WEAPON ---")]
    [Space(10)]
    [SerializeField] private Weapon_XR_GrabInteractableTwoHanded _weaponGrabbed;
    
    [Header("--- OBJECT TRIGGEREABLE ---")]
    [Space(10)]
    [SerializeField] private XR_TriggerGrabbable _objectGrabbed;
    
    [Header("--- SLIDER ---")]
    [Space(10)]
    [SerializeField] private XR_Slider _weaponSliderGrabbed;
    [SerializeField] private bool _grabbingSlider;

    //GETTERS && SETTERS//
    public Weapon_XR_GrabInteractableTwoHanded WeaponGrabbed
    {
        get => _weaponGrabbed;
        set => _weaponGrabbed = value;
    }
    public XR_TriggerGrabbable ObjectGrabbed
    {
        get => _objectGrabbed;
        set => _objectGrabbed = value;
    }
    public bool IsTriggering => _isTriggering;
    public InputActionReference PrimaryButton => _primaryButton;
    public InputActionReference SecondaryButton => _secondaryButton;
    
    /////////////////////////////////////////////////////

    private void Awake()
    {
        _interactor = GetComponent<XRBaseInteractor>();
    }

    private void Update()
    {
        CheckInputs();
        
       //Si dejamos de apretar el trigger se cumplirá la condición;
        if (!_isTriggering)
        {
            //Si estamos agarrando un objeto...;
            if (_objectGrabbed != null)
            {
                DropTriggeredObject();      
            }
            
            ReleaseSlider();
        }
    }

    /// <summary>
    /// Método para detectar que Inputs del Mando se están ejecutando;
    /// </summary>
    private void CheckInputs()
    {
        if (_triggerInput.action.ReadValue<float>() > 0.3f)
        {
            _isTriggering = true;
        }
        else if (_triggerInput.action.ReadValue<float>() <= 0.1f)
        {
            _isTriggering = false;
            _grabbingSlider = false;
        }

        if (_primaryButton.action.IsPressed())
        {
            _primaryButtonWasPressed = true;
        }
        else
        {
            _primaryButtonWasPressed = false;
        }
    }
    
    /// <summary>
    /// Método para Agarrar los objetos que hayan en la escena con el Trigger;
    /// </summary>
    /// <param name="grabInteractable"></param>
    public void GrabTriggerObject(XR_TriggerGrabbable triggerGrabbable)
    {
        if (triggerGrabbable == null) return;

        _objectGrabbed = triggerGrabbable;
        
        _interactor.IsSelecting(triggerGrabbable);
        _interactor.interactionManager.SelectEnter(_interactor, triggerGrabbable);
    }

    /// <summary>
    /// Método para soltar el objeto que tengamos agarrado con el trigger;
    /// </summary>
    public void DropTriggeredObject()
    {
        _objectGrabbed.interactionManager.SelectExit(_interactor, _objectGrabbed);
        _objectGrabbed.GetComponent<Rigidbody>().isKinematic = false;
        _objectGrabbed = null;
    }

    #region - SLIDER -

    /// <summary>
    /// Método para Agarrar el Slider de las Armas que hayan en la escena con el Trigger;
    /// </summary>
    /// <param name="grabInteractable"></param>
    public void GrabSlider(XR_Slider weaponSlider)
    {
        if (weaponSlider == null) return;

        _weaponSliderGrabbed = weaponSlider;
        
        _interactor.IsSelecting(_weaponSliderGrabbed);
        _interactor.interactionManager.SelectEnter(_interactor, _weaponSliderGrabbed);
        _grabbingSlider = true;
    }

    /// <summary>
    /// Método para soltar un slider que tengamos agarrado con el trigger;
    /// </summary>
    public void ReleaseSlider()
    {
        if (_weaponSliderGrabbed == null) return;

        if (_weaponSliderGrabbed.value > 1f && !_isTriggering)
        {
            _weaponSliderGrabbed = null;
            return;
        }

        if (!_isTriggering)
        {
            _weaponSliderGrabbed.value = Mathf.Lerp(_weaponSliderGrabbed.value, 1.5f, 20f * Time.deltaTime);

            if (_weaponSliderGrabbed.isSelected)
            {
                _weaponSliderGrabbed.interactionManager.SelectExit(_interactor, _weaponSliderGrabbed);   
            }
        }
    }

    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (!_isTriggering) return;
        
        if (_objectGrabbed == null)
        {
            if (_grabbingSlider) return;
            
            if (other.CompareTag("WeaponMagazine"))
            {
                if (other.GetComponent<Magazine>().IsBeingInserted) return;
               
                GrabTriggerObject(other.GetComponent<XR_TriggerGrabbable>());
            }

            if (other.CompareTag("WeaponSlide"))
            {
                GrabSlider(other.GetComponent<XR_Slider>());
            }
        }
    }
}