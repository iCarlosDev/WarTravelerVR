using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_InputDetector : MonoBehaviour
{
    [SerializeField] private XRBaseInteractor _interactor;
    [SerializeField] private InputActionReference _controllerPosition;
    [SerializeField] private InputActionReference _triggerInput;
    [SerializeField] private InputActionReference _gripInput;
    [SerializeField] private XRGrabInteractable _objectGrabbed;
    [SerializeField] private bool _isTriggering;

    //GETTERS && SETTERS//
    public XRBaseInteractor Interactor => _interactor;
    public InputActionReference ControllerPosition => _controllerPosition;
    public InputActionReference TriggerInput => _triggerInput;
    public InputActionReference GripInput => _gripInput;
    public bool IsTriggering => _isTriggering;

    /////////////////////////////////////////////////////

    private void Awake()
    {
        _interactor = GetComponent<XRBaseInteractor>();
    }

    private void Update()
    {
        //Debug.Log(_controllerPosition.action.ReadValue<Vector3>());
        
        /*if (_triggerInput.action.ReadValue<float>() == 1 && !_isTriggering)
        {
            _isTriggering = true;
        }
        else if (_triggerInput.action.ReadValue<float>() == 0 && _isTriggering)
        {
            _isTriggering = false;
        }*/
        
        DropMagazine();
    }

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
        if (_objectGrabbed != null && _triggerInput.action.ReadValue<float>() == 0)
        {
            _objectGrabbed.interactionManager.SelectExit(_interactor, _objectGrabbed);
            _objectGrabbed = null;
            _isTriggering = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isTriggering) return;
        
        if (_triggerInput.action.ReadValue<float>() == 1 && !_isTriggering)
        {
            if (other.CompareTag("AmmoPouch"))
            {
                InstantiateAndGrabMagazine();
            }

            if (other.CompareTag("WeaponMagazine"))
            {
            
                GrabMagazine(other.GetComponent<XRGrabInteractable>());
            }
        }
    }
}