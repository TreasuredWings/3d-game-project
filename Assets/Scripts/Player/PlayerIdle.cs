using UnityEngine;

public class PlayerIdle : IPlayerState
{
    public PlayerIdle(PlayerController context)
    {
        // Initialization
        Debug.Log("PlayerIdle state instantiated");
    }

    public override void StateEnter(PlayerController context)
    {
        // Do animation calls
        Debug.Log("PlayerIdle state entered");
    }

    public override void StateUpdate(PlayerController context)
    {
        // Apply gravity

        // Apply deceleration
        context.CurrentSpeed -= context.Deceleration;
        context.CurrentSpeed = Mathf.Clamp(context.CurrentSpeed, 0, context.TopSpeed);

        // Transitions
        // If the jump button is pressed
        // Go to the jumping state
        if (context.MoveInputActive)
        {
            context.SwitchState(new PlayerRunning());
        }

        // If the left stick is tilted
        // Go to the moving state

        if (!context.Controller.isGrounded)
        {
            // Go to falling state
        }
    }

    public override void StateExit(PlayerController context)
    {
        // Not sure what to do here
        Debug.Log("PlayerIdle state exiting");
    }
}
