using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TakePouchAmmo : MonoBehaviour
{
    public static TakePouchAmmo instance;

    [Header("--- WEAPONS ---")]
    [Space(10)]
    [SerializeField] private List<Weapon> _grabbedWeaponsList;
    
    [Header("--- CONTROLLER ---")]
    [Space(10)]
    [SerializeField] private XR_InputDetector _xrInputDetector;
    
    [Header("--- TAKE POUCH AMMO PARAMS ---")]
    [Space(10)]
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
        //Si no es un controlador o estámos usando el grip o no podemos coger munición,
        //no hacemos la lógica restante;
        if (_xrInputDetector == null || _xrInputDetector.IsGrabbing || !_canTakeAmmo) return;

        //Si no estámos pulsando el trigger o no tenemos un arma agarrada,
        //no hacemos la lógica restante;
        if (!_xrInputDetector.IsTriggering || _xrInputDetector.ObjectGrabbed != null) return;
        
        //Instanciamos un cargador del arma correspondiente, la autograbeamos en el mando del player
        //y hacemos que no podamos coger más cargadores;
        GameObject instantiatedObject = Instantiate(GrabbedWeaponsList[0].MagazinePrefab, transform.position, transform.rotation);
        XR_TriggerGrabbable triggerGrabbable = instantiatedObject.GetComponent<XR_TriggerGrabbable>();
        _xrInputDetector.GrabTriggerObject(triggerGrabbable);
        _canTakeAmmo = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.CompareTag("LeftHand") || other.CompareTag("RightHand")) && _grabbedWeaponsList.Count != 0)
        {
            if (_xrInputDetector == null) _xrInputDetector = other.GetComponent<XR_InputDetector>();

            if (_xrInputDetector.IsTriggering) return;

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

