using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Turret_XR_GrabInteractableTwoHanded : XRGrabInteractable
{
    [Space(20)] [Header("--- MY SCRIPT ---")] 
    [Space(10)] 
    [SerializeField] private Vector3 _firstTransformPosition;
    [SerializeField] private Quaternion _firstTransformRotation;
    [SerializeField] private float _timeToReset;

    [Header("--- ATTACHABLE HANDS ---")]
    [Space(10)]
    [SerializeField] private GameObject leftHandPrefabInstantiate;
    [SerializeField] private GameObject rightHandPrefabInstantiate;
    [SerializeField] private XRBaseController _leftController;
    [SerializeField] private XRBaseController _rightController;
    [SerializeField] private Vector3 _firstAttachOffset;
    [SerializeField] private Vector3 _secondAttachOffset;
    [SerializeField] private bool _isLeftGrab;
    [SerializeField] private bool _isRightGrab;
    
    [Header("--- ROTATION CONTROL ---")]
    [Space(10)]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool TrackRotationByPosition;

    //GETTERS && SETTERS//
    public XRBaseController LeftController => _leftController;
    public XRBaseController RightController => _rightController;
    public bool IsLeftGrab => _isLeftGrab;
    public bool IsRightGrab => _isRightGrab;
    
    /////////////////////////////////////////////////////////////////////

    protected override void Awake()
    {
        base.Awake();
        _leftController = GameObject.FindWithTag("LeftHand").GetComponent<XRBaseController>();
        _rightController = GameObject.FindWithTag("RightHand").GetComponent<XRBaseController>();
    }

    private void Start()
    {
        _firstTransformPosition = transform.position;
        _firstTransformRotation = transform.rotation;
    }

    private void Update()
    {
        if (!_isLeftGrab && !_isRightGrab)
        {
            if (transform.rotation == _firstTransformRotation) return;
            
            Debug.Log("Reseting Position");
            transform.position = Vector3.Lerp(transform.position, _firstTransformPosition, _timeToReset * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, _firstTransformRotation, _timeToReset * Time.deltaTime);
            return;
        }
        
        if (_isLeftGrab)
        {
            ControlRotationByLeftPosition();
        }

        if (_isRightGrab)
        {
            ControlRotationByRightPosition();
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        ControllerGrabCheck(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        ControllerExitCheck(args);
    }

    private void ControllerGrabCheck(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.tag.Equals(_leftController.tag))
        {
            GameObject firstHand = Instantiate(_leftController.modelPrefab.gameObject, attachTransform);
            leftHandPrefabInstantiate = firstHand;
            _isLeftGrab = true;
        }
        else
        {
            GameObject secondHand = Instantiate(_rightController.modelPrefab.gameObject, secondaryAttachTransform);
            rightHandPrefabInstantiate = secondHand;
            _isRightGrab = true;
        }
    }

    private void ControllerExitCheck(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.tag.Equals(_leftController.tag))
        {
            Destroy(leftHandPrefabInstantiate);
            _isLeftGrab = false;
        }
        else
        {
            Destroy(rightHandPrefabInstantiate);
            _isRightGrab = false;
        }
    }

    private void ControlRotationByLeftPosition()
    {
        if (!TrackRotationByPosition) return;

        Vector3 direction = ((_leftController.transform.position - _firstAttachOffset) - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    private void ControlRotationByRightPosition()
    {
        if (!TrackRotationByPosition) return;

        Vector3 direction = ((_rightController.transform.position - _secondAttachOffset) - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
