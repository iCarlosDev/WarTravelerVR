using UnityEngine;

public class AntiAereoShoot : MonoBehaviour
{
    [Header("--- OTHER SCRIPTS ---")]
    [Space(10)]
    [SerializeField] private Turret_XR_GrabInteractableTwoHanded _turretXrGrabInteractableTwoHanded;
    [SerializeField] private AntiAereoAnimations _antiAereoAnimations;

    [Header("--- CONTROLLERS ---")]
    [Space(10)]
    [SerializeField] private XR_InputDetector _leftInputDetector;
    [SerializeField] private XR_InputDetector _rightInputDetector;

    [Header("--- MACHINE_GUNS MESHES ---")]
    [Space(10)]
    [SerializeField] private Renderer _leftMachineGunRenderer;
    [SerializeField] private Renderer _rightMachineGunRenderer;

    [Header("--- CANON PARAMS ---")] 
    [Space(10)] 
    [SerializeField] private AudioSource _machineGunAudio;
    [SerializeField] private AudioSource _canonAudio;
    
    [Header("--- OVERHEATING PARAMS ---")] 
    [Space(10)]
    [SerializeField] private float _maxTimeShooting;
    [SerializeField] private float _currentLeftTimeShooting;
    [SerializeField] private float _currentRightTimeShooting;
    [SerializeField] private bool _isLeftOverheated;
    [SerializeField] private bool _isRightOverheated;
    
    [Header("--- MACHINE GUN PARAMS ---")]
    [Space(10)]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletImpulseForce;
    [SerializeField] private float _fireRate; 
    private float _leftFireRateTime;
    private float _rightFireRateTime;
    [SerializeField] private Transform _firstMachineGunCanon;
    [SerializeField] private ParticleSystem _firstMachineGunParticleSystem;
    [SerializeField] private bool _firstMachineGunShooting;
    [SerializeField] private Transform _secondMachineGunCanon;
    [SerializeField] private ParticleSystem _secondMachineGunParticleSystem;
    [SerializeField] private bool _secondMachineGunShooting;
    
    [Header("--- CANON PARAMS ---")]
    [Space(10)]
    [SerializeField] private GameObject _canonBulletPrefab;
    [SerializeField] private float _canonBulletImpulseForce;
    [SerializeField] private Transform _firstCanon;
    [SerializeField] private bool _readyToShootFirstCanon;
    [SerializeField] private ParticleSystem _firstCanonParticleSystem;
    [SerializeField] private Transform _secondCanon;
    [SerializeField] private bool _readyToShootSecondCanon;
    [SerializeField] private ParticleSystem _secondCanonParticleSystem;

    private void Awake()
    {
        _turretXrGrabInteractableTwoHanded = GetComponent<Turret_XR_GrabInteractableTwoHanded>();
        _antiAereoAnimations = GetComponent<AntiAereoAnimations>();
        
        _leftInputDetector = _turretXrGrabInteractableTwoHanded.LeftController.GetComponent<XR_InputDetector>();
        _rightInputDetector = _turretXrGrabInteractableTwoHanded.RightController.GetComponent<XR_InputDetector>();
        
        _firstCanonParticleSystem = _firstCanon.GetComponentInChildren<ParticleSystem>();
        _secondCanonParticleSystem = _secondCanon.GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        _readyToShootFirstCanon = true;
        _readyToShootSecondCanon = true;
    }

    private void Update()
    {
        LeftMachineGunShoot();
        RightMachineGunShoot();
        
        FirstCanonShoot();
        SecondCanonShoot();
        
        CheckLeftMachineGunOverheat();
        CheckRightMachineGunOverheat();
    }

    /// <summary>
    /// Método para controlar el sobrecalentamiento de la ametralladora izquierda;
    /// </summary>
    private void CheckLeftMachineGunOverheat()
    {
        //Comprobamos que estémos pulsando el trigger izquierdo y estémos disparando la ametralladora izquierda;
        if (_leftInputDetector.IsTriggering && _firstMachineGunShooting)
        {
            _currentLeftTimeShooting += Time.deltaTime; //Sumamos el tiempo que disparamos;
        }
        else
        {
            _currentLeftTimeShooting -= Time.deltaTime; //Restamos el tiempo que dejamos de disparar;
        }
        
        //Cambiamos el color del emisivo de la ametralladora para saber si se está sobrecalentando;
        Color color = new Color(_currentLeftTimeShooting - 8f,0f,0f);
        color.r = Mathf.Clamp(color.r, 0f, 255f);
        _leftMachineGunRenderer.material.SetColor("_EmissionColor", color);
        _leftMachineGunRenderer.material.EnableKeyword("_EMISSION");
        ///////////////////////////////////////////////////////////////////////////////////////////

        _currentLeftTimeShooting = Mathf.Clamp(_currentLeftTimeShooting, 0f, _maxTimeShooting); //límitamos el tiempo;

        //Si el tiempo que disparamos es superior al máximo tiempo q podemos disparar...
        //la ametralladora estará sobrecalentada;
        if (_currentLeftTimeShooting >= _maxTimeShooting)
        {
            _isLeftOverheated = true;
        }
        //Si el tiempo que disparamos es menor o igual a la mitad del tiempo máximo...
        //la ametralladora no estará sobrecalentada;
        else if (_currentLeftTimeShooting <= _maxTimeShooting/2)
        {
            _isLeftOverheated = false;
        }
    }

