using UnityEngine;

public class BossIdleState : BossState
{
    public BossIdleState(BossStateMachine _stateMachine) : base(_stateMachine)
    {
    }

    public override void OnEnter()
    {
        stateMachine.bossAnimator.SetBool("Idling", true);
    }

    public override void OnExit()
    {
        stateMachine.bossAnimator.SetBool("Idling", false);
    }

    public override void OnUpdate()
    {

    }
}
