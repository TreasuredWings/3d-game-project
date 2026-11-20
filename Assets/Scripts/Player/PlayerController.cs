using System.Data;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float TopSpeed = 3f;
    public float Acceleration = 0.5f;
    public float Deceleration = 0.5f;

    [Header("Jumping and Falling Parameters")]
    public float JumpHeight = 5f;
    public float RisingGravity = -9.81f;
    public float FallingGravity = -9.81f;
    public int AirActions = 1;

    [Header("Component References")]
    public CharacterController Controller;
    public Transform Camera;

    // Private variables

    private IPlayerState _state;

    // Getters and setters

    // State-related variables
    public IPlayerState State {get {return _state;}}

    // Input-related variables
    public bool MoveInputActive {get; set;} = false;
    public Vector3 IncomingMovement {get; set;} = new Vector3();
    public bool JumpActive {get; set;} = false;

    // Movement-related variables
    public Vector3 AppliedMovement {get; set;} = new Vector3();
    public float CurrentSpeed {get; set;} = 0f;

    // Functions

    public void SwitchState(IPlayerState newState)
    {
        _state.StateExit(this);
        _state = newState;
        _state.StateEnter(this);
    }

    public void HandleInput()
    {
        // Player movement input
        // Set bool
        MoveInputActive = InputManager.inst.IsMoving();
        // Get movement with post-processing from InputManager
        Vector3 move = InputManager.inst.GetMoveVector();
        // Get camera rotation, projected on a horizontal plane
        Vector3 flatCameraDirection = Vector3.ProjectOnPlane(Camera.forward, Vector3.up);
        // Convert the camera direction vector to a quaternion
        Quaternion cameraRotation = Quaternion.LookRotation(flatCameraDirection, Vector3.up);
        // Rotate the move vector to face away from the camera (forwards is forwards now)
        IncomingMovement = cameraRotation * move;

        // Player jump input
        // NOTE: Input buffer is going to be handled by InputManager
        JumpActive = InputManager.inst.IsJumpPressed();
    }

    public void HandleRotate()
    {
        if (MoveInputActive)
        {
            transform.forward = IncomingMovement;
        }
    }

    // MonoBehavior fuctions

    void Start()
    {
        _state = new PlayerIdle(this);
    }

    // Update
    // NOTE: Only put things that happen regardless of state into the update function raw
    // All other functionality must be within its own state
    void Update()
    {
        HandleInput();
        State.StateUpdate(this);
        HandleRotate();
        Controller.Move(AppliedMovement * Time.deltaTime);
    }
}
