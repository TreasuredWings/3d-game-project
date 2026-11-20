using UnityEngine;

public class PlayerJumping : IPlayerState
{
    public PlayerJumping()
    {
        
    }

    public override void StateEnter(PlayerController context)
    {
        Debug.Log("PlayerJumping state entered");
        // Apply jump force
        context.VerticalSpeed = Mathf.Sqrt(context.JumpHeight * 2f * context.RisingGravity);
    }

    public override void StateUpdate(PlayerController context)
    {
        // Apply gravity
        context.VerticalSpeed = -context.RisingGravity;
    }

    public override void StateCheck(PlayerController context)
    {
        
    }

    public override void StateExit(PlayerController context)
    {
        
    }
}
