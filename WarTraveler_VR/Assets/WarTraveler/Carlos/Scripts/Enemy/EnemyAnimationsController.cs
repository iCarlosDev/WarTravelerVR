using UnityEngine;

public class EnemyAnimationsController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    //GETTERS && SETTERS//
    public Animator Animator => _animator;
    
    //////////////////////////////////////////
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
