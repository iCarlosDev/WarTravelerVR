using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneHealth : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticleSystem;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private bool _isDead;

    private void Awake()
    {
        _explosionParticleSystem = GetComponentInChildren<ParticleSystem>(); 
    }

    private void Start()
    {
        _maxHealth = 100;
        _currentHealth = _maxHealth;
    }

    private void TakeDamage(int damage)
    {
        if (_isDead) return;
       
        _currentHealth -= damage;
        
        if (_currentHealth <= 0) Die();
    }

    private void Die()
    {
        _isDead = true;
        _explosionParticleSystem.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            TakeDamage(collision.transform.GetComponent<Bullet>().BulletDamage);
        }
    }
}
