using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour
{
    private Vector2 lastPositionMouse = Vector2.zero;
    [SerializeField] public float sense = 10f;
    [SerializeField] public float scaleSense = 100f;
    [SerializeField] public float rotationSpeed = 100f;

    // constants
    private float deltaFactor = 1000f;

    private float mouseScrollMultiplayer = 0.2f;

    [SerializeField] private float deltaMovementX;
    [SerializeField] private float deltaMovementY;
    [SerializeField] private float localPosX;
    [SerializeField] private float localPosY;
    [SerializeField] private float localToGlobalX;
    [SerializeField] private float localToGlobalY;

    //Rotation
    [SerializeField] private float angleTarget = 0;

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
            deltaMovementX = GetComponent<Camera>().orthographicSize * 2 / 180;
            deltaMovementY = deltaMovementX / Mathf.Cos((90 - transform.rotation.eulerAngles.x) * Mathf.Deg2Rad);

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


    private void RotateInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            angleTarget -= 45;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            angleTarget += 45;
        }
        if (angleTarget >= 360) angleTarget -= 360;
        if (angleTarget < 0) angleTarget += 360;

        RotationTranslator();
    }


    private void RotationTranslator()
    {
        if (Mathf.Abs(angleTarget - transform.rotation.eulerAngles.y) < 0.49)
        {
            transform.Rotate(0, Mathf.Round(transform.rotation.eulerAngles.y) - transform.rotation.eulerAngles.y, 0, Space.World);
        }
        else if (angleTarget < transform.rotation.eulerAngles.y || transform.rotation.eulerAngles.y - 360 - angleTarget <= 45) transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0, Space.World);
        else if (angleTarget > transform.rotation.eulerAngles.y || angleTarget - 360 - transform.rotation.eulerAngles.y <= 45) transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}

