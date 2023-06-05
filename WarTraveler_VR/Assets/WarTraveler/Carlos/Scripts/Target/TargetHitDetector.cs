using UnityEngine;

public class TargetHitDetector : MonoBehaviour
{
    [SerializeField] private TargetAnimationControl _targetAnimationControl;
    
    [SerializeField] private int _scoreToAdd;

    private void Awake()
    {
        _targetAnimationControl = GetComponentInParent<TargetAnimationControl>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Bullet") || !_targetAnimationControl.IsInTrincheras) return;
        
        _targetAnimationControl.SetTargetDown();
        TrincherasManager.instance.Score += _scoreToAdd;
    }
}