    /// <summary>
    /// Método para controlar el sobrecalentamiento de la ametralladora derecha;
    /// </summary>
    private void CheckRightMachineGunOverheat()
    {
        //Comprobamos que estémos pulsando el trigger derecho y estémos disparando la ametralladora derecha;
        if (_rightInputDetector.IsTriggering && _secondMachineGunShooting)
        {
            _currentRightTimeShooting += Time.deltaTime; //Sumamos el tiempo que disparamos;
        }
        else
        {
            _currentRightTimeShooting -= Time.deltaTime; //Restamos el tiempo que dejamos de disparar;
        }
        
        //Cambiamos el color del emisivo de la ametralladora para saber si se está sobrecalentando;
        Color color = new Color(_currentRightTimeShooting - 8f,0f,0f);
        color.r = Mathf.Clamp(color.r, 0f, 255f);
        _rightMachineGunRenderer.material.SetColor("_EmissionColor", color);
        _rightMachineGunRenderer.material.EnableKeyword("_EMISSION");
        ///////////////////////////////////////////////////////////////////////////////////////////

        _currentRightTimeShooting = Mathf.Clamp(_currentRightTimeShooting, 0f, _maxTimeShooting); //límitamos el tiempo;

        //Si el tiempo que disparamos es superior al máximo tiempo q podemos disparar...
        //la ametralladora estará sobrecalentada;
        if (_currentRightTimeShooting >= _maxTimeShooting)
        {
            _isRightOverheated = true;
        }
        //Si el tiempo que disparamos es menor o igual a la mitad del tiempo máximo...
        //la ametralladora no estará sobrecalentada;
        else if (_currentRightTimeShooting <= _maxTimeShooting/2)
        {
            _isRightOverheated = false;
        }
    }
    
