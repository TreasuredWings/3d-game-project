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

    private Vector3 _lookVector;
    private Vector3 _lookAngle;

    private void AdjustCameraOrientation()
    {
        // Track the target's position
        transform.position = target.transform.position;
        
        // Get a look vector from input manager
        _lookVector = InputManager.inst.GetLookVector();
        // Increase the speed by the look speed and adjust for framerate
        _lookVector = _lookVector * lookSpeed * Time.deltaTime;
        // Add the look delta to the look vector
        _lookAngle += _lookVector;
        // Clamp the look's vertical angle to the bounds
        _lookAngle.x = Mathf.Clamp(_lookAngle.x, verticalLowerBoundAngle, verticalUpperBoundAngle);
        // Reset the horizontal angle to prevent overflow
        if (_lookAngle.y > 180)
        {
            _lookAngle.y -= 360f;
        }
        else if (_lookAngle.y < -180)
        {
            _lookAngle.y += 360f;
        }
        // Set the rotation of the camera to the look vector's angles
        transform.eulerAngles = _lookAngle;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lookAngle = Vector3.zero;
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
