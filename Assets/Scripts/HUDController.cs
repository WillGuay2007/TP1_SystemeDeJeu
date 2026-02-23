using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    //HUNGER
    [SerializeField] private Slider m_hungerSlider;
    [SerializeField] private TMP_Text m_hungerTextLabel;
    private Transform m_sliderForeground;

    //HEALTH
    [SerializeField] private Slider m_healthSlider;
    [SerializeField] private TMP_Text m_healthTextLabel;

    //EXP
    [SerializeField] private Slider m_experienceSlider;
    [SerializeField] private TMP_Text m_levelText;
    private void Awake()
    {
        //HUNGER
        m_sliderForeground = m_hungerSlider.transform.Find("Fill Area/Fill");
        HungerController.OnHungerDrain += ReDrawHungerUI;

        //HEALTH
        HealthController.OnTakeDamage += RedrawHealthUI;

        //LEVEL AND EXPERIENCE
    }

    private void ReDrawHungerUI(float hungerFraction)
    {
        m_hungerTextLabel.text = "Hunger: " + (hungerFraction * 100).ToString() + "%";
        m_hungerSlider.value = hungerFraction;
        m_sliderForeground.GetComponent<Image>().color = new Color(hungerFraction, 1f - hungerFraction, 0f, 1f);
    }

    private void RedrawHealthUI(float TakenDamage, float healthFraction, float newHP)
    {
        m_healthSlider.value = healthFraction;
        m_healthTextLabel.text = "Health: " + newHP.ToString() + " HP";
    }

    private void RedrawExperienceUI()
    {

    }
}
