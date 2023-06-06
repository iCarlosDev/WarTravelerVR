using UnityEngine;

public class DestroyBulletSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_audioSource.clip);
        Destroy(gameObject, 3f);
    }
}
