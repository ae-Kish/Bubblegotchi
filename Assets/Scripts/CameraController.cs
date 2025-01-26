using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float maxFov = 110f;
    public float minFov = 62f;
    public int zoomSpeed = 15;

    private float fov;

    private Camera _camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        fov = _camera.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        _camera.fieldOfView = fov;
    }
}
