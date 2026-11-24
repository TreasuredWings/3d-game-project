using UnityEngine;

public class PlayerDashing : IPlayerState
{
    private float _distanceDashed = 0f;

    public PlayerDashing()
    {
        
    }

    public override void StateEnter(PlayerController player)
    {
        if (player.MoveInputActive)
        {
            player.transform.forward = player.IncomingMovement;
        }
        player.VerticalSpeed = 0f;
        player.DashCount--;
    }

    public override void StateCheck(PlayerController player)
    {
        if (_distanceDashed < player.DashDistance)
        {
            return;
        }

        if (player.Controller.isGrounded)
        {
            player.SwitchState(new PlayerGrounded());
            return;
        }

        player.SwitchState(new PlayerFalling());
    }

    public override void StateUpdate(PlayerController player)
    {
        player.CurrentSpeed = player.DashSpeed;
        _distanceDashed += player.DashSpeed * Time.deltaTime;
    }

    public override void StateExit(PlayerController player)
    {
        
    }
}
