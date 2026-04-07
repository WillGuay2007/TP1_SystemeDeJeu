using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public BossState currentState { get; private set; }
    public BossState previousState { get; private set; }
    [SerializeField] public Animator bossAnimator;

    private Dictionary<Type, BossState> m_stateDictionary = new Dictionary<Type, BossState>();

    public void ChangeState(Type newState)
    {
        if (newState != null && newState == (currentState?.GetType()))
        {
            print("Could not change state: Already in state " + newState); //Tu a dis de enlever les print mais je crois que celui la est utile
            return;
        }
        if (currentState != null)
        {
            currentState.OnExit();
            previousState = currentState;
        }

        print("The boss went from state" + currentState?.GetType().Name + " to state " + newState);

        currentState = m_stateDictionary[newState];
        currentState.OnEnter();
        
    }

    public void Init()
    {
        m_stateDictionary.Add(typeof(BossIdleState), new BossIdleState(this));
    }

    private void Update()
    {
        currentState?.OnUpdate();
    }

}
