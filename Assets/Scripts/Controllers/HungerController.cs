using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HungerController : MonoBehaviour
{

    public static event Action<float> OnHungerChanged;
    public static event Action<bool> OnNewStarvationState;

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
        ItemController.OnSpecialItemCollected += (float dmg, float hunger, float exp) => ConsumeItem(hunger);

        CurrentHunger = 0;
        StartCoroutine(GainHunger());
    }

    private void ConsumeItem(float hungerValue)
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
        while (true) {
            if ((CurrentHunger + m_hungerIncrement >= m_maximumHunger) && (CurrentHunger < m_maximumHunger))
            {
                print("[HUNGER CONTROLLER] -> Starvation begins");
                OnNewStarvationState?.Invoke(true);
            }
            CurrentHunger += m_hungerIncrement;
            CurrentHunger = Mathf.Clamp(CurrentHunger, 0f, m_maximumHunger);
            yield return new WaitForSeconds(1f);
        }
    }
}
