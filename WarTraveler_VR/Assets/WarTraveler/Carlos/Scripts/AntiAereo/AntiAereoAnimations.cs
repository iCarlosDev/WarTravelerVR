using UnityEngine;

public class AntiAereoAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetBoolLeftAmmoAnimation(bool _bool)
    {
        _animator.SetBool("LeftAmmoMovement", _bool);
    }
    
    public void SetBoolRightAmmoAnimation(bool _bool)
    {
        _animator.SetBool("RightAmmoMovement", _bool);
    }
    
    public void SetBoolLeftMachineGun(bool _bool)
    {
        _animator.SetBool("LeftAmmoMachineGunShoot", _bool);
    }
    
    public void SetBoolRightMachineGun(bool _bool)
    {
        _animator.SetBool("RightAmmoMachineGunShoot", _bool);
    }

    public void SetTriggerFirstCanon()
    {
        _animator.SetTrigger("FirstCanonShoot");
    }
    
    public void SetTriggerSecondCanon()
    {
        _animator.SetTrigger("SecondCanonShoot");
    }
}
