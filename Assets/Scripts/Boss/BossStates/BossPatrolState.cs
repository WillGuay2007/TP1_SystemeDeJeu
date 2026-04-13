using UnityEngine;

public class BossPatrolState : BossState
{
    public BossPatrolState(BossStateMachine _stateMachine, string _animBool) : base(_stateMachine, _animBool)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        boss.MoveToNextPatrolPoint();
    }

    public override void OnExit()
    {
        base.OnExit();

    }

    public override void OnUpdate()
    {
        if (boss.HasCompletedPath())
        {
            stateMachine.ChangeState(typeof(BossIdleState));
        }
    }
}
