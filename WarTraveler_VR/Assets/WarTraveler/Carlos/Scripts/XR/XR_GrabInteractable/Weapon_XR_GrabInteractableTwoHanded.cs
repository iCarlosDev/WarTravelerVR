using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon_XR_GrabInteractableTwoHanded : XRGrabInteractable
{
    [Space(20)]
    
    [Header("--- MY SCRIPT ---")]
    [Space(10)]
    [SerializeField] private bool _firstGrab;
    [SerializeField] private bool _secondGrab;

    [Header("--- ATTACHABLE HANDS ---")]
    [Space(10)]
    [SerializeField] private GameObject _firstLeftHandPose;
    [SerializeField] private GameObject _firstRightHandPose;
    [SerializeField] private GameObject _secondLeftHandPose;
    [SerializeField] private GameObject _secondRightHandPose;

    [Header("--- CONTROLLERS ---")] 
    [Space(10)] 
    [SerializeField] private XR_InputDetector _leftInputDetector;
    [SerializeField] private XR_InputDetector _rightInputDetector;

    [Header("--- OTHER ---")] 
    [Space(10)] 
    [SerializeField] private Weapon _weapon;

    //GETTERS && SETTERS//
    public bool FirstGrab
    {
        get => _firstGrab;
        set => _firstGrab = value;
    }
    public bool SecondGrab
    {
        get => _secondGrab;
        set => _secondGrab = value;
    }
    public GameObject FirstLeftHandPose => _firstLeftHandPose;
    public GameObject FirstRightHandPose => _firstRightHandPose;
    public GameObject SecondLeftHandPose => _secondLeftHandPose;
    public GameObject SecondRightHandPose => _secondRightHandPose;
    
    ////////////////////////////////////////////////////////////////////////////

    protected override void Awake()
    {
        base.Awake();
        
        Destroy(_firstLeftHandPose.GetComponentInChildren<MeshCollider>());
        Destroy(_firstRightHandPose.GetComponentInChildren<MeshCollider>());
        Destroy(_secondLeftHandPose.GetComponentInChildren<MeshCollider>());
        Destroy(_secondRightHandPose.GetComponentInChildren<MeshCollider>());
        
        _weapon = GetComponent<Weapon>();
    }

    private void Start()
    {
        _firstLeftHandPose.SetActive(false);
        _firstRightHandPose.SetActive(false);
        
        if (_secondLeftHandPose != null)
        {
            _secondLeftHandPose.SetActive(false);
        }
        
        if (_secondRightHandPose != null)
        {
            _secondRightHandPose.SetActive(false);
        }

        XR_InputDetector[] inputDetectors = FindObjectsOfType<XR_InputDetector>();

        foreach (XR_InputDetector inputDetector in inputDetectors)
        {
            if (inputDetector.CompareTag("LeftHand"))
            {
                _leftInputDetector = inputDetector;
            }
            else if (inputDetector.CompareTag("RightHand"))
            {
                _rightInputDetector = inputDetector;
            }
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        ControllerGrabCheck(args);

        if (_weapon != null && !args.interactorObject.transform.CompareTag("Socket"))
        {
            args.interactorObject.transform.GetComponent<XR_InputDetector>().WeaponGrabbed = this;
            TakePouchAmmo.instance.GrabbedWeaponsList.Add(_weapon);

            AudioManager.instance.PlayOneShot("GrabWeapon");
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        InteractableExited(args);
        
        if (_weapon != null && !args.interactorObject.transform.CompareTag("Socket"))
        {
            args.interactorObject.transform.GetComponent<XR_InputDetector>().WeaponGrabbed = null;
            TakePouchAmmo.instance.GrabbedWeaponsList.Remove(_weapon);
        }
    }
    
    private void ControllerGrabCheck(SelectEnterEventArgs args)
    {
        if (!_firstGrab)
        {
            OnControllerEnter(args.interactorObject.transform.GetComponent<XR_InputDetector>());
            
            if (args.interactorObject.transform.CompareTag("LeftHand"))
            { 
                _firstLeftHandPose.SetActive(true);
            }
            else
            { 
                _firstRightHandPose.SetActive(true);
            }
        }
        else
        {
            if (args.interactorObject.transform.CompareTag("LeftHand"))
            {
                _secondLeftHandPose.SetActive(true);
            }
            else
            {
                _secondRightHandPose.SetActive(true);
            }
        }
        
        if (_firstGrab) _secondGrab = true;
        
        _firstGrab = true;
    }

    private void InteractableExited(SelectExitEventArgs args)
    {
        if (!_secondGrab)
        {
            ControllerExitCheck(args);
            return;
        }

        switch (args.interactorObject.transform.tag)
        {
            case "LeftHand":
                if (_secondLeftHandPose.activeSelf)
                {
                    _secondLeftHandPose.SetActive(false);
                }
                else if (_firstLeftHandPose.activeSelf)
                {
                    _firstLeftHandPose.SetActive(false);
                    _secondRightHandPose.SetActive(false);
                    _firstRightHandPose.SetActive(true);
                }
                
                OnControllerEnter(_rightInputDetector);
                break;

            case "RightHand":
                if (_secondRightHandPose.activeSelf)
                {
                    _secondRightHandPose.SetActive(false);
                }
                else if (_firstRightHandPose.activeSelf)
                {
                    _firstRightHandPose.SetActive(false);
                    _secondLeftHandPose.SetActive(false);
                    _firstLeftHandPose.SetActive(true);
                }
                
                OnControllerEnter(_leftInputDetector);
                break;
        }
        _secondGrab = false;
    }

    private void ControllerExitCheck(SelectExitEventArgs args)
    {
        if (isSelected) return;

        if (args.interactorObject.transform.CompareTag("LeftHand"))
        {
            _firstLeftHandPose.SetActive(false);
        }
        else
        {
            _firstRightHandPose.SetActive(false);
        }
    }

    public void OnControllerEnter(XR_InputDetector xrInputDetector)
    {
        _weapon.XRInputDetector = xrInputDetector;
    }

    public void OnControllerExited()
    {
        _firstGrab = false;
        _weapon.XRInputDetector = null;
    }
}
