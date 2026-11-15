using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float topSpeed = 3f;
    public float acceleration = 0.5f;
    public float deceleration = 0.5f;

    [Header("Jumping and Falling Parameters")]
    public float jumpHeight = 5f;
    public float risingGravity = -9.81f;
    public float fallingGravity = -9.81f;
    public int airJumps = 1;

    [Header("Component References")]
    public CharacterController controller;
    public Transform camera;

    [Header("Action References")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;

    [Header("Debug Toggles")]
    public bool showTrail = false;

    private Vector3 playerVelocity;
    private float currentSpeed = 0f;
    private bool grounded;
    private int airJumpsReset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        airJumpsReset = airJumps;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug trail
        if (showTrail)
        {
            // TODO: Add a trail to the player's center
        }

        // Prevent falling through the floor
        grounded = controller.isGrounded;
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        // Reset air jumps
        if (grounded)
        {
            airJumps = airJumpsReset;
        }

        // Read input
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = Vector3.ClampMagnitude(move, 1f);

        // Get camera rotation, projected on a horizontal plane
        Vector3 flatCameraDirection = Vector3.ProjectOnPlane(camera.forward, Vector3.up);
        // Convert the camera direction vector to a quaternion
        Quaternion cameraRotation = Quaternion.LookRotation(flatCameraDirection, Vector3.up);
        // Rotate the move vector to face away from the camera (forwards is forwards now)
        move = cameraRotation * move;

        // Check for direction being held
        if (move != Vector3.zero)
        {
            transform.forward = move;
            currentSpeed += acceleration;
        }
        else
        {
            currentSpeed -= deceleration;
        }

        // Bind currentSpeed by 0 and topSpeed
        currentSpeed = Mathf.Clamp(currentSpeed, 0, topSpeed);

        // Handle jumping
        if ((grounded && jumpAction.action.triggered) || (airJumps > 0 && jumpAction.action.triggered))
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * risingGravity);
            if (!grounded)
            {
                airJumps--;
            }
        }

        // Apply gravity
        if (playerVelocity.y > 0)
        {
            playerVelocity.y += risingGravity * Time.deltaTime;
        }
        else
        {
            playerVelocity.y += fallingGravity * Time.deltaTime;
        }

        // Combine all movement
        Vector3 finalMove = (move * currentSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }
}
