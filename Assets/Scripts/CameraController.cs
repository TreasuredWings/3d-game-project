using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float lookSpeed = 3f;
    public float orbitDamping = 10f;
    public InputActionReference lookAction;

    private Vector3 lookVector;

    private void AdjustCameraOrientation()
    {
        transform.position = target.transform.position;

        // look takes the value of lookAction (x, y)
        Vector2 look = lookAction.action.ReadValue<Vector2>();
        // look then clamps the magnitude to 1 unit, then multiplies by the lookSpeed
        // This gives us the distance TO MOVE, not the final position
        look = Vector2.ClampMagnitude(look, 1f) * lookSpeed;
        Vector3 convertedLook = new Vector3(-look.y, look.x, 0);
        transform.eulerAngles += convertedLook * Time.deltaTime;
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
