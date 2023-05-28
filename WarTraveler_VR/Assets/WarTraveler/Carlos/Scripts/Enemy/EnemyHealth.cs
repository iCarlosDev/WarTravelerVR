using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyComponentsStorage _enemyComponentsStorage;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    private void Awake()
    {
        _enemyComponentsStorage = GetComponent<EnemyComponentsStorage>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Die()
    {
        
    }
}
