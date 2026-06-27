using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Panning")]
    [SerializeField] private float _panSpeed = 25f;
    [SerializeField] private float _edgeScrollSize = 20f; //pixels from edge before panning

    [Header("Zooming")]
    [SerializeField] private float _zoomSpeed = 25f;
    [SerializeField] private float _minZoom = 10f;
    [SerializeField] private float _maxZoom = 50f;
    [SerializeField] private float _zoomSmoothing = 10f;

    private Camera _camera;
    private float targetZoom;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        targetZoom = _camera.orthographicSize;
    }

    private void Update()
    {
        Vector3 moveInput = Vector3.zero;

        //WASD
        if (Input.GetKey(KeyCode.W)) moveInput.z += 1f;
        if (Input.GetKey(KeyCode.A)) moveInput.x -= 1f;
        if (Input.GetKey(KeyCode.S)) moveInput.z -= 1f;
        if (Input.GetKey(KeyCode.D)) moveInput.x += 1f;

        //Edge Scroll
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        if (mouseX >= 0 && mouseX <= _edgeScrollSize) moveInput.x -= 1f;
        if (mouseX <= Screen.width && mouseX >= Screen.width - _edgeScrollSize) moveInput.x += 1f;
        if (mouseY >= 0 && mouseY <= _edgeScrollSize) moveInput.z -= 1f;
        if (mouseY <= Screen.height && mouseY >= Screen.height - _edgeScrollSize) moveInput.z += 1f;

        Vector3 moveDirection = moveInput.normalized * _panSpeed * Time.deltaTime;
        transform.Translate(moveDirection, Space.World);

        // 3. Zooming (Scroll Wheel) - smoothed
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scroll * _zoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, _minZoom, _maxZoom);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, targetZoom, Time.deltaTime * _zoomSmoothing);
    }



}
