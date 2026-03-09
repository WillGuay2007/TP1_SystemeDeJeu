using System;
using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float m_maxHealth;
    public static event Action<float, float, float> OnTakeDamage;
    public static event Action OnDeath;
    public static event Action<float, float> OnHealthChanged;
    private float m_currentHealth;
    public float CurrentHealth
    {
        get => m_currentHealth;
        private set
        {
            m_currentHealth = value;
            OnHealthChanged?.Invoke(m_currentHealth, m_currentHealth / m_maxHealth);
        }
    }
    private Coroutine m_hungerHealthDrainCoroutine = null;
    private float m_starveDamage = 10f;
    private float m_levelUpHealthGain = 10f;

    private void Awake()
    {
        CurrentHealth = m_maxHealth;
        HungerController.OnNewStarvationState += SetStarvation;
        ItemController.OnSpecialItemCollected += (float dmg, float hunger, float exp) => Damage(dmg);
        ExperienceController.OnLevelUp += IncrementMaxHP;
    }

    private void Damage(float damage)
    {
        if (CurrentHealth > 0 && CurrentHealth - damage <= 0f)
        {
            print("[HEALTH CONTROLLER] -> Player died.");
            OnDeath?.Invoke();
        }
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
        OnTakeDamage?.Invoke(damage, Mathf.Clamp((CurrentHealth / m_maxHealth), 0f, 1f), m_currentHealth);
    }

    private void SetStarvation(bool isStarving)
    {
        if (isStarving)
        {
            m_hungerHealthDrainCoroutine = StartCoroutine(HungerHealthDrain());
        }
        else
        {
            StopCoroutine(m_hungerHealthDrainCoroutine);
            m_hungerHealthDrainCoroutine = null;
        }
    }

    private void IncrementMaxHP(int level)
    {
        m_maxHealth += m_levelUpHealthGain;
        CurrentHealth = CurrentHealth; //Pour invoke le event dans le setter.
    }

    private IEnumerator HungerHealthDrain()
    {
        while (true)
        {
            Damage(m_starveDamage);
            yield return new WaitForSeconds(5f);
        }
    }
}
