using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float lookSpeed = 3f;
    public float verticalLowerBoundAngle = 0f;
    public float verticalUpperBoundAngle = 80f;
    public InputActionReference lookAction;

    private Vector3 lookDelta;
    private Vector3 lookVector;

    private void AdjustCameraOrientation()
    {
        // Track the target's position
        transform.position = target.transform.position;

        // look takes the value of lookAction (x, y)
        Vector2 look = lookAction.action.ReadValue<Vector2>();
        // look then clamps the magnitude to 1 unit, then multiplies by the lookSpeed
        look = Vector2.ClampMagnitude(look, 1f) * lookSpeed;
        // Create a look delta and assign the correct properties for it
        lookDelta = new Vector3(-look.y, look.x, 0) * Time.deltaTime;
        // Add the look delta to the look vector
        lookVector += lookDelta;
        // Clamp the look's vertical angle to the bounds
        lookVector.x = Mathf.Clamp(lookVector.x, verticalLowerBoundAngle, verticalUpperBoundAngle);
        // Reset the horizontal angle to prevent overflow
        if (lookVector.y > 180)
        {
            lookVector.y -= 360f;
        }
        else if (lookVector.y < -180)
        {
            lookVector.y += 360f;
        }
        // Set the rotation of the camera to the look vector's angles
        transform.eulerAngles = lookVector;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lookVector = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        AdjustCameraOrientation();
    }
}
