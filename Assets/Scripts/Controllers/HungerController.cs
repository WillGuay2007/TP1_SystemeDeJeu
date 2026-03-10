using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HungerController : MonoBehaviour
{

    public static event Action<float> OnHungerChanged;
    public static event Action<bool> OnNewStarvationState;
    private bool m_isSprinting = false;
    private bool m_isHungerGainEnabled = true;

    [SerializeField] private int m_maximumHunger;
    private float m_currentHunger;
    public float CurrentHunger
    {
        get => m_currentHunger;
        private set
        {
            m_currentHunger = value;
            OnHungerChanged?.Invoke(value / m_maximumHunger);
        }
    }
    private float m_hungerIncrement { get { return m_maximumHunger * 0.01f; } }

    private void Awake()
    {
        OnHungerChanged?.Invoke(0); //Update le UI au start
        ItemController.OnConsumableCollected += ConsumeItem;
        ItemController.OnSpecialItemCollected += (string name, float dmg, float hunger, float exp) => ConsumeItem(name, hunger);
        PlayerInputController.OnSprintInputChanged += isSprinting => m_isSprinting = isSprinting;
        HealthController.OnDeath += () => m_isHungerGainEnabled = false;
        //LA RAISON POURQUOI ON LES UNSUBSCRIBE PAS C'EST PARCE QUE LES CONTROLLERS NE SERONT JAMAIS DÉTRUIT. CA SERAIT INUTILE.

        CurrentHunger = 0;
        StartCoroutine(GainHunger());
    }

    private void ConsumeItem(string name, float hungerValue)
    {
        if (CurrentHunger >= m_maximumHunger)
        {
            print("[HUNGER CONTROLLER] -> Starvation ends");
            OnNewStarvationState?.Invoke(false);
        }
        CurrentHunger -= hungerValue;
        CurrentHunger = Mathf.Clamp(CurrentHunger, 0, m_maximumHunger);
    }

    private IEnumerator GainHunger()
    {
        while (m_isHungerGainEnabled)
        {
            float increment = !m_isSprinting ? m_hungerIncrement : m_hungerIncrement * 2;

            if ((CurrentHunger + increment >= m_maximumHunger) && (CurrentHunger < m_maximumHunger))
            {
                print("[HUNGER CONTROLLER] -> Starvation begins");
                OnNewStarvationState?.Invoke(true);
            }

            CurrentHunger += increment;
            CurrentHunger = Mathf.Clamp(CurrentHunger, 0f, m_maximumHunger);

            yield return new WaitForSeconds(1f);
        }
    }
}
