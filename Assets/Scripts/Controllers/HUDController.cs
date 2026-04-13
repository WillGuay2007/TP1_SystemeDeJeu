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

    [SerializeField] private Button m_interactButton;

    private HungerController m_hungerController;
    private HealthController m_healthController;
    private ExperienceController m_experienceController;
    private NpcController m_npcController;
    private DialogueController m_dialogueController;

    public void SetDependencies(GameController gameController)
    {
        m_hungerController = gameController.hungerController;
        m_healthController = gameController.healthController;
        m_experienceController = gameController.experienceController;
        m_npcController = gameController.npcController;
        m_dialogueController = gameController.dialogueController;
    }

    public void Init()
    {
        //HUNGER
        m_sliderForeground = m_hungerSlider.transform.Find("Fill Area/Fill");
        m_hungerController.OnHungerChanged += ReDrawHungerUI;

        //HEALTH
        m_healthController.OnHealthChanged += RedrawHealthUI;

        //LEVEL AND EXPERIENCE
        m_experienceController.OnExperienceChanged += RedrawExperienceUI;

        m_npcController.OnNpcTriggerEnter += (entered) => m_interactButton.gameObject.SetActive(entered);
        m_dialogueController.OnDialogueStarted += () => m_interactButton.gameObject.SetActive(false);
        //LA RAISON POURQUOI ON LES UNSUBSCRIBE PAS C'EST PARCE QUE LES CONTROLLERS NE SERONT JAMAIS DÉTRUIT. CA SERAIT INUTILE.
    }

    public void InternalStart()
    {

    }

    private void ReDrawHungerUI(float hungerFraction)
    {
        m_hungerTextLabel.text = "Hunger: " + (hungerFraction * 100).ToString() + "%";
        m_hungerSlider.value = hungerFraction;
        m_sliderForeground.GetComponent<Image>().color = new Color(hungerFraction, 1f - hungerFraction, 0f, 1f);
    }

    private void RedrawHealthUI(float newHP, float healthFraction)
    {
        m_healthSlider.value = healthFraction;
        m_healthTextLabel.text = "Health: " + newHP.ToString() + " HP";
    }

    private void RedrawExperienceUI(float experienceFraction, int level)
    {
        m_experienceSlider.value = experienceFraction;
        m_levelText.text = "Level: " + level.ToString();
    }
}
