using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticFeedBack : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor _xrDirectInteractor;

    private void Awake()
    {
        _xrDirectInteractor = GetComponent<XRDirectInteractor>();
    }

    /// <summary>
    /// Método que manda vibración al mando;
    /// </summary>
    /// <param name="amplitud"></param>
    /// <param name="duration"></param>
    public void ControllerVibration(float amplitud, float duration)
    {
        _xrDirectInteractor.SendHapticImpulse(amplitud, duration);
    }
}
