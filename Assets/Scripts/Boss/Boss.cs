using UnityEngine;
using UnityEngine.AI;

//Pour tuer le boss, tu doit le mettre en retreat state une ou plusieurs fois. Aller dans la retreat state pour mieux comprendre

public class Boss : MonoBehaviour
{

    public const float DETECTION_RANGE = 20f;
    public const int MAX_HEALTH = 5;

    public BossStateMachine m_stateMachine;
    public Animator Animator;
    public NavMeshAgent Agent;
    public GameObject[] PatrolPoints;
    public PlayerController playerTarget;
    public Vector3 startPosition;
    [SerializeField] private ItemSO m_dropLoot;
    private int m_currentPatrolIndex = 0;
    private int m_currentHealth;

    private void Awake()
    {
        startPosition = transform.position;
        m_currentHealth = MAX_HEALTH;
        m_stateMachine.Init();
        m_stateMachine.ChangeState(typeof(BossIdleState));
    }

    public bool CheckIfPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, playerTarget.transform.position) <= range;
    }

    public void DieAndDropLoot()
    {
        Vector3 groundPoint = FindGroundPosition();
        if (groundPoint != Vector3.zero)
        {
            ItemFactory.Instance.CreateItem(m_dropLoot, groundPoint);
        } else
        {
            ItemFactory.Instance.CreateItem(m_dropLoot, transform.position);
        }

        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;
        if (m_currentHealth <= 0)
        {
            DieAndDropLoot();
        }
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

    private Vector3 FindGroundPosition()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
