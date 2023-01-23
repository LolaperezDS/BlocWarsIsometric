using UnityEngine;

public class ScaleRepeater : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        GetComponent<Camera>().orthographicSize = _camera.orthographicSize;
    }
}
