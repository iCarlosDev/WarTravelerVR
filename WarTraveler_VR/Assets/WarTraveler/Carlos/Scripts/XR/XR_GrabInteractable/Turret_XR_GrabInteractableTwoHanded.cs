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
    [SerializeField] private GameObject _leftHandPose;
    [SerializeField] private GameObject _rightHandPose;
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
        _leftController = GameObject.Find("XR Origin/Camera Offset/LeftHand Controller").GetComponent<XRBaseController>();
        _rightController = GameObject.Find("XR Origin/Camera Offset/RightHand Controller").GetComponent<XRBaseController>();
    }

    private void Start()
    {
        _leftHandPose.SetActive(false);
        _rightHandPose.SetActive(false);
        
        _firstTransformPosition = transform.position;
        _firstTransformRotation = transform.rotation;
    }

    private void Update()
    {
        if (!_isLeftGrab && !_isRightGrab)
        {
            ResetPosition();
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

    /// <summary>
    /// Método para resetear la posición del objeto una vez no esté agarrado;
    /// </summary>
    private void ResetPosition()
    {
        if (transform.rotation == _firstTransformRotation) return;
            
        Debug.Log("Reseting Position");
        transform.position = Vector3.Lerp(transform.position, _firstTransformPosition, _timeToReset * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _firstTransformRotation, _timeToReset * Time.deltaTime);
    }
    
    /// <summary>
    /// Método para saber que mano ha agarrado el objeto y así hacerla aparecer;
    /// </summary>
    /// <param name="args"></param>
    private void ControllerGrabCheck(SelectEnterEventArgs args)
    {
        PlayerScriptStorage playerScriptStorage = args.interactorObject.transform.GetComponentInParent<PlayerScriptStorage>();
        
        if (args.interactorObject.transform.CompareTag("LeftHand"))
        {
            _leftHandPose.SetActive(true);
            _isLeftGrab = true;
        }
        else
        {
            _rightHandPose.SetActive(true);
            _isRightGrab = true;
        }

        if (playerScriptStorage != null)
        {
            playerScriptStorage.TransitionsManager.DisablePlayerMovement();
        }
    }

    /// <summary>
    /// Método para saber que mano se ha soltado y así hacerla desaparecer;
    /// </summary>
    /// <param name="args"></param>
    private void ControllerExitCheck(SelectExitEventArgs args)
    {
        PlayerScriptStorage playerScriptStorage = args.interactorObject.transform.GetComponentInParent<PlayerScriptStorage>();
        
        if (args.interactorObject.transform.CompareTag("LeftHand"))
        {
            _leftHandPose.SetActive(false);
            _isLeftGrab = false;
        }
        else
        {
            _rightHandPose.SetActive(false);
            _isRightGrab = false;
        }

        if (playerScriptStorage != null)
        {
            if (!_isLeftGrab && !_isRightGrab)
            {
                playerScriptStorage.TransitionsManager.EnablePlayerMovement();
            }
        }
    }

    /// <summary>
    /// Método para controlar el objeto con la mano izquierda;
    /// </summary>
    private void ControlRotationByLeftPosition()
    {
        if (!TrackRotationByPosition) return;

        Vector3 direction = ((_leftController.transform.position - _firstAttachOffset) - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Método para controlar el objeto con la mano derecha;
    /// </summary>
    private void ControlRotationByRightPosition()
    {
        if (!TrackRotationByPosition) return;

        Vector3 direction = ((_rightController.transform.position - _secondAttachOffset) - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
