using UnityEngine;

public class AntiAereoAnimations : MonoBehaviour
{
    [Header("--- ANIMATOR ---")]
    [Space(10)]
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Método que cambia el estado de la animación de la munición izquierda del antiaereo;
    /// </summary>
    /// <param name="_bool"></param>
    public void SetBoolLeftAmmoAnimation(bool _bool)
    {
        _animator.SetBool("LeftAmmoMovement", _bool);
    }
    
    /// <summary>
    /// Método que cambia el estado de la animación de la munición derecha del antiaereo;
    /// </summary>
    /// <param name="_bool"></param>
    public void SetBoolRightAmmoAnimation(bool _bool)
    {
        _animator.SetBool("RightAmmoMovement", _bool);
    }
    
    /// <summary>
    /// Método que cambia el estado de la animación del ametralladora izquierda del antiaereo;
    /// </summary>
    /// <param name="_bool"></param>
    public void SetBoolLeftMachineGun(bool _bool)
    {
        _animator.SetBool("LeftMachineGunShoot", _bool);
    }
    
    /// <summary>
    /// Método que cambia el estado de la animación de la ametralladora derecha del antiaereo;
    /// </summary>
    /// <param name="_bool"></param>
    public void SetBoolRightMachineGun(bool _bool)
    {
        _animator.SetBool("RightMachineGunShoot", _bool);
    }

    /// <summary>
    /// Método que cambia el estado de la animación del primer cañon del antiaereo;
    /// </summary>
    public void SetTriggerFirstCanon()
    {
        _animator.SetTrigger("FirstCanonShoot");
    }
    
    /// <summary>
    /// Método que cambia el estado de la animación del segundo cañon del antiaereo;
    /// </summary>
    public void SetTriggerSecondCanon()
    {
        _animator.SetTrigger("SecondCanonShoot");
    }
}
