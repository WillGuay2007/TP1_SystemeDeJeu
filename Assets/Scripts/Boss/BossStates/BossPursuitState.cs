using UnityEngine;

public class BossPursuitState : BossState
{
    private const float ATTACK_RANGE = 3f;
    private const float RETREAT_RANGE = 22f;

    public BossPursuitState(BossStateMachine _stateMachine, string _animBool) : base(_stateMachine, _animBool)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        boss.Agent.SetDestination(boss.playerTarget.transform.position);
    }

    public override void OnUpdate()
    {
        boss.Agent.SetDestination(boss.playerTarget.transform.position);

        if (boss.CheckIfPlayerInRange(ATTACK_RANGE))
        {
            stateMachine.ChangeState(typeof(BossAttackState));
            return;
        }
        if (!boss.CheckIfPlayerInRange(RETREAT_RANGE))
        {
            stateMachine.ChangeState(typeof(BossRetreatState));
        }
    }
}
