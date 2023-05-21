using UnityEngine;

public class Magazine : MonoBehaviour
{
    [Header("--- AMMO PARAMS ---")]
    [Space(10)]
    [SerializeField] protected int _magazineCapacity;
    [SerializeField] protected int _currentAmmoInMagazine;

    //GETTERS && SETTERS//
    public int MagazineCapacity => _magazineCapacity;
    public int CurrentAmmoInMagazine
    {
        get => _currentAmmoInMagazine;
        set => _currentAmmoInMagazine = value;
    }

    ////////////////////////////////////////////////////////////

    private void Start()
    {
        _currentAmmoInMagazine = _magazineCapacity;
    }
}
