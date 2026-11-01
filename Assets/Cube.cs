using UnityEngine;
using UnityEngine.InputSystem;

public class Cube : MonoBehaviour
{
    InputAction moveAction;
    float speed = 0.01f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>() * speed;
        transform.position += new Vector3(moveValue.x, moveValue.y, 0);
    }
}
