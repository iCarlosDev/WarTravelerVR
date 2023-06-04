using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_InputDetector : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor _interactor;
    [SerializeField] private HapticFeedBack _hapticFeedBack;
    
    [Header("--- TRIGGER INPUT ---")]
    [Space(10)]
    [SerializeField] private InputActionReference _triggerInput;
    [SerializeField] private bool _isTriggering;
    
    [Header("--- GRAB INPUT ---")]
    [Space(10)]
    [SerializeField] private InputActionReference _grabInput;
    [SerializeField] private bool _isGrabbing;
    
    [Header("--- JOISTICK BUTTON ---")]
    [Space(10)]
    [SerializeField] private InputActionReference _joistickInput;
    [SerializeField] private bool _joistickPressed;
    
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
    [SerializeField] private float _lerpTime;
    [SerializeField] private float _time;
    [SerializeField] private bool _grabbingSlider;

    //GETTERS && SETTERS//
    public HapticFeedBack HapticFeedBack => _hapticFeedBack;
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
    public bool IsGrabbing => _isGrabbing;
    public bool JoistickPressed => _joistickPressed;
    public InputActionReference JoistickInput => _joistickInput;
    public InputActionReference PrimaryButton => _primaryButton;
    public InputActionReference SecondaryButton => _secondaryButton;
    
    /////////////////////////////////////////////////////

    private void Awake()
    {
        _interactor = GetComponent<XRBaseInteractor>();
        _hapticFeedBack = GetComponent<HapticFeedBack>();
    }

    private void Update()
    {
        CheckInputs();
        
       //Si dejamos de apretar el trigger se cumplirá la condición;
        if (!_isTriggering)
        {
            //Solo si estámos agarrando un objeto podremos soltarlo;
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
        #region - TRIGGER -

        if (_triggerInput.action.IsPressed())
        {
            _isTriggering = true;
        }
        else
        {
            _isTriggering = false;
            _grabbingSlider = false;
        }

        #endregion

        #region - PRIMARY BUTTON -

        _primaryButtonWasPressed = _primaryButton.action.IsPressed();

        #endregion

        #region - GRIP -

        _isGrabbing = _grabInput.action.IsPressed();

        #endregion

        #region - JOISTICK -

        _joistickPressed = _joistickInput.action.IsPressed();

        #endregion
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
        
        AudioManager.instance.PlayOneShot("GrabWeapon");
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

        //Cuando haya vuelto a su posición inicial y no lo estemos agarrando se cumplirá la condición;
        if (_weaponSliderGrabbed.value > 1f && !_isTriggering)
        {
            _weaponSliderGrabbed = null;
            _time = 0f;
            return;
        }

        //Una vez soltemos el slider, este volverá a su posición inicial (como mecanismo de muelle);
        if (!_isTriggering)
        {
            _weaponSliderGrabbed.value = Mathf.Lerp(_weaponSliderGrabbed.value, 1.5f, _time);

            _time += _lerpTime * Time.deltaTime;
            
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
            //Solo si no estámos agarrando nada podremos interactuar con los sliders;
            if (_grabbingSlider || _isGrabbing) return;
            
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