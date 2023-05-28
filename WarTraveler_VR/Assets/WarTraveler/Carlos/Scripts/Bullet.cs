using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _bulletDamage;

    public int BulletDamage => _bulletDamage;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f); //Destruimos el objeto después de 3s para no tener muchos instanciados;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Si colisiona con algo que no queramos o un arma no se destruirá;
        if (collision.transform.CompareTag("BulletNoCollisionable") || collision.transform.CompareTag("Weapon")) return;
        
        Destroy(gameObject);
    }
}
