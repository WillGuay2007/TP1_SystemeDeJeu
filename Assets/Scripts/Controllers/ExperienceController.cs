using System;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    [SerializeField] private float m_amountOfExperienceForLvlUp = 100f;

    public static event Action<float, int> OnExperienceChanged;
    public static event Action<int> OnLevelUp;


    private float m_currentExperience = 0f;
    private float m_fraction => (m_currentExperience % m_amountOfExperienceForLvlUp) / m_amountOfExperienceForLvlUp;

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
        ItemController.OnSpecialItemCollected += (string name, float dmg, float hunger, float exp) => AddExperience(name, exp);
        //LA RAISON POURQUOI ON LES UNSUBSCRIBE PAS C'EST PARCE QUE LES CONTROLLERS NE SERONT JAMAIS DÉTRUIT. CA SERAIT INUTILE.
    }

    public void AddExperience(string name, float experience)
    {
        if (((CurrentExperience + experience) / m_amountOfExperienceForLvlUp) >= CurrentLevel + 1)
        {
            OnLevelUp?.Invoke(CurrentLevel);
        }
        CurrentExperience += experience;
    }
}