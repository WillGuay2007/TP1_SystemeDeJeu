using UnityEngine;

public class BossRetreatState : BossState
{
    public BossRetreatState(BossStateMachine _stateMachine, string _animBool) : base(_stateMachine, _animBool)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        boss.Agent.SetDestination(boss.startPosition);
    }

    public override void OnUpdate()
    {
        if (boss.HasCompletedPath())
        {
            stateMachine.ChangeState(typeof(BossIdleState));
        }
    }
}
