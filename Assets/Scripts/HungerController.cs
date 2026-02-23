using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HungerController : MonoBehaviour
{

    public static event Action<float> OnHungerDrain;
    public static event Action<bool> OnNewStarvationState;

    [SerializeField] private int m_maximumHunger;
    private float m_currentHunger;
    private float m_hungerIncrement { get { return m_maximumHunger * 0.01f; } }

    private void Awake()
    {
        m_currentHunger = 0;
        StartCoroutine(GainHunger());
    }

    private IEnumerator GainHunger()
    {
        while (true) {
            if ((m_currentHunger + m_hungerIncrement >= m_maximumHunger) && (m_currentHunger < m_maximumHunger))
            {
                print("[HUNGER CONTROLLER] -> Starvation begins");
                OnNewStarvationState?.Invoke(true);
            }
            m_currentHunger += m_hungerIncrement;
            m_currentHunger = Mathf.Clamp(m_currentHunger, 0f, m_maximumHunger);
            OnHungerDrain?.Invoke(Mathf.Clamp((m_currentHunger / m_maximumHunger), 0f, 1f));
            yield return new WaitForSeconds(1f);
        }
    }
}
