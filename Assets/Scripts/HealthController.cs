using System;
using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float m_maxHealth;
    public static event Action<float, float, float> OnTakeDamage;
    public static event Action OnDeath;
    private float m_currentHealth;
    private Coroutine m_hungerHealthDrainCoroutine = null;
    private float m_starveDamage = 10f;

    private void Awake()
    {
        m_currentHealth = m_maxHealth;
        HungerController.OnNewStarvationState += SetStarvation;
    }

    private void Damage(float damage)
    {
        m_currentHealth -= damage;
        m_currentHealth = Mathf.Max(m_currentHealth, 0);
        OnTakeDamage?.Invoke(damage, Mathf.Clamp((m_currentHealth / m_maxHealth), 0f, 1f), m_currentHealth);
        if (m_currentHealth <= 0f)
        {
            OnDeath?.Invoke();
        }
    }

    private void SetStarvation(bool isStarving)
    {
        if (isStarving)
        {
            m_hungerHealthDrainCoroutine = StartCoroutine(HungerHealthDrain());
        }
        else
        {
            StopCoroutine(HungerHealthDrain());
            m_hungerHealthDrainCoroutine = null;
        }
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
