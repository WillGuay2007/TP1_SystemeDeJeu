using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{

    public BossState currentState { get; private set; }
    public BossState previousState { get; private set; }
    public Boss boss;

    private Dictionary<Type, BossState> m_stateDictionary = new Dictionary<Type, BossState>();
    private List<string> m_stateHistory = new List<string>();


    public void ChangeState(Type newState)
    {
        if (currentState != null)
        {
            m_stateHistory.Add("Went from" + currentState.GetType().Name + " to -> " + newState.Name);
            currentState.OnExit();
            previousState = currentState;
        }

        currentState = m_stateDictionary[newState];
        currentState.OnEnter();
        
    }

    public void Init()
    {
        m_stateDictionary.Add(typeof(BossIdleState), new BossIdleState(this, "Idling"));
        m_stateDictionary.Add(typeof(BossPatrolState), new BossPatrolState(this, "Walking"));
        m_stateDictionary.Add(typeof(BossPursuitState), new BossPursuitState(this, "Walking"));
        m_stateDictionary.Add(typeof(BossAttackState), new BossAttackState(this, "Idling"));  //J'ai pas d'animation pour attack
        m_stateDictionary.Add(typeof(BossRetreatState), new BossRetreatState(this, "Walking"));
    }

    [ContextMenu("Print State History")]
    private void PrintStateHistory()
    {
        foreach (string history in m_stateHistory)
        {
            print(history);
        }
    }

    private void Update()
    {
        currentState?.OnUpdate();
    }

}
