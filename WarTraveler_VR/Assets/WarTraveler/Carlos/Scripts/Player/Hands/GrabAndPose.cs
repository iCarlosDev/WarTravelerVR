using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabAndPose : MonoBehaviour
{
    [SerializeField] private HandData rightHandPose;
    
    private void Start()
    {
        Weapon_XR_GrabInteractableTwoHanded _weaponXRGrab = GetComponent<Weapon_XR_GrabInteractableTwoHanded>();
        _weaponXRGrab.selectEntered.AddListener(SetupPose);
        rightHandPose.gameObject.SetActive(false);
    }

    private void SetupPose(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            HandData handData = args.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.Animator.enabled = false;
        }
    }
}
