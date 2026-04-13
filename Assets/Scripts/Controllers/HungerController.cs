using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HungerController : MonoBehaviour
{

    public event Action<float> OnHungerChanged;
    public event Action<bool> OnNewStarvationState;
    private ItemController m_itemController;
    private HealthController m_healthController;
    private PlayerInputController m_playerInputController;
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

    public void SetDependencies(GameController gameController)
    {
        m_itemController = gameController.itemController;
        m_healthController = gameController.healthController;
        m_playerInputController = gameController.playerInputController;
    }

    public void Init()
    {
        OnHungerChanged?.Invoke(0); //Update le UI au start
        m_itemController.OnConsumableCollected += (ItemSO item) => ConsumeItem(item.hungerAmount);
        m_playerInputController.OnSprintInputChanged += isSprinting => m_isSprinting = isSprinting;
        m_healthController.OnDeath += () => m_isHungerGainEnabled = false;
        //LA RAISON POURQUOI ON LES UNSUBSCRIBE PAS C'EST PARCE QUE LES CONTROLLERS NE SERONT JAMAIS DÉTRUIT. CA SERAIT INUTILE.

        CurrentHunger = 0;
    }

    public void InternalStart()
    {
        StartCoroutine(GainHunger());
    }

    private void ConsumeItem(float hungerAmount)
    {
        if (hungerAmount <= 0) return;
        if (CurrentHunger >= m_maximumHunger)
        {
            //print("[HUNGER CONTROLLER] -> Starvation ends");
            OnNewStarvationState?.Invoke(false);
        }
        CurrentHunger -= hungerAmount;
        CurrentHunger = Mathf.Clamp(CurrentHunger, 0, m_maximumHunger);
    }

    private IEnumerator GainHunger()
    {
        while (m_isHungerGainEnabled)
        {
            float increment = !m_isSprinting ? m_hungerIncrement : m_hungerIncrement * 2;

            if ((CurrentHunger + increment >= m_maximumHunger) && (CurrentHunger < m_maximumHunger))
            {
                //print("[HUNGER CONTROLLER] -> Starvation begins");
                OnNewStarvationState?.Invoke(true);
            }

            CurrentHunger += increment;
            CurrentHunger = Mathf.Clamp(CurrentHunger, 0f, m_maximumHunger);

            yield return new WaitForSeconds(1f);
        }
    }
}
