using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerScriptStorage : MonoBehaviour
{
    [Header("--- COMPONENTS ---")]
    [Space(10)]
    [SerializeField] private CharacterController _characterController;
    
    [Header("--- SCRIPTS ---")]
    [Space(10)]
    [SerializeField] private ActionBasedContinuousMoveProvider _actionBasedContinuousMoveProvider;
    [SerializeField] private ActionBasedSnapTurnProvider _actionBasedSnapTurnProvider;
    
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private TakePouchAmmo _takePouchAmmo;

    [SerializeField] private XR_InputDetector _leftInputDetector;
    [SerializeField] private XR_InputDetector _rightInputDetector;
    
    [SerializeField] private TransitionsManager _transitionsManager;

    //GETTERS && SETTERS//
    public CharacterController CharacterController => _characterController;

    public ActionBasedContinuousMoveProvider ActionBasedContinuousMoveProvider => _actionBasedContinuousMoveProvider;
    public ActionBasedSnapTurnProvider ActionBasedSnapTurnProvider => _actionBasedSnapTurnProvider;
    
    public PlayerInventory PlayerInventory => _playerInventory;
    public TakePouchAmmo TakePouchAmmo => _takePouchAmmo;

    public XR_InputDetector LeftInputDetector => _leftInputDetector;
    public XR_InputDetector RightInputDetector => _rightInputDetector;

    public TransitionsManager TransitionsManager => _transitionsManager;

    //////////////////////////////////////

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        
        _actionBasedContinuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        _actionBasedSnapTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();

        _playerInventory = GetComponent<PlayerInventory>();
        _takePouchAmmo = GetComponentInChildren<TakePouchAmmo>();

        _transitionsManager = GetComponentInChildren<TransitionsManager>();

        List<XR_InputDetector> xrInputDetectors = new List<XR_InputDetector>();
        xrInputDetectors.AddRange(GetComponentsInChildren<XR_InputDetector>());

        foreach (XR_InputDetector xrInputDetector in xrInputDetectors)
        {
            if (xrInputDetector.CompareTag("LeftHand"))
            {
                _leftInputDetector = xrInputDetector;
            }
            else if (xrInputDetector.CompareTag("RightHand"))
            {
                _rightInputDetector = xrInputDetector;
            }
        }
    }
}