    /// <summary>
    /// Método para disparar la ametralladora izquierda;
    /// </summary>
    /// <returns></returns>
    private void LeftMachineGunShoot()
    {
        if (_isLeftOverheated) //Si está sobrecalentada devolvemos falso y se paran las animaciones;
        {
            _antiAereoAnimations.SetBoolLeftAmmoAnimation(false);
            _antiAereoAnimations.SetBoolLeftMachineGun(false);
            
            _firstMachineGunShooting = false;
            return;
        }

        //Si se pulsa el trigger izquierdo y está agarrada la ametralladora izquierda...;
        if (_leftInputDetector.IsTriggering && _turretXrGrabInteractableTwoHanded.IsLeftGrab)
        {
            if (!(Time.time > _leftFireRateTime)) return; //Si el tiempo no es mayor al "_leftFireRateTime" devolvemos falso;
            
            //Se instancia una bala y se impulsa con una fuerza hacía adelante;
            GameObject bullet = Instantiate(_bulletPrefab, _firstMachineGunCanon.position, _firstMachineGunCanon.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(_firstMachineGunCanon.forward * _bulletImpulseForce, ForceMode.Impulse);
            
            //Se playean las particulas de disparo de la ametralladora y se manda vibración al mando
            _firstMachineGunParticleSystem.Play();
            _leftInputDetector.HapticFeedBack.ControllerVibration(1f, 0.1f);

            //Se suma en el "_leftFireRateTime" el tiempo transcurrido + el "_fireRate";
            _leftFireRateTime = Time.time + _fireRate;
            
            //Se activan las animaciones de la ametralladora;
            _antiAereoAnimations.SetBoolLeftAmmoAnimation(true);
            _antiAereoAnimations.SetBoolLeftMachineGun(true);
                
            //Se playea un sonido de disparo;
            AudioManager.instance.PlayOneShot("MachineGunShoot");

            _firstMachineGunShooting = true;
            return;
        }
        
        //Se paran las animaciones si no estamos apretando el trigger mientras agarramos la ametralladora;
        _antiAereoAnimations.SetBoolLeftAmmoAnimation(false);
        _antiAereoAnimations.SetBoolLeftMachineGun(false);
        
        _firstMachineGunShooting = false;
    }

    /// <summary>
    /// Método para disparar la ametralladora derecha;
    /// </summary>
    /// <returns></returns>
    private void RightMachineGunShoot()
    {
        if (_isRightOverheated) //Si está sobrecalentada devolvemos falso y se paran las animaciones;
        {
            _antiAereoAnimations.SetBoolRightAmmoAnimation(false);
            _antiAereoAnimations.SetBoolRightMachineGun(false);
            
            _secondMachineGunShooting = false;
            return;
        }
        
        //Si se pulsa el trigger derecho y está agarrada la ametralladora derecha...;
        if (_rightInputDetector.IsTriggering && _turretXrGrabInteractableTwoHanded.IsRightGrab)
        {
            if (!(Time.time > _rightFireRateTime)) return; //Si el tiempo no es mayor al "_rightFireRateTime" devolvemos falso;
            
            //Se instancia una bala y se impulsa con una fuerza hacía adelante;
            GameObject bullet = Instantiate(_bulletPrefab, _secondMachineGunCanon.position, _secondMachineGunCanon.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(_secondMachineGunCanon.forward * _bulletImpulseForce, ForceMode.Impulse);
            
            //Se playean las particulas de disparo de la ametralladora y se manda vibración al mando
            _secondMachineGunParticleSystem.Play();
            _rightInputDetector.HapticFeedBack.ControllerVibration(1f, 0.1f);

            //Se suma en el "_rightFireRateTime" el tiempo transcurrido + el "_fireRate";
            _rightFireRateTime = Time.time + _fireRate;
                
            //Se activan las animaciones de la ametralladora;
            _antiAereoAnimations.SetBoolRightAmmoAnimation(true);
            _antiAereoAnimations.SetBoolRightMachineGun(true);

            //Se playea un sonido de disparo si no estamos disparando la ametralladora izquierda;
            //(Esto evita que haya 2 sonidos sobrepuestos);
            if (!_firstMachineGunShooting)
            {
                AudioManager.instance.PlayOneShot("MachineGunShoot");
            }

            _secondMachineGunShooting = true;
            return;
        }

        //Se paran las animaciones si no estamos apretando el trigger mientras agarramos la ametralladora;
        _antiAereoAnimations.SetBoolRightAmmoAnimation(false);
        _antiAereoAnimations.SetBoolRightMachineGun(false);
        
        _secondMachineGunShooting = false;
    }

    /// <summary>
    /// Método para disparar el primer cañon;
    /// </summary>
    private void FirstCanonShoot()
    {
        if (!_readyToShootFirstCanon) return; //Si no está listo para disparar no hacemos nada;

        //Si no estamos presionando el botón primario o no estámos agarrando la ametralladora izquierda...;
        if (!_leftInputDetector.PrimaryButton.action.triggered || !_turretXrGrabInteractableTwoHanded.IsLeftGrab) return;
        
        //Llamamos al método que elige que cañon se ha disparado;
        ChooseCanonShoot(_firstCanon, _firstCanonParticleSystem);
            
        //Se setea la animación del cañon y hacemos que no esté listo para disparar;
        _antiAereoAnimations.SetTriggerFirstCanon();
        _readyToShootFirstCanon = false;
            
        //Se invoca al método que pondrá listo para disparar el cañon en 3s;
        Invoke(nameof(SetReadyToShootFirstCanon), 3f);
        
        AudioManager.instance.PlayOneShot("CanonShot");
    }

    /// <summary>
    /// Método para disparar el segundo cañon;
    /// </summary>
    private void SecondCanonShoot()
    {
        if (!_readyToShootSecondCanon) return; //Si no está listo para disparar no hacemos nada;
        
        //Si no estamos presionando el botón primario o no estámos agarrando la ametralladora derecha...;
        if (!_rightInputDetector.PrimaryButton.action.triggered || !_turretXrGrabInteractableTwoHanded.IsRightGrab) return;
        
        //Llamamos al método que elige que cañon se ha disparado;
        ChooseCanonShoot(_secondCanon, _secondCanonParticleSystem);
        
        //Se setea la animación del cañon y hacemos que no esté listo para disparar;
        _antiAereoAnimations.SetTriggerSecondCanon();
        _readyToShootSecondCanon = false;
        
        //Se invoca al método que pondrá listo para disparar el cañon en 3s;
        Invoke(nameof(SetReadyToShootSecondCanon), 3f);
        
        AudioManager.instance.PlayOneShot("CanonShot");
    }

    /// <summary>
    /// Método que escoge que cañon se tiene que disparar;
    /// </summary>
    /// <param name="canon"></param>
    /// <param name="canonParticleSystem"></param>
    private void ChooseCanonShoot(Transform canon, ParticleSystem canonParticleSystem)
    {
        //Se instancia una bala y se impulsa con una fuerza hacia adelante;
        GameObject bullet = Instantiate(_canonBulletPrefab, canon.position, canon.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(canon.forward * _canonBulletImpulseForce, ForceMode.Impulse);
        
        //Se playea las particulas de disparo;
        canonParticleSystem.Play();
    }

    /// <summary>
    /// Método para setear listo el disparo del primer cañon;
    /// </summary>
    private void SetReadyToShootFirstCanon()
    {
        _readyToShootFirstCanon = true;
    }

    /// <summary>
    /// Método para setear listo el disparo del segundo cañon;
    /// </summary>
    private void SetReadyToShootSecondCanon()
    {
        _readyToShootSecondCanon = true;
    }
}
