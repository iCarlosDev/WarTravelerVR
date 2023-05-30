using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaneHealth : MonoBehaviour
{
    [Header("--- PARTICLES ---")]
    [Space(10)]
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private ParticleSystem _smokeParticle;
    [SerializeField] private ParticleSystem _fireParticle;
    
    
    [SerializeField] private List<Rigidbody> _rigidbodyList;
    [SerializeField] private float _forceImpulse;
    [SerializeField] private float _torqueImpulse;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private bool _isDead;

    public bool IsDead => _isDead;

    private void Awake()
    {
        _rigidbodyList.AddRange(GetComponentsInChildren<Rigidbody>());
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    [ContextMenu("Take Damage")]
    private void TakeDamageDEBUG()
    {
        TakeDamage(999);
    }
    
    private void TakeDamage(int damage)
    {
        if (_isDead) return;
       
        _currentHealth -= damage;
       Debug.LogWarning("HITTED");
        
        if (_currentHealth <= 0) Die();
    }

    private void Die()
    {
        _isDead = true;
        _explosionParticle.Play();
        _smokeParticle.Play();
        _fireParticle.Play();
        
        foreach (Rigidbody rigidbody in _rigidbodyList)
        {
            rigidbody.isKinematic = false;
            rigidbody.AddForce(transform.forward * _forceImpulse, ForceMode.Impulse);
            rigidbody.AddTorque(Vector3.forward * _torqueImpulse, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            TakeDamage(collision.transform.GetComponent<Bullet>().BulletDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mar"))
        {
            Destroy(gameObject, 2f);
        }
    }
}
