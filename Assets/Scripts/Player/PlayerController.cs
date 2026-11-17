using UnityEngine;

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

    IPlayerState _state;

    float _currentSpeed = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _state = new PlayerGrounded();
    }

    // Update is called once per frame
    void Update()
    {
        _state.StateUpdate(this);
    }
}
