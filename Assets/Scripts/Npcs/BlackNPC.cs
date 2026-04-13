using UnityEngine;

public class BlackNPC : NPC
{
    [SerializeField] HealthController m_healthController;
    private float m_lastHealTime = 0f;

    public override void Interact()
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
