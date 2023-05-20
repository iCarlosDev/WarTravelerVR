using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XR_InputDetector : MonoBehaviour
{
    [SerializeField] private InputActionReference _gripInput;
    [SerializeField] private InputActionReference _triggerInput;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("AmmoPouch"))
        {
            Debug.Log(_gripInput.action.ReadValue<float>());
        }
    }
}
