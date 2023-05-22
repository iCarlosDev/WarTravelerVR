using System;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class MoveWeaponSlider : MonoBehaviour
{
    [SerializeField] private XR_InputDetector _xrInputDetector;
    [SerializeField] private XR_Slider _xrSlider;
    [SerializeField] private Transform _sliderEndPoint;
    [SerializeField] private Vector3 _sliderStartPoint;
    [SerializeField] private float _totalDistance;

    private void Awake()
    {
        _xrSlider = GetComponent<XR_Slider>();
        _sliderStartPoint = transform.position;
        
        _totalDistance = _sliderEndPoint.transform.position.z - _sliderStartPoint.z;
    }

    private void Update()
    {
        //MoveSlider();
    }

    private void MoveSlider()
    {
        if (_xrInputDetector == null) return;


        float distanceValue = Vector3.Distance(_xrInputDetector.transform.position, _sliderEndPoint.transform.position) / _totalDistance;
        _xrSlider.value = distanceValue;
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
