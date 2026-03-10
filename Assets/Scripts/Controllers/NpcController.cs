using System;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public static event Action<bool> OnNpcTriggerEnter;

    [SerializeField] private DialogueController m_dialogueController;

    [SerializeField] private InventoryController m_inventoryController;
    [SerializeField] private ExperienceController m_experienceController;
    [SerializeField] private HealthController m_healthController;

    [SerializeField] private DialogueObject m_redNpcHasItemDialogue;
    [SerializeField] private DialogueObject m_redNpcNoItemDialogue;

    private NPC m_currentNpc;

    private float m_lastHealTime = 0f;

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

        switch (m_currentNpc.npcType)
        {
            case NPC.NpcType.Normal:
                m_dialogueController.ShowDialogue(m_currentNpc.m_dialogueObject);
                break;

            case NPC.NpcType.Red:
                HandleRedNpc();
                break;

            case NPC.NpcType.White:
                HandleWhiteNpc();
                break;

            case NPC.NpcType.Black:
                HandleBlackNpc();
                break;
        }
    }

    private void HandleRedNpc()
    {
        if (m_inventoryController.CheckIfHasItem("Exp_Variation1"))
        {
            m_dialogueController.ShowDialogue(m_redNpcHasItemDialogue);
        }
        else
        {
            m_dialogueController.ShowDialogue(m_redNpcNoItemDialogue);
        }
    }

    private void HandleWhiteNpc() //Techniquement c'est x3 vu que la collection compte aussi mais je pense pas que c'est trop grave.
    {
        foreach (string item in m_inventoryController.GetQuestItems())
        {
            float value = m_inventoryController.GetItemValue(item);
            m_experienceController.AddExperience("", value * 2);
        }

        m_inventoryController.ClearQuestItems();

        print("All quest items exchanged for experience");
    }

    private void HandleBlackNpc()
    {
        if (Time.time - m_lastHealTime >= 20f)
        {
            m_healthController.HealPercentage(0.25f);
            m_lastHealTime = Time.time;

            print("Player healed for 25%");
        }
        else
        {
            print("Heal on cooldown");
        }
    }
}