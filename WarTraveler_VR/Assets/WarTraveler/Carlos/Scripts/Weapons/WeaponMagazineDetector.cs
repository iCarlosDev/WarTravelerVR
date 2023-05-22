using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class WeaponMagazineDetector : MonoBehaviour
{
    [SerializeField] private XR_InputDetector _xrInputDetector;
    [SerializeField] private XR_Slider _xrSlider;
    [SerializeField] private XR_TriggerGrabbable _magazine;

    private void Awake()
    {
        _xrSlider = GetComponent<XR_Slider>();
    }

    private void DetectMagazineToInsert(Collider other)
    {
        _magazine = other.GetComponent<XR_TriggerGrabbable>();
        
        _magazine.GetComponent<BoxCollider>().isTrigger = true;
        _magazine.GetComponent<Rigidbody>().isKinematic = true;
        _magazine.GetComponent<Magazine>().IsBeingInserted = true;
            
        _xrInputDetector = _magazine.firstInteractorSelecting.transform.GetComponent<XR_InputDetector>();
        _xrInputDetector.DropTriggeredObject();

        _magazine.transform.parent = transform;
        _magazine.transform.localPosition = Vector3.zero;
            
        Quaternion magazineRotation = Quaternion.Euler(90f, 0f, 0f);
        _magazine.transform.localRotation = magazineRotation;

        _xrSlider.MHandle = _magazine.transform;
        _xrInputDetector.GrabSlider(_xrSlider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WeaponMagazine") && _xrSlider.MHandle == null)
        {
            if (!other.GetComponent<XR_TriggerGrabbable>().isSelected) return;

            DetectMagazineToInsert(other);
        }
    }
}
