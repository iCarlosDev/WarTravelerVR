using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_GrabInteractableTwoHanded : XRGrabInteractable
{
    [Space(20)]
    
    [Header("--- MY SCRIPT ---")]
    [Space(10)]
    [SerializeField] private bool isGrabbed;
    
    [Header("--- ATACHABLE HANDS ---")]
    [Space(10)]
    [SerializeField] private GameObject leftHandPrefabInstantiate;
    [SerializeField] private GameObject rightHandPrefabInstantiate;
    [SerializeField] private XRBaseController controller;
    
    [Header("--- ROTATION CONTROL ---")]
    [Space(10)]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool TrackRotationByPosition;

    private void Update()
    {
        if (isGrabbed)
        {
            ControlRotationByPosition();
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        controller = args.interactorObject.transform.GetComponent<XRBaseController>();
        
        InteractableSelected();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        controller = args.interactorObject.transform.GetComponent<XRBaseController>();
        
        InteractableExited();
    }

    private void InteractableSelected()
    {
        if (controller == null) return;
        
        ControllerGrabCheck(isGrabbed);
        isGrabbed = true;
    }
    
    private void InteractableExited()
    {
        ControllerExitCheck();

        if (!isGrabbed) return;

        if (secondaryAttachTransform.childCount != 0)
        {
            if (secondaryAttachTransform.GetChild(0).name.Contains("Left_Hand"))
            {
                leftHandPrefabInstantiate.transform.position = attachTransform.position;
                leftHandPrefabInstantiate.transform.parent = attachTransform;
            }
            else
            {
                rightHandPrefabInstantiate.transform.position = attachTransform.position;
                rightHandPrefabInstantiate.transform.parent = attachTransform;
            }
        }
    }

    private void ControllerGrabCheck(bool isGrabbed)
    {
        if (!isGrabbed)
        {
            GameObject firstHand = Instantiate(controller.modelPrefab.gameObject, attachTransform);
            HandGrabCheck(firstHand);
        }
        else
        {
            GameObject secondHand = Instantiate(controller.modelPrefab.gameObject, secondaryAttachTransform);
            HandGrabCheck(secondHand);
        }
    }

    private void HandGrabCheck(GameObject hand)
    {
        if (hand.name.Contains("Left_Hand"))
        {
            leftHandPrefabInstantiate = hand;
        }
        else
        {
            rightHandPrefabInstantiate = hand;
        }
    }

    private void ControllerExitCheck()
    {
        if (controller == null) return;

        if (controller.CompareTag("LeftHand"))
        {
            Destroy(leftHandPrefabInstantiate);
        }
        else
        {
            Destroy(rightHandPrefabInstantiate);
        }
    }

    public void OnControllerExited()
    {
        isGrabbed = false;
    }

    private void ControlRotationByPosition()
    {
        if (!TrackRotationByPosition) return;

        Vector3 direction = (controller.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
