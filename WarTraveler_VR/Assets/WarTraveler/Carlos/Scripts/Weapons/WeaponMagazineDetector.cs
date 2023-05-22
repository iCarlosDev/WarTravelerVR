using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class WeaponMagazineDetector : MonoBehaviour
{
    [SerializeField] private XR_Slider _xrSlider;

    private void Awake()
    {
        _xrSlider = GetComponent<XR_Slider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponMagazine") && _xrSlider.MHandle == null)
        {
            other.transform.parent = _xrSlider.transform;
            other.GetComponent<Rigidbody>().isKinematic = true;
            _xrSlider.MHandle = other.transform;
        }
    }
}
