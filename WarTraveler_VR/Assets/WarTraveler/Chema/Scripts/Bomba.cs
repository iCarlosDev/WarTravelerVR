using System;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionParticleSystem;

    private void Awake()
    {
        _explosionParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Quaternion rotationOffset = Quaternion.Euler(-180f, 0f, 0f);
        transform.rotation = rotationOffset;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cubierta"))
        {
            ParticleSystemControl();
            Destroy(gameObject);
        }

        if (!collision.gameObject.CompareTag("Craft")) return;
        
        ParticleSystemControl();
        collision.transform.GetComponent<PlaneHealth>().DestroyPlane();
        Destroy(gameObject);
    }

    private void ParticleSystemControl()
    {
        DestroyExplosionParticles explosionParticles = _explosionParticleSystem.GetComponent<DestroyExplosionParticles>();
        _explosionParticleSystem.Play();
        explosionParticles.AudioSource.PlayOneShot(explosionParticles.AudioSource.clip);
        _explosionParticleSystem.transform.parent = null;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if  (other.gameObject.CompareTag("Mar"))
        {
            Destroy(gameObject);
        }
    }
}
