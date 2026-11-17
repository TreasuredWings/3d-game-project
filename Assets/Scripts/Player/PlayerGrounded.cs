using UnityEngine;

public class PlayerGrounded : IPlayerState
{
    public PlayerGrounded()
    {
        // Initialization
        Debug.Log("PlayerGrounded instantiated");
    }

    public override void StateEnter(PlayerController context)
    {
        throw new System.NotImplementedException();
    }

    public override void StateUpdate(PlayerController context)
    {
        throw new System.NotImplementedException();
    }

    public override void StateExit(PlayerController context)
    {
        throw new System.NotImplementedException();
    }
}
