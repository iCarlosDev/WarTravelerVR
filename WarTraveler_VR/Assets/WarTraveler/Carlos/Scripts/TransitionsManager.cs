using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionsManager : MonoBehaviour
{
    [SerializeField] private PlayerScriptStorage _playerScriptStorage;
    
    [SerializeField] private Animator _animator;
    [SerializeField] private int _sceneIndex;
    
    [SerializeField] private bool _inTransition;

    public bool InTransition => _inTransition;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerScriptStorage = GetComponentInParent<PlayerScriptStorage>();
    }

    private void Start()
    {
        Fade_OUT();
    }

    public void Fade_IN(int sceneIndex)
    {
        _animator.SetTrigger("Fade_IN");
        _sceneIndex = sceneIndex;

        _inTransition = true;
    }

    public void Fade_OUT()
    {
        _animator.SetTrigger("Fade_OUT");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    public void DisablePlayerMovement()
    {
        _playerScriptStorage.ActionBasedContinuousMoveProvider.moveSpeed = 0f;
        _playerScriptStorage.ActionBasedSnapTurnProvider.turnAmount = 0f;
    }

    public void EnablePlayerMovement()
    {
        _playerScriptStorage.ActionBasedContinuousMoveProvider.moveSpeed = 2f;
        _playerScriptStorage.ActionBasedSnapTurnProvider.turnAmount = 35f;
    }
}
