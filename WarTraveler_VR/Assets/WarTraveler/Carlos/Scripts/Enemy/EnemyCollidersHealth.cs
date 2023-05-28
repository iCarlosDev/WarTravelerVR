using System;
using UnityEngine;

public class EnemyCollidersHealth : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemyHealth;

    [SerializeField] private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _enemyHealth = GetComponentInParent<EnemyHealth>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidbody.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            Bullet bullet = collision.transform.GetComponent<Bullet>();
            _enemyHealth.TakeDamage(bullet.BulletDamage);
        }
    }
}
