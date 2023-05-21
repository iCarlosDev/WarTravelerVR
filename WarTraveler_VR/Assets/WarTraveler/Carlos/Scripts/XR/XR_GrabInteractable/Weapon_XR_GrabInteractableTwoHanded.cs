using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Weapon_XR_GrabInteractableTwoHanded : XRGrabInteractable
{
    [Space(20)]
    
    [Header("--- MY SCRIPT ---")]
    [Space(10)]
    [SerializeField] private bool _firstGrab;
    [SerializeField] private bool _secondGrab;

    [Header("--- ATTACHABLE HANDS ---")]
    [Space(10)]
    [SerializeField] private GameObject leftHandPrefabInstantiate;
    [SerializeField] private GameObject rightHandPrefabInstantiate;
    [SerializeField] private XRBaseController _leftController;
    [SerializeField] private XRBaseController _rightController;

    [Header("--- OTHER ---")] 
    [Space(10)] 
    [SerializeField] private Weapon _weapon;

    protected override void Awake()
    {
        base.Awake();
        _leftController = GameObject.FindWithTag("LeftHand").GetComponent<XRBaseController>();
        _rightController = GameObject.FindWithTag("RightHand").GetComponent<XRBaseController>();
        _weapon = GetComponent<Weapon>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        ControllerGrabCheck(args);

        if (_weapon != null)
        {
            TakePouchAmmo.instance.GrabbedWeaponsList.Add(_weapon);   
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        InteractableExited(args);
        
        if (_weapon != null)
        {
            TakePouchAmmo.instance.GrabbedWeaponsList.Remove(_weapon);
        }
    }
    
    private void ControllerGrabCheck(SelectEnterEventArgs args)
    {
        XRBaseController controller = args.interactorObject.transform.GetComponent<XRBaseController>();
        
        if (!_firstGrab)
        {
            GameObject firstHand = Instantiate(controller.modelPrefab.gameObject, attachTransform);

            if (firstHand.CompareTag("LeftHand"))
            {
                leftHandPrefabInstantiate = firstHand;
            }
            else
            {
                rightHandPrefabInstantiate = firstHand;
            }
        }
        else
        {
            GameObject secondHand = Instantiate(controller.modelPrefab.gameObject, secondaryAttachTransform);
            
            if (secondHand.CompareTag("LeftHand"))
            {
                leftHandPrefabInstantiate = secondHand;
            }
            else
            {
                rightHandPrefabInstantiate = secondHand;
            }
        }
        
        if (_firstGrab) _secondGrab = true;
        
        _firstGrab = true;
    }

    private void InteractableExited(SelectExitEventArgs args)
    {
        ControllerExitCheck(args);
        
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

            _secondGrab = false;
        }
    }

    private void ControllerExitCheck(SelectExitEventArgs args)
    {
        Destroy(args.interactorObject.transform.tag.Equals(_leftController.tag) ? leftHandPrefabInstantiate : rightHandPrefabInstantiate);
    }

    public void OnControllerExited()
    {
        _firstGrab = false;
    }
}
