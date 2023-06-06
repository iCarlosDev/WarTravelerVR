using System.Collections;
using UnityEngine;

public class PlaneHealth : MonoBehaviour
{
    [Header("--- DESTROYED PLANE PREFAB ---")]
    [Space(10)]
    [SerializeField] private GameObject _destroyedPlanePrefab;
    
    [Header("--- OTHER SCRIPTS ---")]
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _explosionAudio;
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
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    #region - DEBUG -
    
    [ContextMenu("Take Damage")]
    private void TakeDamageDEBUG()
    {
        TakeDamage(999);
    }
    
    #endregion
    
    /// <summary>
    /// Método para quitar vida al avión;
    /// </summary>
    /// <param name="damage"></param>
    private void TakeDamage(int damage)
    {
        if (_isDead) return; //Si ya está muerto no hace falta hacer la lógica restante;
        
        //Restamos a la vida del avión el daño que reciba
        //y si llega a 0 o menos se llama al método de morir;
        _currentHealth -= damage;
        if (_currentHealth <= 0) Die();
    }

    /// <summary>
    /// Método para que el avión muera;
    /// </summary>
    private void Die()
    {
        //Seteamos al avión muerto y cambiamos su layer
        //para que no le afecten las balas;
        _isDead = true;
        gameObject.layer = 2;
        
        //Iniciamos todas las particulas correspondientes;
        _explosionParticle.Play();
        _smokeParticle.Play();
        _fireParticle.Play();
        
        //Hacemos que le afecten las físicas y le damos impulso
        //de tracción y rotación para que no se quede quieto en el aire;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(transform.forward * _forceImpulse, ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.forward * _forwardTorqueImpulse, ForceMode.Impulse);
        _rigidbody.AddTorque(Vector3.left * _leftTorqueImpulse, ForceMode.Impulse);

        //Se desactiva su animator;
        _animator.enabled = false;
        
        //Se añaden puntos al "AntiaereoScoreManager";
        AntiaereoScoreManager.instance.AddScore(100);
    }

    /// <summary>
    /// Método para destruir el avión;
    /// </summary>
    [ContextMenu("Destroy Plane")]
    public void DestroyPlane()
    {
        if (!_meshRenderer.enabled) return; //Si ya a sido destruido no hace falta hacer la lógica restante;

        //Se inician las particulas correspondientes;
        _bodyExplosionParticle.Play();
        _giganticExplosionParticle.Play();
        
        _explosionAudio.PlayOneShot(_explosionAudio.clip);
            
        //Se desactivan las meshes necesarias para que
        //no se vea el avión roto y el intacto al mismo tiempo;
        _meshRenderer.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);

        //Se instancia el avión destruido en el mismo sitio donde está el avión intacto;
        Instantiate(_destroyedPlanePrefab, transform.position, transform.rotation);
        
        DeletePlane();
    }

    /// <summary>
    /// Método para borrar el avión;
    /// </summary>
    private void DeletePlane()
    {
        //Si la corrutina de audio es nula llamamos a la corrutina "SetAudioOff_Coroutine";
        SetAudioOff ??= StartCoroutine(SetAudioOff_Coroutine());
        
        Destroy(gameObject, 15f); //Borramos el avión en 15s;
    }
    
    /// <summary>
    /// Corrutina que baja el audio del avión progresivamente
    /// *Evita que un avión encallado en un barco lo estémos escuchando durante toda la partida*
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetAudioOff_Coroutine()
    {
        float lerpTime = 10f;
        float time = 0f;
        
        //Mientras el volumen no sea 0...;
        while (_audioSource.volume != 0)
        {
            //Hacemos una transición del volumen a 0 en el tiempo asignado;
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
            
            AntiaereoScoreManager.instance.AddScore(200);
        }

        if (collision.transform.CompareTag("Cubierta"))
        {
            if (!_meshRenderer.enabled) return;
            
            DestroyPlane();
            AntiaereoScoreManager.instance.AddScore(25);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mar"))
        {
            DeletePlane();
        }
    }
}
