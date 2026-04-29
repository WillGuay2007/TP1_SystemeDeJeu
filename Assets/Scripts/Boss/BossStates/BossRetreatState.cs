using UnityEngine;

public class BossRetreatState : BossState
{
    private const float TAKE_DAMAGE_TIMER = 3f; // Je veut qu'il prenne du dégat quand il retreat. C'est comme si il volait tes hp et que si il est trop loin il peut plus les voler
    private const int DAMAGE_AMOUNT = 1;
    private float m_timeSinceLastTakeDamage = 0f;

    public BossRetreatState(BossStateMachine _stateMachine, string _animBool) : base(_stateMachine, _animBool)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        m_timeSinceLastTakeDamage = 0f;
        boss.Agent.SetDestination(boss.startPosition);
    }

    public override void OnUpdate()
    {
        if (boss.HasCompletedPath())
        {
            stateMachine.ChangeState(typeof(BossIdleState));
        }
        m_timeSinceLastTakeDamage += Time.deltaTime;
        if (m_timeSinceLastTakeDamage > TAKE_DAMAGE_TIMER)
        {
            m_timeSinceLastTakeDamage = 0f;
            boss.TakeDamage(DAMAGE_AMOUNT);
        }
    }
}
