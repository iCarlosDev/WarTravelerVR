using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class WeaponBolt : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private XR_Slider _xrSlider;
    [SerializeField] private bool _wasBolted;

    [SerializeField] private List<AudioSource> _audioSourcesList;

    public XR_Slider XRSlider => _xrSlider;

    private void Awake()
    {
        _weapon = GetComponentInParent<Weapon>();
        _xrSlider = GetComponent<XR_Slider>();
        
        _audioSourcesList.Clear();
        _audioSourcesList.AddRange(GetComponents<AudioSource>());
    }

    private void Update()
    {
        if (_xrSlider.value == 0 && !_wasBolted)
        {
            _weapon.BoltAction();

            if (!_weapon.IsShooting)
            {
                _audioSourcesList[0].PlayOneShot(_audioSourcesList[0].clip);
            }
            
            _wasBolted = true;
        }
        else if (_xrSlider.value != 0 && _wasBolted)
        {
            if (!_weapon.IsShooting)
            {
                _audioSourcesList[1].PlayOneShot(_audioSourcesList[1].clip);
            }
            
            _weapon.IsShooting = false;
            _wasBolted = false;
        }
    }
}
