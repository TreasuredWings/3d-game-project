using UnityEngine;

public class PlayerJumping : IPlayerState
{
    public PlayerJumping()
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

        // Apply jump force
        player.VerticalSpeed = Mathf.Sqrt(player.JumpHeight * 2f * player.RisingGravity);

        Debug.Log("PlayerJumping state entered");
    }

    public override void StateCheck(PlayerController player)
    {
        // Check transitions
        // Possible states to switch to:
        // Grounded, Falling

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

        if (player.VerticalSpeed <= 0)
        {
            player.SwitchState(new PlayerFalling());
            return;
        }
    }

    public override void StateUpdate(PlayerController player)
    {
        // Apply gravity
        player.VerticalSpeed += -player.RisingGravity * Time.deltaTime;
    }

    public override void StateExit(PlayerController player)
    {
        
    }
}
