using System.Collections;
using UnityEngine;

public class PlaneHealth : MonoBehaviour
{
    [SerializeField] private GameObject _destroyedPlanePrefab;
    [SerializeField] private AudioSource _audioSource;
    private Coroutine SetAudioOff;

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
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _forceImpulse;
    [SerializeField] private float _forwardTorqueImpulse;
    [SerializeField] private float _leftTorqueImpulse;
    
    private MeshRenderer _meshRenderer;
    
    //GETTERS && SETTERS//
    public bool IsDead => _isDead;
    
    //////////////////////////////////

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _audioSource = GetComponent<AudioSource>();
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
        gameObject.layer = 2;
        _explosionParticle.Play();
        _smokeParticle.Play();
        _fireParticle.Play();
        
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform.forward * _forceImpulse, ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.forward * _forwardTorqueImpulse, ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.left * _leftTorqueImpulse, ForceMode.Impulse);
    }

    [ContextMenu("Destroy Plane")]
    private void DestroyPlane()
    {
        if (!_meshRenderer.enabled) return;

        _bodyExplosionParticle.Play();
        _giganticExplosionParticle.Play();
            
        _meshRenderer.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);

        Instantiate(_destroyedPlanePrefab, transform.position, transform.rotation);
        
        DeletePlane();
    }

    private IEnumerator SetAudioOff_Coroutine()
    {
        float lerpTime = 10f;
        
        float time = 0f;
        
        while (_audioSource.volume != 0)
        {
            _audioSource.volume = Mathf.Lerp(_audioSource.volume, 0f, time);
            time += lerpTime * Time.deltaTime;
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            TakeDamage(collision.transform.GetComponent<Bullet>().BulletDamage);
        }

        if (collision.transform.CompareTag("CanonBullet"))
        {
            Die();
            DestroyPlane();
        }

        if (collision.transform.CompareTag("Cubierta"))
        {
            DestroyPlane();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mar"))
        {
            DeletePlane();
        }
    }

    private void DeletePlane()
    {
        SetAudioOff ??= StartCoroutine(SetAudioOff_Coroutine());

        Destroy(gameObject, 15f);
    }
}
