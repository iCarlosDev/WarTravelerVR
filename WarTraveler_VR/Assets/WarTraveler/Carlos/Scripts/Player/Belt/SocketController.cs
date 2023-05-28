using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketController : MonoBehaviour
{
    private Coroutine _setWeaponToSocket;

    public void SetWeaponToSocket(SelectEnterEventArgs args)
    {
        if (!args.interactableObject.transform.TryGetComponent(out Weapon weapon)) return;

        if (_setWeaponToSocket != null)
        {
            StopCoroutine(_setWeaponToSocket);
            _setWeaponToSocket = null;
        }
        
        _setWeaponToSocket = StartCoroutine(SetWeaponToSocket_Coroutine(args));
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
}
