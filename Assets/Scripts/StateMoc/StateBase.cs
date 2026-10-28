using UnityEngine;

public interface IState
{
    void Enter();
    void Exit();
}

public enum StageState
{
    None = 0,
    Idle,
    Rolling,
    Clear,
    Failed
}

public abstract class StateBase : IState
{
    protected StageState _state;
    public abstract void Enter();
    public abstract void Exit();
}
