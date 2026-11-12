using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float lookSpeed = 10f;
    public InputActionReference lookAction;

    private void AdjustCameraOrientation()
    {
        Vector2 look = lookAction.action.ReadValue<Vector2>();
        look = Vector2.ClampMagnitude(look, 1f);

        Debug.Log(look);

        Vector3 finalLook = look * lookSpeed;
        transform.eulerAngles += finalLook;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.SetParent(target);
    }

    // Update is called once per frame
    void Update()
    {
        AdjustCameraOrientation();
    }
}
