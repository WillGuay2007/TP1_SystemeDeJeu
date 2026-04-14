using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public const float DETECTION_RANGE = 20f;
    public BossStateMachine m_stateMachine;
    public Animator Animator;
    public NavMeshAgent Agent;
    public GameObject[] PatrolPoints;
    public PlayerController playerTarget;
    public Vector3 startPosition;
    private int m_currentPatrolIndex = 0;

    private void Awake()
    {
        startPosition = transform.position;
        m_stateMachine.Init();
        m_stateMachine.ChangeState(typeof(BossIdleState));
    }

    public bool CheckIfPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, playerTarget.transform.position) <= range;
    }

    public void MoveToNextPatrolPoint()
    {
        if (PatrolPoints.Length == 0) return;
        m_currentPatrolIndex = (m_currentPatrolIndex + 1) % PatrolPoints.Length;
        Agent.SetDestination(PatrolPoints[m_currentPatrolIndex].transform.position);
    }

    public bool HasCompletedPath()     {
        if (Agent.pathPending) return false;
        if (Agent.remainingDistance <= Agent.stoppingDistance)
        {
            if (!Agent.hasPath || Agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }
        return false;
    }
}
