using UnityEngine;

public class BossAttackState : BossState
{
    private const float ATTACK_DURATION = 2f;
    private const float ATTACK_RANGE = 3f;
    private float m_attackTimer;

    public BossAttackState(BossStateMachine _stateMachine, string _animBool) : base(_stateMachine, _animBool)
    {
    }


    public override void OnEnter()
    {
        base.OnEnter();
        m_attackTimer = 0f;
        boss.Agent.SetDestination(boss.transform.position);
        AudioManager.Instance.PlayAudio(AudioManager.Sounds.BossAttack);
    }

    public override void OnUpdate()
    {
        m_attackTimer += Time.deltaTime;
        if (m_attackTimer >= ATTACK_DURATION)
        {
            if (boss.CheckIfPlayerInRange(ATTACK_RANGE))
                stateMachine.ChangeState(typeof(BossAttackState));
            else
                stateMachine.ChangeState(typeof(BossPursuitState));
        }
    }
}
