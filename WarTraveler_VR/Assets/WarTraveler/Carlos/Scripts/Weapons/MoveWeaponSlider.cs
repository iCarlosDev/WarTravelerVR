using System;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class MoveWeaponSlider : MonoBehaviour
{
    [SerializeField] private XR_InputDetector _xrInputDetector;
    [SerializeField] private XR_Slider _xrSlider;
    [SerializeField] private float _moveSlideSpeed = 5f;

    private void Awake()
    {
        _xrSlider = GetComponent<XR_Slider>();
    }

    private void Update()
    {
        MoveSlider();
    }

    private void MoveSlider()
    {
        if (_xrInputDetector == null) return;
        
        _xrSlider.value += _xrInputDetector.ControllerPosition.action.ReadValue<Vector3>().x;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_xrInputDetector != null) return;

        if (other.CompareTag("LeftHand") || other.CompareTag("RightHand"))
        {
            _xrInputDetector = other.GetComponent<XR_InputDetector>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_xrInputDetector == null) return;

        if (other.CompareTag("LeftHand") || other.CompareTag("RightHand"))
        {
            _xrInputDetector = null;
        }
    }
}
