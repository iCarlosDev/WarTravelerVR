using UnityEngine;

public class SpawnWeapon : MonoBehaviour
{
    [SerializeField] private WeaponsSpawnManager _weaponsSpawnManager;

    [SerializeField] private GameObject _weaponSpawned;
    [SerializeField] private GameObject _weaponAmmoSpawned;

    [SerializeField] private Vector3 _weaponAmmoSpawnOffset;
    
    [SerializeField] private WeaponType _weaponTypeEnum;
    private enum WeaponType
    {
        Colt1911,
        Luger,
        Thompson,
        PPSH,
        MP_40,
        STG_44,
        BAR
    }

    private void Awake()
    {
        _weaponsSpawnManager = FindObjectOfType<WeaponsSpawnManager>();
    }

    private void Start()
    {
        WeaponSpawn();
    }

    public void WeaponSpawn()
    {
        Destroy(_weaponSpawned);
        Destroy(_weaponAmmoSpawned);
        
        switch (_weaponTypeEnum)
        {
            case WeaponType.Colt1911:
            {
                _weaponSpawned = Instantiate(_weaponsSpawnManager.Colt1911Prefab, transform.position, transform.rotation);
                _weaponAmmoSpawned = Instantiate(_weaponSpawned.GetComponent<Weapon>().MagazinePrefab, transform.position + _weaponAmmoSpawnOffset, transform.rotation);
                break;
            }
            case WeaponType.Luger:
            {
                _weaponSpawned = Instantiate(_weaponsSpawnManager.LugerPrefab, transform.position, transform.rotation);
                _weaponAmmoSpawned = Instantiate(_weaponSpawned.GetComponent<Weapon>().MagazinePrefab, transform.position + _weaponAmmoSpawnOffset, transform.rotation);
                break;
            }
            case WeaponType.Thompson:
            {
                _weaponSpawned = Instantiate(_weaponsSpawnManager.ThompsonPrefab, transform.position, transform.rotation);
                _weaponAmmoSpawned = Instantiate(_weaponSpawned.GetComponent<Weapon>().MagazinePrefab, transform.position + _weaponAmmoSpawnOffset, transform.rotation);
                break;
            }
            case WeaponType.PPSH:
            {
                _weaponSpawned = Instantiate(_weaponsSpawnManager.PpshPrefab, transform.position, transform.rotation);
                _weaponAmmoSpawned = Instantiate(_weaponSpawned.GetComponent<Weapon>().MagazinePrefab, transform.position + _weaponAmmoSpawnOffset, transform.rotation);
                break;
            }
            case WeaponType.MP_40:
            {
                _weaponSpawned = Instantiate(_weaponsSpawnManager.Mp40Prefab, transform.position, transform.rotation);
                _weaponAmmoSpawned = Instantiate(_weaponSpawned.GetComponent<Weapon>().MagazinePrefab, transform.position + _weaponAmmoSpawnOffset, transform.rotation);
                break;
            }
            case WeaponType.STG_44:
            {
                _weaponSpawned = Instantiate(_weaponsSpawnManager.Stg44Prefab, transform.position, transform.rotation);
                _weaponAmmoSpawned = Instantiate(_weaponSpawned.GetComponent<Weapon>().MagazinePrefab, transform.position + _weaponAmmoSpawnOffset, transform.rotation);
                break;
            }
            case WeaponType.BAR:
            {
                _weaponSpawned = Instantiate(_weaponsSpawnManager.BarPrefab, transform.position, transform.rotation);
                _weaponAmmoSpawned = Instantiate(_weaponSpawned.GetComponent<Weapon>().MagazinePrefab, transform.position + _weaponAmmoSpawnOffset, transform.rotation);
                break;
            }
        }
    }
}
