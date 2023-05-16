using Unity.Mathematics;
using UnityEngine;

public class BeltController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3 _beltPositionOffset;
    [SerializeField] private float _rotationSmoothMultiplier;

    private void Awake()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _camera.transform.position + _beltPositionOffset;
        Quaternion cameraRotation = Quaternion.Euler(0, _camera.transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraRotation, _rotationSmoothMultiplier * Time.deltaTime);
    }
}
