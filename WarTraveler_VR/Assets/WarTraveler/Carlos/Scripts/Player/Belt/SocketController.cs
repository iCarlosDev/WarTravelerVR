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

    public void SetWeaponToSocket(SelectEnterEventArgs args)
    {
        if (!args.interactableObject.transform.TryGetComponent(out Weapon weapon)) return;

        if (_setWeaponToSocket != null)
        {
            StopCoroutine(_setWeaponToSocket);
            _setWeaponToSocket = null;
        }
        
        _setWeaponToSocket = StartCoroutine(SetWeaponToSocket_Coroutine(args));
        
        _playerScriptStorage.PlayerInventory.WeaponsList.Add(weapon);

        AudioManager.instance.PlayOneShot("SaveWeapon");
    }

    private IEnumerator SetWeaponToSocket_Coroutine(SelectEnterEventArgs args)
    {
        yield return new WaitForSeconds(0.1f);
        
        Weapon_XR_GrabInteractableTwoHanded weaponXRGrabInteractableTwoHanded = args.interactableObject.transform.GetComponent<Weapon_XR_GrabInteractableTwoHanded>();
        
        weaponXRGrabInteractableTwoHanded.FirstLeftHandPose.SetActive(false);
        weaponXRGrabInteractableTwoHanded.FirstRightHandPose.SetActive(false);
        weaponXRGrabInteractableTwoHanded.SecondLeftHandPose.SetActive(false);
        weaponXRGrabInteractableTwoHanded.SecondRightHandPose.SetActive(false);
        
        weaponXRGrabInteractableTwoHanded.FirstGrab = false;
        weaponXRGrabInteractableTwoHanded.SecondGrab = false;
    }

    public void RemoveWeaponInventory(SelectExitEventArgs args)
    {
        if (!args.interactableObject.transform.TryGetComponent(out Weapon weapon)) return;
        
        _playerScriptStorage.PlayerInventory.WeaponsList.Remove(weapon);
    }
}
