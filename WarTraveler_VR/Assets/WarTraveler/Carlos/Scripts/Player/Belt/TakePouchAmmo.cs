using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TakePouchAmmo : MonoBehaviour
{
    public static TakePouchAmmo instance;

    [SerializeField] private List<Weapon> _grabbedWeaponsList;
    [SerializeField] private XR_InputDetector _xrInputDetector;
    [SerializeField] private bool _canTakeAmmo;

    //GETTERS && SETTERS//
    public List<Weapon> GrabbedWeaponsList => _grabbedWeaponsList;

    /////////////////////////////////////////////////

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        TakeAmmo();
    }

    /// <summary>
    /// Método para Instanciar y Agarrar automaticamente un cargador cuando pulsemos el trigger en nuestro "Pouch";
    /// </summary>
    private void TakeAmmo()
    {
        if (_xrInputDetector == null) return;

        if (_xrInputDetector.IsTriggering && _xrInputDetector.ObjectGrabbed == null)
        {
            GameObject instantiatedObject = Instantiate(GrabbedWeaponsList[0].MagazinePrefab, transform.position, transform.rotation);
            XR_TriggerGrabbable triggerGrabbable = instantiatedObject.GetComponent<XR_TriggerGrabbable>();
            _xrInputDetector.GrabTriggerObject(triggerGrabbable);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("LeftHand") || other.CompareTag("RightHand")) && _grabbedWeaponsList.Count != 0)
        {
            _xrInputDetector = other.GetComponent<XR_InputDetector>();
            if (_xrInputDetector.WeaponGrabbed != null) return;

            _canTakeAmmo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LeftHand") || other.CompareTag("RightHand"))
        {
            if (_xrInputDetector != other.GetComponent<XR_InputDetector>()) return;

            _canTakeAmmo = false;
            _xrInputDetector = null;
        }
    }
}

