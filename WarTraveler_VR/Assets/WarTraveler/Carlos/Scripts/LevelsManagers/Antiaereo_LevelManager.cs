using System;
using System.Linq;
using UnityEngine;

public class Antiaereo_LevelManager : MonoBehaviour
{
    public static Antiaereo_LevelManager instance;

    [Header("--- OTHER SCRIPTS ---")]
    [Space(10)]
    [SerializeField] private PlayerScriptStorage _playerScriptStorage;
    [SerializeField] private Turret_XR_GrabInteractableTwoHanded _turretXRGrabInteractable;

    [Header("--- PLAYER SPAWN ---")]
    [SerializeField] private Transform _playerSpawn;

    private void Awake()
    {
        instance = this;

        _playerScriptStorage = FindObjectOfType<PlayerScriptStorage>();
        _turretXRGrabInteractable = FindObjectOfType<Turret_XR_GrabInteractableTwoHanded>();
    }

    private void Start()
    {
        AudioManager.instance.Stop("LobbyTheme");
        
        _playerScriptStorage.transform.position = _playerSpawn.position;
        _playerScriptStorage.transform.rotation = _playerSpawn.localRotation;
    }

    /// <summary>
    /// Método para soltar el antiaereo y volver al Lobby;
    /// </summary>
    public void DropAntiaereoAndReturnLobby()
    {
        //Se invoca un método para llevarnos al lobby en 3s;
        Invoke(nameof(ReturnToLobby), 3f);
        
        //Si no tenemos agarrado el antiaereo no hace falta que se haga la siguiente lógica;
        if (!_turretXRGrabInteractable.isSelected) return;

        //Guardamos los interactor que tiene el antiaereo;
        var interactors = _turretXRGrabInteractable.interactorsSelecting.ToList();

        //Reocrremos los interactors del antiaereo y hacemos que dejen de interactuar con este;
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

        //Deshabilitamos el collider del antiaereo para que no se pueda volver a agarrar;
        _turretXRGrabInteractable.BoxCollider.enabled = false;
    }
    
    /// <summary>
    /// Método para iniciar una animación FadeIn y vovler al Lobby;
    /// </summary>
    public void ReturnToLobby()
    {
        //Se modifica el indice de spawn en el gamemanager para aparecer
        //en un sitio concreto cuando se regrese al lobby;
        GameManager.instance.PlayerSpawnIndex = 1;
        _playerScriptStorage.TransitionsManager.Fade_IN(0);
    }
}
