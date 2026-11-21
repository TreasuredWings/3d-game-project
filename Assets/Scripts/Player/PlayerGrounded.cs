using UnityEngine;

public class PlayerGrounded : IPlayerState
{
    public PlayerGrounded()
    {
        // Constructor if needed
    }

    public override void StateEnter(PlayerController player)
    {
        // Reset player dashes
        player.DashCount = player.DashMax;

        // Set new substate
        if (player.MoveInputActive)
        {
            player.SwitchSubState(new PlayerRunning());
        }
        else
        {
            player.SwitchSubState(new PlayerIdle());
        }
        
        Debug.Log("PlayerGrounded state entered");
    }

    public override void StateCheck(PlayerController player)
    {
        // Check transitions
        // Possible states to switch to:
        // Jumping, Falling

        if (player.JumpActive)
        {
            player.SwitchState(new PlayerJumping());
            return;
        }

        if (player.DashActive && player.DashCount >= 1)
        {
            player.SwitchState(new PlayerDashing());
            return;
        }

        if (!player.Controller.isGrounded)
        {
            player.SwitchState(new PlayerFalling());
            return;
        }
    }

    public override void StateUpdate(PlayerController player)
    {
        // Apply gravity
        player.VerticalSpeed = -player.FallingGravity;
    }

    public override void StateExit(PlayerController player)
    {
        
    }
}
