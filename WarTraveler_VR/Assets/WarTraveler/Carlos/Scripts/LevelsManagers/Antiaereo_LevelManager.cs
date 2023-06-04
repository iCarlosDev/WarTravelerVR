using System.Linq;
using UnityEngine;

public class Antiaereo_LevelManager : MonoBehaviour
{
    public static Antiaereo_LevelManager instance;

    [SerializeField] private PlayerScriptStorage _playerScriptStorage;
    [SerializeField] private Turret_XR_GrabInteractableTwoHanded _turretXRGrabInteractable;

    private void Awake()
    {
        instance = this;

        _playerScriptStorage = FindObjectOfType<PlayerScriptStorage>();
        _turretXRGrabInteractable = FindObjectOfType<Turret_XR_GrabInteractableTwoHanded>();
    }

    public void DropAntiaereo()
    {
        Invoke(nameof(ReturnToLobby), 3f);
        
        if (!_turretXRGrabInteractable.isSelected) return;

        var interactors = _turretXRGrabInteractable.interactorsSelecting.ToList();

        foreach (var interactor in interactors)
        {
            if (interactor.transform.CompareTag("LeftHand"))
            {
                _turretXRGrabInteractable.interactionManager.SelectExit(interactor, _turretXRGrabInteractable);
            }
            else if (interactor.transform.CompareTag("RightHand"))
            {
                _turretXRGrabInteractable.interactionManager.SelectExit(interactor, _turretXRGrabInteractable);
            }
        }

        _turretXRGrabInteractable.BoxCollider.enabled = false;
    }
    
    public void ReturnToLobby()
    {
        _playerScriptStorage.TransitionsManager.Fade_IN(0);
    }
}
