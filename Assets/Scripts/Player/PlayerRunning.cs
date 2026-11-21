using System;
using UnityEngine;

public class PlayerRunning : IPlayerState
{
    public PlayerRunning()
    {
        
    }

    public override void StateEnter(PlayerController player)
    {
        //Debug.Log("PlayerRunning state entered");
    }

    public override void StateCheck(PlayerController player)
    {
        if (!player.MoveInputActive)
        {
            player.SwitchSubState(new PlayerIdle());
            return;
        }
    }

    public override void StateUpdate(PlayerController player)
    {
        // Accelerate
        player.CurrentSpeed += player.Acceleration;
        player.CurrentSpeed = Mathf.Clamp(player.CurrentSpeed, 0, player.TopSpeed);
    }

    public override void StateExit(PlayerController player)
    {
        
    }
}