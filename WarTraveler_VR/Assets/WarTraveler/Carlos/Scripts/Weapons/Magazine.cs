using System;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class Magazine : MonoBehaviour
{
    [SerializeField] private XR_TriggerGrabbable _xrTriggerGrabbable;
    [SerializeField] private XR_Slider _xrSlider;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Rigidbody _rigidbody;
    
    [SerializeField] private bool _isBeingInserted;
    [SerializeField] private bool _isInserted;
    
    [Header("--- AMMO PARAMS ---")]
    [Space(10)]
    [SerializeField] protected int _magazineCapacity;
    [SerializeField] protected int _currentAmmoInMagazine;

    //GETTERS && SETTERS//
    public BoxCollider BoxCollider => _boxCollider;
    public Rigidbody Rigidbody => _rigidbody;
    public bool IsBeingInserted
    {
        get => _isBeingInserted;
        set => _isBeingInserted = value;
    }
    public bool IsInserted
    {
        get => _isInserted;
        set => _isInserted = value;
    }
    public int MagazineCapacity => _magazineCapacity;
    public int CurrentAmmoInMagazine
    {
        get => _currentAmmoInMagazine;
        set => _currentAmmoInMagazine = value;
    }

    ////////////////////////////////////////////////////////////

    private void Awake()
    {
        _xrTriggerGrabbable = GetComponent<XR_TriggerGrabbable>();
        _boxCollider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _currentAmmoInMagazine = _magazineCapacity;
    }

    private void Update()
    {
        if (_xrTriggerGrabbable.isSelected && _isInserted)
        {
            GrabInsertedMagazine();
        }
    }

    private void GrabInsertedMagazine()
    {
        _boxCollider.isTrigger = false;
        _rigidbody.isKinematic = false;

        _isInserted = false;
    }
}
