using UnityEngine;

public class PlayerIdle : IPlayerState
{
    public PlayerIdle()
    {
        // Initialization
    }

    public override void StateEnter(PlayerController player)
    {
        // Do animation calls
        //Debug.Log("PlayerIdle state entered");
    }

    public override void StateCheck(PlayerController player)
    {
        // If the left stick is tilted
        // Go to the moving state
        if (player.MoveInputActive)
        {
            player.SwitchSubState(new PlayerRunning());
            return;
        }
    }

    public override void StateUpdate(PlayerController player)
    {
        // Apply deceleration to speed
        player.CurrentSpeed -= player.Deceleration;
        player.CurrentSpeed = Mathf.Clamp(player.CurrentSpeed, 0, player.TopSpeed);
    }

    public override void StateExit(PlayerController player)
    {
        // Not sure what to do here
    }
}
