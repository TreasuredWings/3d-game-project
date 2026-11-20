using UnityEngine;

public abstract class IPlayerState
{
    public abstract void StateEnter(PlayerController context);
    public abstract void StateUpdate(PlayerController context);
    public abstract void StateExit(PlayerController context);

    public IPlayerState Transition(PlayerController context, IPlayerState newState)
    {
        StateExit(context);
        newState.StateEnter(context);
        return newState;
    }
}
