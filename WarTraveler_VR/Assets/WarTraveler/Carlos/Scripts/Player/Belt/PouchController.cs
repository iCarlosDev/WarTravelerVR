using UnityEngine;

public class PouchController : MonoBehaviour
{
    [Header("--- CAMERA ---")]
    [Space(10)]
    [SerializeField] private Camera _camera;
    
    [Header("--- POUCH PARAMS ---")]
    [Space(10)]
    [SerializeField] private Vector3 _beltPositionOffset;
    [SerializeField] private float _rotationSmoothMultiplier;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    void Update()
    { 
        PouchTransfomControl();   
    }

    /// <summary>
    /// Método para actualizar la posición y rotación de las cartucheras donde esté la cámara;
    /// </summary>
    private void PouchTransfomControl()
    {
        transform.position = _camera.transform.position + _beltPositionOffset;
        Quaternion cameraRotation = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraRotation, _rotationSmoothMultiplier * Time.deltaTime);
    }
}
