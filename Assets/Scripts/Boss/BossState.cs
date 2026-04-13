using UnityEngine;

public class BossState
{
    protected BossStateMachine stateMachine;
    protected Boss boss;
    private string m_animBool;

    public BossState(BossStateMachine _stateMachine, string _animBool)
    {
        boss = _stateMachine.boss;
        stateMachine = _stateMachine;
        m_animBool = _animBool;
    }

    public virtual void OnEnter()
    {
        boss.Animator.SetBool(m_animBool, true);
    }

    public virtual void OnExit()
    {
        boss.Animator.SetBool(m_animBool, false);
    }

    public virtual void OnUpdate() { }
}
