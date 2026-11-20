using System;
using UnityEngine;

public class PlayerRunning : IPlayerState
{
    public PlayerRunning()
    {
        Debug.Log("PlayerRunning state initialized");
    }

    public override void StateEnter(PlayerController context)
    {
        Debug.Log("PlayerRunning state entered");
    }

    public override void StateUpdate(PlayerController context)
    {
        context.CurrentSpeed += context.Acceleration;
        context.CurrentSpeed = Mathf.Clamp(context.CurrentSpeed, 0, context.TopSpeed);
        context.AppliedMovement = context.IncomingMovement * context.CurrentSpeed;
    }

    public override void StateExit(PlayerController context)
    {
        Debug.Log("PlayerRunning state exited");
    }
}