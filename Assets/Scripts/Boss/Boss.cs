using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private BossStateMachine m_stateMachine;

    private void Awake()
    {
        m_stateMachine.Init();
        m_stateMachine.ChangeState(typeof(BossIdleState));
    }
}
