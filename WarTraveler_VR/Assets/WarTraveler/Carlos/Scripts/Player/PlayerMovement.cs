using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerScriptStorage _playerScriptStorage;

    [Header("--- PLAYER MOVEMENT PARAMS ---")]
    [Space(10)]
    [SerializeField] private Vector2 _notMovingVectors;
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

        if (_playerScriptStorage.ActionBasedContinuousMoveProvider.leftHandMoveAction.action.ReadValue<Vector2>().Equals(_notMovingVectors) && _isSprinting) DisableSprint();

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

    /// <summary>
    /// Método para Sprintar;
    /// </summary>
    private void ActivateSprint()
    {
        if (_isSprinting) return; //Si no estámos sprintando, no hacemos la lógica restante;

        //Aumentamos la velocidad del player;
        _playerScriptStorage.ActionBasedContinuousMoveProvider.moveSpeed = 3.2f;

        //Si ya estamos invocando al método de para de sprintar, lo paramos y lo volvemos a llamar;
        if (IsInvoking(nameof(DisableSprint))) CancelInvoke(nameof(DisableSprint));
        Invoke(nameof(DisableSprint), 4f);
        
        //Activamos el bool de estar sprintando;
        _isSprinting = true;
    }

    /// <summary>
    /// Método para dejar de sprintar;
    /// </summary>
    private void DisableSprint()
    {
        //Si estamos haciendo una transición a otra escena dejamos de sprintar, pero no cambiamos la velocidad;
        if (_playerScriptStorage.TransitionsManager.InTransition)
        {
            _isSprinting = false;
            return;
        }
        
        //Volvemos a la velocidad de andar y dejamos de sprintar;
        _playerScriptStorage.ActionBasedContinuousMoveProvider.moveSpeed = 2f;
        _isSprinting = false;
    }
}
