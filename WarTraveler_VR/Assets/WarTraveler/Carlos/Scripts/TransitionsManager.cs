using UnityEngine;

public class TransitionsManager : MonoBehaviour
{
    public static TransitionsManager instance;

    [SerializeField] private Animator _animator;
    
    private void Awake()
    {
        KeepInstance();
        _animator = GetComponent<Animator>();
    }

    private void KeepInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void Fade_IN()
    {
        _animator.SetTrigger("Fade_IN");
    }

    public void Fade_OUT()
    {
        _animator.SetTrigger("Fade_OUT");
    }
}
