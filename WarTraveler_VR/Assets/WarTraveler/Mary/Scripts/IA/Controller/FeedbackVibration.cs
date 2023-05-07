using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class FeedbackVibration : MonoBehaviour
{
    // Declarando variables
    [SerializeField] private float time;
    [SerializeField] private float intensity;
    
    // Variables para los controladores
    [SerializeField] InputDevice leftController;
    [SerializeField] InputDevice rightController;
    // Start is called before the first frame update
    void Start()
    {
        // Obtener los dispositivos de entrada de los controladores izquierdo y derecho
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, new List<InputDevice> {leftController});
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, new List<InputDevice> {rightController});

       
    }


    IEnumerator Vibrate()
    {
        // Hacer que el controlador derecho vibre 
        rightController.SendHapticImpulse(0, intensity, time);
        
        yield return new WaitForSeconds(time);
        // Parar la vibración del controlador derecho
        rightController.SendHapticImpulse(0, 0, 0);
    }

    public void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Holaaa");
        StartCoroutine(Vibrate());

    }
}
