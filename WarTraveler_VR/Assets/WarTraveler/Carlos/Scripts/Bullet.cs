using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("BulletNoCollisionable")) return;
        Destroy(gameObject);
    }
}
