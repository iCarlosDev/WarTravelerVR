using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerScriptStorage _playerScriptStorage;

    private void Awake()
    {
        _playerScriptStorage = GetComponent<PlayerScriptStorage>();
    }

    private void Update()
    {
        if (_playerScriptStorage.LeftInputDetector.JoistickPressed && _playerScriptStorage.ActionBasedContinuousMoveProvider.moveSpeed == 5f)
        {
            _playerScriptStorage.ActionBasedContinuousMoveProvider.moveSpeed = 5f;
        }
        else if (_playerScriptStorage.LeftInputDetector.JoistickPressed && _playerScriptStorage.ActionBasedContinuousMoveProvider.moveSpeed == 5f)
        {
            _playerScriptStorage.ActionBasedContinuousMoveProvider.moveSpeed = 2f;
        }
    }
}
