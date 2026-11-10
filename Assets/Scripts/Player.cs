using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float gravity = 0.8f;
    public float acceleration = 0.5f;
    public float deceleration = 0.5f;
    public float topSpeed = 3f;
    public float jumpHeight = 5f;
    public bool showTrail = false;

    public CharacterController controller;

    private Vector3 playerVelocity;
    private float currentSpeed = 0f;
    private bool grounded;

    public InputActionReference moveAction;
    public InputActionReference jumpAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        if (showTrail)
        {
            // Add a trail to the player's center
        }

        // Prevent falling through the floor
        grounded = controller.isGrounded;
        if (grounded && playerVelocity.y > 0)
        {
            playerVelocity.y = 0;
        }

        // Read input
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = Vector3.ClampMagnitude(move, 1f);

        if (move != Vector3.zero)
        {
            transform.forward = move;
            currentSpeed += acceleration;
        }
        else
        {
            currentSpeed -= deceleration;
        }

        if (currentSpeed > topSpeed)
        {
            currentSpeed = topSpeed;
        }
        else if (currentSpeed < 0)
        {
            currentSpeed = 0;
        }

        // Handle jumping
        if (grounded && jumpAction.action.triggered)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // Combine all movement
        Vector3 finalMove = (move * currentSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }
}
