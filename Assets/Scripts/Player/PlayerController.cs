using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float TopSpeed = 3f;
    public float Acceleration = 0.5f;
    public float Deceleration = 0.5f;
    public float DashDistance = 25f;
    public float DashSpeed = 15f;
    public int DashMax = 1;

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
    private IPlayerState _subState;

    // Getters and setters

    // State-related variables
    public IPlayerState State {get {return _state;}}
    public IPlayerState SubState {get {return _subState;}}

    // Input-related variables
    public bool MoveInputActive {get; set;} = false;
    public Vector3 IncomingMovement {get; set;} = new Vector3();
    public bool JumpActive {get; set;} = false;
    public bool DashActive {get; set;} = false;
    public int DashCount {get; set;}

    // Movement-related variables
    public Vector3 HorizontalMovement {get; set;} = new Vector3();
    public float VerticalSpeed {get; set;} = 0f;
    public Vector3 AppliedMovement {get; set;} = new Vector3();
    public float CurrentSpeed {get; set;} = 0f;

    // Functions

    public void SwitchState(IPlayerState newState)
    {
        _state.StateExit(this);
        _state = newState;
        _subState = null;
        _state.StateEnter(this);
    }

    public void SwitchSubState(IPlayerState newSubState)
    {
        if (_subState != null)
        {
            _subState.StateExit(this);
        }
        _subState = newSubState;
        if (_subState != null)
        {
            _subState.StateEnter(this);
        }
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

        // Player dash input
        DashActive = InputManager.inst.IsDashPressed();
    }

    public void ApplyMoves()
    {
        HorizontalMovement = transform.forward;
        AppliedMovement = (HorizontalMovement * CurrentSpeed) + (Vector3.up * VerticalSpeed);
        Controller.Move(AppliedMovement * Time.deltaTime);
    }

    // MonoBehavior fuctions

    void Start()
    {
        // Initialize unassigned variables
        DashCount = DashMax;

        _state = new PlayerFalling();
        _state.StateEnter(this);
    }

    // Update
    // NOTE: Only put things that happen regardless of state into the update function raw
    // All other functionality must be within its own state
    void Update()
    {
        // Process inputs from the player
        HandleInput();

        // Check for any needed state transitions based on player input
        _state.StateCheck(this);
        if (_subState != null)
        {
            _subState.StateCheck(this);
        }

        // Handle each state from Super -> Sub
        _state.StateUpdate(this);
        if (_subState != null)
        {
            _subState.StateUpdate(this);
        }

        // Finalize movement and apply it
        ApplyMoves();
    }
}
