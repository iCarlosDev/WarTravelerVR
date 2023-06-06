using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager instance;

    [SerializeField] private PlayerScriptStorage _playerScriptStorage;
    
    [SerializeField] private XR_InputDetector _leftInputDetector;
    [SerializeField] private XR_InputDetector _rightInputDetector;

    [SerializeField] private Transform _playerCamera;
    
    [SerializeField] private GameObject _returnToLobbyBTN;
    [SerializeField] private Transform _pauseMenu;
    [SerializeField] private Vector3 _pauseMenuPositionOffset;
    [SerializeField] private bool _isPaused;

    [SerializeField] private float _timePauseButtonPressed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
        _pauseMenu = transform.GetChild(0);
    }

    private void OnEnable()
    {
        TryGetComponents();
        
        _returnToLobbyBTN.SetActive(SceneManager.GetActiveScene().buildIndex == 1);
        _pauseMenu.gameObject.SetActive(false);
    }
    
    private void TryGetComponents()
    { 
        _playerScriptStorage ??= FindObjectOfType<PlayerScriptStorage>();
        if (Camera.main != null) _playerCamera = Camera.main.transform;

        if (_leftInputDetector != null && _rightInputDetector != null) return;
        
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

    private void Update()
    {
        CheckPauseButtonPress();
    }

    private void LateUpdate()
    {
        TryGetComponents();
    }

    private void CheckPauseButtonPress()
    {
        if (_playerScriptStorage.TransitionsManager.InTransition)
        {
            PauseMenu_OFF();
            return;
        }

        if (_isPaused)
        {
            if (_leftInputDetector.SecondaryButton.action.triggered || _rightInputDetector.SecondaryButton.action.triggered)
            {
                PauseMenuControl();
            }
            
            return;
        }

        if (_leftInputDetector.SecondaryButton.action.IsPressed() || _rightInputDetector.SecondaryButton.action.IsPressed())
        {
            _timePauseButtonPressed += Time.deltaTime;
        }
        else
        {
            _timePauseButtonPressed = 0f;
        }

        if (!(_timePauseButtonPressed >= 2.5f)) return;
        
        PauseMenuControl();
        _timePauseButtonPressed = 0f;
    }

    private void PauseMenuControl()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            PauseMenu_ON();
        }
        else
        {
            PauseMenu_OFF();
        }
    }

    private void PauseMenu_ON()
    {
        _playerScriptStorage.ActionBasedContinuousMoveProvider.moveSpeed = 0f;
        
        _pauseMenu.gameObject.SetActive(true);
        
        _pauseMenu.position = _playerCamera.position + _pauseMenuPositionOffset;
        
        Vector3 playerEulerAngles = _playerCamera.transform.rotation.eulerAngles;
        Quaternion rotation = Quaternion.Euler(0f, playerEulerAngles.y, 0f);
        _pauseMenu.transform.rotation = rotation;
    }
    
    private void PauseMenu_OFF()
    {
        _pauseMenu.gameObject.SetActive(false);
        _playerScriptStorage.ActionBasedContinuousMoveProvider.moveSpeed = 2f;
    }

    public void Resume()
    {
        PauseMenu_OFF();
    }

    public void ReturnToLobby()
    {
        GameManager.instance.PlayerSpawnIndex = 1;
        _playerScriptStorage.TransitionsManager.Fade_IN(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
