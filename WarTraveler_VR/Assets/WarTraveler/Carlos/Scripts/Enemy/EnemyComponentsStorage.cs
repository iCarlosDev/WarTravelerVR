using System.Collections.Generic;
using UnityEngine;

public class EnemyComponentsStorage : MonoBehaviour
{
    [SerializeField] private EnemyAnimationsController _enemyAnimationsController;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private EnemyHealth _enemyHealth;

    [SerializeField] private List<EnemyCollidersHealth> _enemyBonesList;

    //GETTERS && SETTERS//
    public EnemyAnimationsController EnemyAnimationsController => _enemyAnimationsController;
    public EnemyController EnemyController => _enemyController;
    public EnemyHealth EnemyHealth => _enemyHealth;
    public List<EnemyCollidersHealth> EnemyBonesList => _enemyBonesList;

    ////////////////////////////////////////

    private void Awake()
    {
        _enemyAnimationsController = GetComponent<EnemyAnimationsController>();
        _enemyController = GetComponent<EnemyController>();
        _enemyHealth = GetComponent<EnemyHealth>();
        
        _enemyBonesList.AddRange(GetComponentsInChildren<EnemyCollidersHealth>());
    }
}
