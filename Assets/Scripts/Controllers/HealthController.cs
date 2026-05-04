using System;
using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float m_maxHealth;
    public event Action<float, float, float> OnTakeDamage;
    public event Action OnDeath;
    public event Action<float, float> OnHealthChanged;
    private float m_currentHealth;
    private HungerController m_hungerController;
    private ItemController m_itemController;
    private ExperienceController m_experienceController;
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

    public void SetDependencies(GameController gameController)
    {
        m_hungerController = gameController.hungerController;
        m_itemController = gameController.itemController;
        m_experienceController = gameController.experienceController;
    }

    public void Init()
    {
        CurrentHealth = m_maxHealth;
        m_hungerController.OnNewStarvationState += SetStarvation;
        m_itemController.OnPickupableCollected += (ItemSO item) => { if (item.damageAmount > 0) Damage(item.damageAmount); };
        m_experienceController.OnLevelUp += IncrementMaxHP;
        //LA RAISON POURQUOI ON LES UNSUBSCRIBE PAS C'EST PARCE QUE LES CONTROLLERS NE SERONT JAMAIS DÉTRUIT. CA SERAIT INUTILE.
    }

    public void InternalStart()
    {

    }

    public void HealPercentage(float percent)
    {
        CurrentHealth += m_maxHealth * percent;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, m_maxHealth);
    }

    public void Damage(float damage)
    {
        if (CurrentHealth <= 0) return;
        if (CurrentHealth > 0 && CurrentHealth - damage <= 0f)
        {
            //print("[HEALTH CONTROLLER] -> Player died. Inputs disabled. Game ended.");
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
