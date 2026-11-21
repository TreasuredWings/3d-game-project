using UnityEngine;

public abstract class IPlayerState
{
    public abstract void StateEnter(PlayerController player);
    public abstract void StateUpdate(PlayerController player);
    public abstract void StateCheck(PlayerController player);
    public abstract void StateExit(PlayerController player);
}
