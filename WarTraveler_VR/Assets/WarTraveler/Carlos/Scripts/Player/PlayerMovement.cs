using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerScriptStorage _playerScriptStorage;

    [SerializeField] private Vector2 _notMovingRef;
    [SerializeField] private bool _isSprinting;

    private void Awake()
    {
        _playerScriptStorage = GetComponent<PlayerScriptStorage>();
    }

    private void Update()
    {
        if (_playerScriptStorage.TransitionsManager.InTransition)
        {
            _isSprinting = false;
            return;
        }

        if (_playerScriptStorage.ActionBasedContinuousMoveProvider.leftHandMoveAction.action.ReadValue<Vector2>().Equals(_notMovingRef) && _isSprinting) DisableSprint();

        if (!_playerScriptStorage.LeftInputDetector.JoistickInput.action.triggered) return;

        if (!_isSprinting)
        {
            ActivateSprint();
        }
        else
        {
            DisableSprint();
        }
    }

    private void ActivateSprint()
    {
        if (_isSprinting) return;

        _playerScriptStorage.ActionBasedContinuousMoveProvider.moveSpeed = 3.2f;

        if (IsInvoking(nameof(DisableSprint))) CancelInvoke(nameof(DisableSprint));
        Invoke(nameof(DisableSprint), 4f);
        
        _isSprinting = true;
    }

    private void DisableSprint()
    {
        if (_playerScriptStorage.TransitionsManager.InTransition)
        {
            _isSprinting = false;
            return;
        }
        
        _playerScriptStorage.ActionBasedContinuousMoveProvider.moveSpeed = 2f;
        _isSprinting = false;
    }
}
