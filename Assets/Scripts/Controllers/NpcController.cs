using System;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public event Action<bool> OnNpcTriggerEnter;

    private NPC m_currentNpc;

    public void SetDependencies(GameController gameController)
    {
        
    }

    public void Init()
    {

    }

    public void InternalStart()
    {

    }

    public void EnterNpc(NPC npc)
    {
        m_currentNpc = npc;
        OnNpcTriggerEnter?.Invoke(true);
    }

    public void ExitNpc(NPC npc)
    {
        if (m_currentNpc == npc)
        {
            m_currentNpc = null;
            OnNpcTriggerEnter?.Invoke(false);
        }
    }

    public void Interact()
    {
        if (m_currentNpc == null) return;

        m_currentNpc.Interact();
    }
}