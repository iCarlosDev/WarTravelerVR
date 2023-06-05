using UnityEngine;

public class DianaTrincheraCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        TrincherasManager.instance.SetDianaUp();
        gameObject.SetActive(false);
    }
}
