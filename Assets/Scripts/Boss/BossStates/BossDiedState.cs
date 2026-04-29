using UnityEngine;

public class BossDiedState : BossState
{
    public BossDiedState(BossStateMachine _stateMachine, string _animBool) : base(_stateMachine, _animBool)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        boss.DieAndDropLoot();
    }
}
