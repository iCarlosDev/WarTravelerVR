using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _bulletDamage;

    public int BulletDamage => _bulletDamage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("BulletNoCollisionable") || collision.transform.CompareTag("Weapon")) return;
        Destroy(gameObject);
    }
}
