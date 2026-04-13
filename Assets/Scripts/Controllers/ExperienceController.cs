using System;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    [SerializeField] private float m_amountOfExperienceForLvlUp = 100f;

    public event Action<float, int> OnExperienceChanged;
    public event Action<int> OnLevelUp;

    private ItemController m_itemController;

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
    public void SetDependencies(GameController gameController)
    {
        m_itemController = gameController.itemController;
    }

    public void Init()
    {
        m_itemController.OnConsumableCollected += (ItemSO item) => AddExperience(item.expAmount); //Si c'est un pickupable, le npc va donner le exp.
        //LA RAISON POURQUOI ON LES UNSUBSCRIBE PAS C'EST PARCE QUE LES CONTROLLERS NE SERONT JAMAIS DÉTRUIT. CA SERAIT INUTILE.
    }

    public void InternalStart()
    {
        OnExperienceChanged?.Invoke(m_fraction, CurrentLevel);
    }

    public int CurrentLevel => Mathf.FloorToInt(m_currentExperience / m_amountOfExperienceForLvlUp);

    public void AddExperience(float experienceAmount)
    {
        if (((CurrentExperience + experienceAmount) / m_amountOfExperienceForLvlUp) >= CurrentLevel + 1)
        {
            OnLevelUp?.Invoke(CurrentLevel);
        }
        CurrentExperience += experienceAmount;
    }
}