using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketController : MonoBehaviour
{
    [SerializeField] private PlayerScriptStorage _playerScriptStorage;
    private Coroutine _setWeaponToSocket;

    private void Awake()
    {
        _playerScriptStorage = GetComponentInParent<PlayerScriptStorage>();
    }

    /// <summary>
    /// Método para guardar el arma que soltemos en el socket;
    /// </summary>
    /// <param name="args"></param>
    public void SetWeaponToSocket(SelectEnterEventArgs args)
    {
        //Si el objeto que acercamos al socket no es un arma, no hace falta hacer la lógica restante;
        if (!args.interactableObject.transform.TryGetComponent(out Weapon weapon)) return;

        //Si la corrutina no es nula,
        //paramos la corrutina y la hacemos nula;
        if (_setWeaponToSocket != null)
        {
            StopCoroutine(_setWeaponToSocket);
            _setWeaponToSocket = null;
        }
        
        //Iniciamos la corrutina "SetWeaponToSocket_Coroutine"
        _setWeaponToSocket = StartCoroutine(SetWeaponToSocket_Coroutine(args));
        
        //Añadimos al inventario el arma que hemos añadido;
        _playerScriptStorage.PlayerInventory.WeaponsList.Add(weapon);

        //Iniciamos el audio de guardar arma;
        AudioManager.instance.PlayOneShot("SaveWeapon");
    }

    /// <summary>
    /// Corrutina que setea el arma que hemos guardado en el socket en el estado que queremos;
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private IEnumerator SetWeaponToSocket_Coroutine(SelectEnterEventArgs args)
    {
        yield return new WaitForSeconds(0.1f); //Esperamos 0,1s;
        
        //Obtenemos el componente de agarre del arma;
        Weapon_XR_GrabInteractableTwoHanded weaponXRGrabInteractableTwoHanded = args.interactableObject.transform.GetComponent<Weapon_XR_GrabInteractableTwoHanded>();
        
        //Desactivamos todas las poses de manos que hayan activas;
        weaponXRGrabInteractableTwoHanded.FirstLeftHandPose.SetActive(false);
        weaponXRGrabInteractableTwoHanded.FirstRightHandPose.SetActive(false);
        weaponXRGrabInteractableTwoHanded.SecondLeftHandPose.SetActive(false);
        weaponXRGrabInteractableTwoHanded.SecondRightHandPose.SetActive(false);
        
        //Apagamos todos los agarres que habían encendidos;
        weaponXRGrabInteractableTwoHanded.FirstGrab = false;
        weaponXRGrabInteractableTwoHanded.SecondGrab = false;
    }

    /// <summary>
    /// Método para quitar el arma del inventario cuando el socket no tenga el arma guardada;
    /// </summary>
    /// <param name="args"></param>
    public void RemoveWeaponInventory(SelectExitEventArgs args)
    {
        //Si el objeto que acercamos al socket no es un arma, no hace falta hacer la lógica restante;
        if (!args.interactableObject.transform.TryGetComponent(out Weapon weapon)) return;
        
        //Quitamos del inventario el arma;
        _playerScriptStorage.PlayerInventory.WeaponsList.Remove(weapon);
    }
}
