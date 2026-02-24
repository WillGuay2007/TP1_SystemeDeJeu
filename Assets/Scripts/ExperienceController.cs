using System;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    [SerializeField] private float m_amountOfExperienceForLvlUp = 100f;

    public static event Action<float, int> OnExperienceChanged;

    private float m_currentExperience = 0f;
    private float m_fraction => m_currentExperience / m_amountOfExperienceForLvlUp;

    public float CurrentExperience
    {
        get => m_currentExperience;
        private set
        {
            m_currentExperience = Mathf.Max(0, value);

            OnExperienceChanged?.Invoke(m_fraction, CurrentLevel);
        }
    }

    public int CurrentLevel => Mathf.FloorToInt(m_currentExperience / m_amountOfExperienceForLvlUp);

    private void Awake()
    {
        OnExperienceChanged?.Invoke(m_fraction, CurrentLevel);
        ItemController.OnQuestItemCollected += AddExperience;
    }

    private void AddExperience(float experience)
    {
        CurrentExperience += experience;
    }
}