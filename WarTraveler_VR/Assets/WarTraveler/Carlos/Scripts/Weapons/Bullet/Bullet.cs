using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _bulletDamage;

    [SerializeField] private GameObject _bulletSound;
    
    [SerializeField] private ParticleSystem _targetImpactParticleSystem;
    [SerializeField] private ParticleSystem _defaultImpactParticleSystem;

    public int BulletDamage => _bulletDamage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f); //Destruimos el objeto después de 3s para no tener muchos instanciados;
    }

    private void SpawnParticleSystem(Collision collision ,ParticleSystem particleSystem)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 collisionNormal = contact.normal;

            Quaternion rotation = Quaternion.LookRotation(collisionNormal);
            ParticleSystem particleSystemInstance = Instantiate(particleSystem, contact.point, rotation);
            particleSystemInstance.Play();
            particleSystemInstance.transform.parent = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Si colisiona con algo que no queramos o un arma no se destruirá;
        if (collision.transform.CompareTag("BulletNoCollisionable") || collision.transform.CompareTag("Weapon")) return;

        if (collision.transform.CompareTag("Target"))
        {
            SpawnParticleSystem(collision, _targetImpactParticleSystem);
            _bulletSound.SetActive(true);
            _bulletSound.transform.parent = null;
            Destroy(gameObject);
            
            return;
        }
        
        SpawnParticleSystem(collision, _defaultImpactParticleSystem);
        
        Destroy(gameObject);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mar"))
        {
            Vector3 offset = new Vector3(0f, _verticalOffset, 0f);
            
            ParticleSystem particleSystem = Instantiate(_waterSplashParticleSystem, transform.position + offset, Quaternion.identity);
            particleSystem.Play();
            particleSystem.transform.parent = null;
        }
    }*/
}
