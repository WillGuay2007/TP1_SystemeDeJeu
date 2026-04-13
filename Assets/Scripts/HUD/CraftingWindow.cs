using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingWindow : Window
{
    [SerializeField] private TextMeshProUGUI m_craftProgressText;
    [SerializeField] Slider m_craftProgressSlider;

    public void SetCraftSliderValue(float value)
    {
        m_craftProgressSlider.value = value;
    }

    public void SetCraftProgressText(string text)
    {
        m_craftProgressText.text = text;
    }
}
