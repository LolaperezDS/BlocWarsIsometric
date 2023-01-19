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
    [SerializeField] private float localPosX;
    [SerializeField] private float localPosY;
    [SerializeField] private float localToGlobalX;
    [SerializeField] private float localToGlobalY;

    private void Update()
    {
        deltaMovementX = GetComponent<Camera>().orthographicSize * 2 / 180;
        deltaMovementY = deltaMovementX / Mathf.Cos((90 - transform.rotation.eulerAngles.x) * Mathf.Deg2Rad);

        MoveCamera();
        ScaleCamera();
        RotateCam();

        Ray camDirOfView = new Ray();
        camDirOfView.direction = Vector3.forward;
        camDirOfView.origin = transform.position;
        Debug.DrawRay(camDirOfView.origin, camDirOfView.direction, Color.red);
    }


    private void MoveCamera()
    {
        if (Input.GetMouseButton(2))
        {
            if (lastPositionMouse != Vector2.zero)
            {

                localPosX += (lastPositionMouse.x - Input.mousePosition.x) * sense / deltaFactor;
                localPosY += (lastPositionMouse.y - Input.mousePosition.y) * sense / deltaFactor;

                localToGlobalX = Mathf.Floor(localPosX / deltaMovementX) * deltaMovementX * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) +
                                 Mathf.Floor(localPosY / deltaMovementY) * deltaMovementY * Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
                localToGlobalY = Mathf.Floor(localPosY / deltaMovementY) * deltaMovementY * Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad) +
                                 Mathf.Floor(localPosX / deltaMovementX) * deltaMovementX * -Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);

                transform.position = new Vector3(localToGlobalX, transform.position.y, localToGlobalY);
            }
            lastPositionMouse = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(2))
        {
            lastPositionMouse = Vector2.zero;
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
        if (Input.GetKeyDown(KeyCode.D)) transform.Rotate(0, -45, 0, Space.World);
        else if (Input.GetKeyDown(KeyCode.A)) transform.Rotate(0, 45, 0, Space.World);
    }
}
