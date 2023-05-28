using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketController : MonoBehaviour
{
    private Coroutine _setPistolToSocket;
    
    public void SetPistolToSocket(SelectEnterEventArgs args)
    {
        if (!args.interactableObject.transform.TryGetComponent(out Weapon_Pistol weaponPistol)) return;

        if (_setPistolToSocket != null)
        {
            StopCoroutine(_setPistolToSocket);
            _setPistolToSocket = null;
        }
        
        _setPistolToSocket = StartCoroutine(SetPistolToSocket_Coroutine(args));
    }

    private IEnumerator SetPistolToSocket_Coroutine(SelectEnterEventArgs args)
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
