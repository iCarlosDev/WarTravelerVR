using System;
using UnityEngine;

public class DestroyExplosionParticles : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public AudioSource AudioSource => _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, 15f);
    }

}
