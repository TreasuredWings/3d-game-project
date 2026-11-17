using UnityEngine;

public abstract class IBaseState
{
    public abstract void Enter();
    public abstract void StateUpdate();
    public abstract void Exit();
}
