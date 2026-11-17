using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
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

    [Header("Debug Toggles")]

    private Vector3 playerVelocity;
    private float currentSpeed = 0f;
    private bool grounded;
    private int airJumpsReset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        airJumpsReset = airJumps;
    }

    // Update is called once per frame
    void Update()
    {
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
        Vector3 move = InputManager.inst.GetMoveVector();

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
        bool jumpTriggered = InputManager.inst.JumpPressed();
        if ((grounded && jumpTriggered) || (airJumps > 0 && jumpTriggered))
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
