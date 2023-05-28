using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyComponentsStorage _enemyComponentsStorage;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    
    //GETTERS && SETTERS//

    /////////////////////////////////

    private void Awake()
    {
        _enemyComponentsStorage = GetComponent<EnemyComponentsStorage>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Die();
    }
    
    private void Die()
    {
        foreach (EnemyCollidersHealth enemyBone in _enemyComponentsStorage.EnemyBonesList)
        {
            enemyBone.Rigidbody.isKinematic = false;
        }
        
        _enemyComponentsStorage.EnemyAnimationsController.Animator.enabled = false;
    }
}
