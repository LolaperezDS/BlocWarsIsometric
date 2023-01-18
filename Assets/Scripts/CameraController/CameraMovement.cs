using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector2 lastPositionMouse = new Vector2(0, 0);
    [SerializeField] public float sense = 10f;
    [SerializeField] public float scaleSense = 100f;

    // constants
    private float deltaFactor = 1000f;

    private float mouseScrollMultiplayer = 0.2f;

    [SerializeField] private float deltaMovementX;
    [SerializeField] private float deltaMovementY;

    [SerializeField] private Vector3 continiousPosition;

    private void Update()
    {
        deltaMovementX = GetComponent<Camera>().orthographicSize * 2 / 180;
        deltaMovementY = deltaMovementX / Mathf.Cos(20 * Mathf.Deg2Rad);

        MoveCamera();
        ScaleCamera();
        RotateCam();
    }


    private void MoveCamera()
    {
        if (Input.GetMouseButton(2))
        {
            if (lastPositionMouse != new Vector2(0, 0))
            {
                continiousPosition += new Vector3((lastPositionMouse.x - Input.mousePosition.x), 0, (lastPositionMouse.y - Input.mousePosition.y)) * sense / deltaFactor;



                transform.position = new Vector3(Mathf.Floor(continiousPosition.x / deltaMovementX) * deltaMovementX,
                                                 transform.position.y,
                                                 Mathf.Floor(continiousPosition.z / deltaMovementY) * deltaMovementY);
            }
            lastPositionMouse = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(2))
        {
            lastPositionMouse = new Vector2(0, 0);
        }
    }


    private void ScaleCamera()
    {
        float mouseScrollDelta = Input.mouseScrollDelta.y;

        if (true)
        {
            GetComponent<Camera>().orthographicSize -= mouseScrollDelta * mouseScrollMultiplayer;
        }
    }


    private void RotateCam()
    {

    }
}
