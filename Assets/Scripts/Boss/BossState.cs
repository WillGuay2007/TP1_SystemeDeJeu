using UnityEngine;

public abstract class BossState
{
    protected readonly BossStateMachine stateMachine;

    public BossState(BossStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public abstract void OnEnter();

    public abstract void OnExit();

    public abstract void OnUpdate();
}
