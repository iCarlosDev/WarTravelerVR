using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerScriptStorage : MonoBehaviour
{
    [SerializeField] private ActionBasedContinuousMoveProvider _actionBasedContinuousMoveProvider;
    [SerializeField] private ActionBasedSnapTurnProvider _actionBasedSnapTurnProvider;
    
    [SerializeField] private TransitionsManager _transitionsManager;
    
    //GETTERS && SETTERS//
    public ActionBasedContinuousMoveProvider ActionBasedContinuousMoveProvider => _actionBasedContinuousMoveProvider;
    public ActionBasedSnapTurnProvider ActionBasedSnapTurnProvider => _actionBasedSnapTurnProvider;
    public TransitionsManager TransitionsManager => _transitionsManager;

    //////////////////////////////////////

    private void Awake()
    {
        _actionBasedContinuousMoveProvider = GetComponent<ActionBasedContinuousMoveProvider>();
        _actionBasedSnapTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();

        _transitionsManager = GetComponentInChildren<TransitionsManager>();
    }
}
