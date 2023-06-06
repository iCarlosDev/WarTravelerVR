using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSpawnManager : MonoBehaviour
{
    [Header("--- PISTOLS ---")] 
    [Space(10)] 
    [SerializeField] private GameObject _colt1911_Prefab;
    [SerializeField] private GameObject _luger_Prefab;

    [Header("--- SUBMACHINEGUNS ---")]
    [Space(10)]
    [SerializeField] private GameObject _thompson_Prefab;
    [SerializeField] private GameObject _ppsh_Prefab;
    [SerializeField] private GameObject _mp40_Prefab;

    [Header("--- RIFLES ---")] 
    [Space(10)] 
    [SerializeField] private GameObject _stg44_Prefab;

    [Header("--- MACHINEGUNS ---")] 
    [Space(10)] 
    [SerializeField] private GameObject _bar_Prefab;

    [Header("--- ALL SPAWNERS ---")] 
    [Space(10)] 
    [SerializeField] private List<SpawnWeapon> _spawnWeaponsList;


    //GETTERS && SETTERS//
    public GameObject Colt1911Prefab => _colt1911_Prefab;
    public GameObject LugerPrefab => _luger_Prefab;
    public GameObject ThompsonPrefab => _thompson_Prefab;
    public GameObject PpshPrefab => _ppsh_Prefab;
    public GameObject Mp40Prefab => _mp40_Prefab;
    public GameObject Stg44Prefab => _stg44_Prefab;
    public GameObject BarPrefab => _bar_Prefab;

    /////////////////////////////////////////

    private void Awake()
    {
        _spawnWeaponsList.AddRange(FindObjectsOfType<SpawnWeapon>());
    }

    public void ReespawnAllWeapons()
    {
        foreach (SpawnWeapon spawnWeapon in _spawnWeaponsList)
        {
            spawnWeapon.WeaponSpawn();
        }
    }
}
