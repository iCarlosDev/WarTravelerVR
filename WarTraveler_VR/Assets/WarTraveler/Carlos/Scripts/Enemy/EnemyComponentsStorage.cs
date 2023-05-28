using System;
using UnityEngine;

public class EnemyComponentsStorage : MonoBehaviour
{
    [SerializeField] private EnemyAnimationsController _enemyAnimationsController;
    [SerializeField] private EnemyController _enemyController;
    [SerializeField] private EnemyHealth _enemyHealth;
    
    //GETTERS && SETTERS//
    public EnemyAnimationsController EnemyAnimationsController => _enemyAnimationsController;
    public EnemyController EnemyController => _enemyController;
    public EnemyHealth EnemyHealth => _enemyHealth;

    ////////////////////////////////////////

    private void Awake()
    {
        _enemyAnimationsController = GetComponent<EnemyAnimationsController>();
        _enemyController = GetComponent<EnemyController>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }
}
