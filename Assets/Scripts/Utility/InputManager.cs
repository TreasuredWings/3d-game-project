using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager inst;

    [Header("Action References")]
    public InputActionReference LookAction;
    public InputActionReference MoveAction;
    public InputActionReference JumpAction;

    public Vector3 GetMoveVector()
    {
        Vector2 input = MoveAction.action.ReadValue<Vector2>();
        Vector3 value = new Vector3(input.x, 0, input.y);
        return value;
    }

    public Vector3 GetLookVector()
    {
        Vector2 temp = LookAction.action.ReadValue<Vector2>();
        Vector3 value = new Vector3(-temp.y, temp.x, 0);
        return value;
    }

    public bool JumpPressed()
    {
        return JumpAction.action.triggered;
    }

    void Start()
    {
        inst = this;
    }

    private void OnEnable()
    {
        MoveAction.action.Enable();
        JumpAction.action.Enable();
    }

    private void OnDisable()
    {
        MoveAction.action.Disable();
        JumpAction.action.Disable();
    }
}
