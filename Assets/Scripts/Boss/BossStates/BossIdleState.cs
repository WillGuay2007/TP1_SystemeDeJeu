using UnityEngine;

public class BossIdleState : BossState
{
    private const float MINIMUM_IDLE_DELAY = 2f;
    private const float MAXIMUM_IDLE_DELAY = 5f;
    private float idleDelay;
    private float idleTimer;

    public BossIdleState(BossStateMachine _stateMachine, string _animBool) : base(_stateMachine, _animBool)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        idleDelay = Random.Range(MINIMUM_IDLE_DELAY, MAXIMUM_IDLE_DELAY);
    }

    public override void OnExit()
    {
        base.OnExit();
        idleTimer = 0f;
    }

    public override void OnUpdate()
    {
        idleTimer += Time.deltaTime;

        if (boss.CheckIfPlayerInRange(Boss.DETECTION_RANGE))
        {
            stateMachine.ChangeState(typeof(BossPursuitState));
            return;
        }

        if (idleTimer >= idleDelay)
        {
            stateMachine.ChangeState(typeof(BossPatrolState));
        }
    }
}
