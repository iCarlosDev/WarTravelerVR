using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class WeaponBolt : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private XR_Slider _xrSlider;
    [SerializeField] private bool _wasBolted;

    private void Awake()
    {
        _weapon = GetComponentInParent<Weapon>();
        _xrSlider = GetComponent<XR_Slider>();
    }

    private void Update()
    {
        if (_xrSlider.value == 0 && !_wasBolted)
        {
            _weapon.BoltAction();
            _wasBolted = true;
        }
        else if (_xrSlider.value != 0 && _wasBolted)
        {
            _wasBolted = false;
        }
    }
}
