using System;
using System.Collections.Generic;
using UnityEngine;

public class PlaneHealth : MonoBehaviour
{
    [SerializeField] private GameObject _destroyedPlanePrefab;
    
    [Header("--- PARTICLES ---")]
    [Space(10)]
    [SerializeField] private ParticleSystem _explosionParticle;
    [SerializeField] private ParticleSystem _giganticExplosionParticle;
    [SerializeField] private ParticleSystem _bodyExplosionParticle;
    [SerializeField] private ParticleSystem _smokeParticle;
    [SerializeField] private ParticleSystem _fireParticle;
    
    [Header("--- HEALTH PARAMS ---")]
    [Space(10)]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private bool _isDead;
    
    [Header("--- DIE PARAMS ---")]
    [Space(10)]
    [SerializeField] private LayerMask _layerToChange;
    [SerializeField] private List<Rigidbody> _rigidbodyList;
    [SerializeField] private float _forceImpulse;
    [SerializeField] private float _dieImpulse;
    [SerializeField] private float _torqueImpulse;
    
    //GETTERS && SETTERS//
    public bool IsDead => _isDead;
    
    //////////////////////////////////

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
        gameObject.layer = _layerToChange;
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

        if (collision.transform.CompareTag("Cubierta"))
        {
            _bodyExplosionParticle.Play();
            _giganticExplosionParticle.Play();
            
            GetComponent<MeshRenderer>().enabled = false;
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
