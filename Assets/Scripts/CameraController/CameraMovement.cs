using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector2 lastPositionMouse = Vector2.zero;
    [SerializeField] public float sense;
    [SerializeField] public float rotationSpeed;

    // constants
    private float deltaFactor = 1000f;
    private float mouseScrollMultiplier = 0.2f;

    // Move
    [SerializeField] private float deltaMovementX;
    [SerializeField] private float deltaMovementY;
    [SerializeField] private float localPosX;
    [SerializeField] private float localPosY;
    [SerializeField] private float localToGlobalX;
    [SerializeField] private float localToGlobalY;
    [SerializeField] private float globalXOffset = 0;
    [SerializeField] private float globalYOffset = 0;

    // Rotation
    [SerializeField] private float angleTarget = 0;
    [SerializeField] private bool inRotation = false;
    [SerializeField] private float deltaAngle = 0;
    [SerializeField] private float currentRotSpeed;

    // Scale
    private float mouseScrollDelta;
    private float upperLimitOfScale = 10f;
    private float lowerLimitOfScale = 2f;

    // Optimization Variables
    private float senseDividedByDeltaFactor;
    private float cosYrot;
    private float sinYrot;
    private float discreteLocalX;
    private float discreteLocalY;


    private void Start()
    {

        deltaMovementX = GetComponent<Camera>().orthographicSize * 2 / 180;
        deltaMovementY = deltaMovementX / Mathf.Cos((90 - transform.rotation.eulerAngles.x) * Mathf.Deg2Rad);
    }

    private void Update()
    {
        MoveCamera();
        ScaleCamera();
        RotateInput();
        RotationTranslator();
    }

    private void MoveCamera()
    {
        if (Input.GetMouseButton(2))
        {
            if (lastPositionMouse != Vector2.zero && !inRotation)
            {
                senseDividedByDeltaFactor = sense / deltaFactor * GetComponent<Camera>().orthographicSize;
                cosYrot = Mathf.Cos(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);
                sinYrot = Mathf.Sin(transform.rotation.eulerAngles.y * Mathf.Deg2Rad);

                localPosX += (lastPositionMouse.x - Input.mousePosition.x) * senseDividedByDeltaFactor;
                localPosY += (lastPositionMouse.y - Input.mousePosition.y) * senseDividedByDeltaFactor;

                discreteLocalX = Mathf.Floor(localPosX / deltaMovementX) * deltaMovementX;
                discreteLocalY = Mathf.Floor(localPosY / deltaMovementY) * deltaMovementY;

                localToGlobalX = discreteLocalX * cosYrot + discreteLocalY * sinYrot + globalXOffset;
                localToGlobalY = discreteLocalY * cosYrot + discreteLocalX * -sinYrot + globalYOffset;

                transform.position = new Vector3(localToGlobalX, transform.position.y, localToGlobalY);
            }
            lastPositionMouse = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(2)) lastPositionMouse = Vector2.zero;
    }


    private void ScaleCamera()
    {
        mouseScrollDelta = -Input.mouseScrollDelta.y * mouseScrollMultiplier;
        if (mouseScrollDelta > 0 && upperLimitOfScale >= GetComponent<Camera>().orthographicSize)
        {
            GetComponent<Camera>().orthographicSize += mouseScrollDelta;
        }
        else if (mouseScrollDelta < 0 && lowerLimitOfScale <= GetComponent<Camera>().orthographicSize)
        {
            GetComponent<Camera>().orthographicSize += mouseScrollDelta;
        }
        if (mouseScrollDelta != 0)
        {
            deltaMovementX = GetComponent<Camera>().orthographicSize * 2 / 180;
            deltaMovementY = deltaMovementX / Mathf.Cos((90 - transform.rotation.eulerAngles.x) * Mathf.Deg2Rad);
        }
    }


    private void RotateInput()
    {
        if (Input.GetKeyDown(KeyCode.D)) angleTarget -= 45;
        else if (Input.GetKeyDown(KeyCode.A)) angleTarget += 45;

        if (angleTarget >= 360) angleTarget -= 360;
        if (angleTarget < 0) angleTarget += 360;
    }


    private void RotationTranslator()
    {
        inRotation = false;
        deltaAngle = Mathf.Abs(angleTarget - transform.rotation.eulerAngles.y);
        currentRotSpeed = LogCurve(deltaAngle) * rotationSpeed;
        if (deltaAngle < 0.49) transform.Rotate(0, Mathf.Round(transform.rotation.eulerAngles.y) - transform.rotation.eulerAngles.y, 0, Space.World);

        else if (angleTarget < 180 && transform.rotation.eulerAngles.y > 180) transform.Rotate(0, currentRotSpeed * Time.deltaTime, 0, Space.World);
        else if (angleTarget > 180 && transform.rotation.eulerAngles.y < 180) transform.Rotate(0, -currentRotSpeed * Time.deltaTime, 0, Space.World);

        else if (angleTarget > transform.rotation.eulerAngles.y) transform.Rotate(0, currentRotSpeed * Time.deltaTime, 0, Space.World);
        else if (angleTarget < transform.rotation.eulerAngles.y) transform.Rotate(0, -currentRotSpeed * Time.deltaTime, 0, Space.World);

        if (deltaAngle > 0.49)
        {
            inRotation = true;
            globalYOffset = localToGlobalY;
            localPosY = 0;
            globalXOffset = localToGlobalX;
            localPosX = 0;
        }
    }

    private float LogCurve(float x)
    {
        if (x > 315)
        {
            return Mathf.Log(361 - x, 45) + 0.25f;
        }
        return Mathf.Log(x + 1, 45) + 0.25f;
    }
}