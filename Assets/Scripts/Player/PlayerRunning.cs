using System;
using UnityEngine;

public class PlayerRunning : IPlayerState
{
    public PlayerRunning()
    {
        
    }

    public override void StateEnter(PlayerController context)
    {
        Debug.Log("PlayerRunning state entered");
    }

    public override void StateUpdate(PlayerController context)
    {
        // Apply gravity
        context.VerticalSpeed = -context.FallingGravity;

        // Accelerate
        context.CurrentSpeed += context.Acceleration;
        context.CurrentSpeed = Mathf.Clamp(context.CurrentSpeed, 0, context.TopSpeed);
    }
    public override void StateCheck(PlayerController context)
    {
        if (!context.MoveInputActive)
        {
            context.SwitchState(new PlayerIdle());
        }
    }

    public override void StateExit(PlayerController context)
    {
        
    }
}