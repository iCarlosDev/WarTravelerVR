using UnityEngine;
using Random = UnityEngine.Random;

public class PlaneBehaviour : MonoBehaviour
{
    [Header("Speed")]
    public float min_speed;    
    public float max_speed;

    [Space]
    [Header("Height")]
    public float min_height;    
    public float max_height;
    public float crashHeight;

    public float currentSpeed;
    
    void Start()
    {
        currentSpeed = Random.Range(min_speed, max_speed +1);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EndMap"))
        {
            float newHeight = Random.Range(min_height, max_height);

            if (transform.position.y - newHeight < crashHeight)
            {
                transform.position = new Vector3(transform.position.x, crashHeight, transform.position.z - 2000f);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + newHeight, transform.position.z - 2000f);
            }
            
        }
    }
}
