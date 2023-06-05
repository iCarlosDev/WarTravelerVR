using UnityEngine;

public class TargetAnimationControl : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isInTrincheras;

    public bool IsInTrincheras
    {
        get => _isInTrincheras;
        set => _isInTrincheras = value;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetTargetDown()
    {
        _animator.SetTrigger("TargetDown");

        if (_isInTrincheras) return;
      
        if (IsInvoking(nameof(SetTargetUp))) CancelInvoke(nameof(SetTargetUp));
        Invoke(nameof(SetTargetUp), 5f);
    }

    private void SetTargetUp()
    {
        _animator.SetTrigger("TargetUp");
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Bullet")) return;
        
        SetTargetDown();
    }
}
