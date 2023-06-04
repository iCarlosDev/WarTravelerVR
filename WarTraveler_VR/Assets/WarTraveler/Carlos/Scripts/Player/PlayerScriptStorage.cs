using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerScriptStorage : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    
    [SerializeField] private ActionBasedContinuousMoveProvider _actionBasedContinuousMoveProvider;
    [SerializeField] private ActionBasedSnapTurnProvider _actionBasedSnapTurnProvider;

    [SerializeField] private XR_InputDetector _leftInputDetector;
    [SerializeField] private XR_InputDetector _rightInputDetector;
    
    [SerializeField] private TransitionsManager _transitionsManager;

    //GETTERS && SETTERS//
    public CharacterController CharacterController => _characterController;

    public ActionBasedContinuousMoveProvider ActionBasedContinuousMoveProvider => _actionBasedContinuousMoveProvider;
    public ActionBasedSnapTurnProvider ActionBasedSnapTurnProvider => _actionBasedSnapTurnProvider;

    public XR_InputDetector LeftInputDetector => _leftInputDetector;
    public XR_InputDetector RightInputDetector => _rightInputDetector;

    public TransitionsManager TransitionsManager => _transitionsManager;

    //////////////////////////////////////

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        
        _actionBasedContinuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        _actionBasedSnapTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();

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
