using UnityEngine;

public abstract class IPlayerState
{
    public abstract void StateEnter(PlayerController context);
    public abstract void StateUpdate(PlayerController context);
    public abstract void StateCheck(PlayerController context);
    public abstract void StateExit(PlayerController context);
}
