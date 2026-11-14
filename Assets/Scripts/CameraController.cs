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
    public Vector3 lookVector;

    private void AdjustCameraOrientation()
    {
        transform.position = target.transform.position;

        // look takes the value of lookAction (x, y)
        Vector2 look = lookAction.action.ReadValue<Vector2>();
        // look then clamps the magnitude to 1 unit, then multiplies by the lookSpeed
        // This gives us the distance TO MOVE, not the final position
        look = Vector2.ClampMagnitude(look, 1f) * lookSpeed;
        lookDelta = new Vector3(-look.y, look.x, 0) * Time.deltaTime;
        lookVector += lookDelta;
        lookVector.x = Mathf.Clamp(lookVector.x, verticalLowerBoundAngle, verticalUpperBoundAngle);
        if (lookVector.y > 180)
        {
            lookVector.y -= 360f;
        }
        else if (lookVector.y < -180)
        {
            lookVector.y += 360f;
        }
        transform.eulerAngles = lookVector;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
