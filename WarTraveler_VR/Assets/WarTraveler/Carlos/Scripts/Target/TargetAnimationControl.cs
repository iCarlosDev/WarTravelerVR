using UnityEngine;

public class TargetAnimationControl : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void SetTargetDown()
    {
        _animator.SetTrigger("TargetDown");

        if (IsInvoking(nameof(SetTargetUp))) CancelInvoke(nameof(SetTargetUp));
        Invoke(nameof(SetTargetUp), 5f);
    }

    private void SetTargetUp()
    {
        _animator.SetTrigger("TargetUp");
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Bullet"))
        {
            SetTargetDown();
        }
    }
}
