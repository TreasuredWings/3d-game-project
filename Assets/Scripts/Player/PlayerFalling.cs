using UnityEngine;

public class PlayerFalling : IPlayerState
{
    public PlayerFalling()
    {
        // Constructor if needed
    }

    public override void StateEnter(PlayerController player)
    {
        // Set new substate
        if (player.MoveInputActive)
        {
            player.SwitchSubState(new PlayerRunning());
        }
        else
        {
            player.SwitchSubState(new PlayerIdle());
        }

        Debug.Log("PlayerFalling state entered");
    }

    public override void StateCheck(PlayerController player)
    {
        // Check transitions
        // Possible states to switch to:
        // Grounded
        // NOTE: The player can't jump again, no transition needed

        if (player.DashActive && player.DashCount >= 1)
        {
            player.SwitchState(new PlayerDashing());
            return;
        }

        if (player.Controller.isGrounded)
        {
            player.SwitchState(new PlayerGrounded());
            return;
        }
    }

    public override void StateUpdate(PlayerController player)
    {
        // Apply gravity
        player.VerticalSpeed += -player.FallingGravity * Time.deltaTime;
    }

    public override void StateExit(PlayerController player)
    {
        
    }
}
