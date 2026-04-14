using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsWindow : Window
{
    [SerializeField] private PlayerInputController m_playerInputController;
    [SerializeField] private Slider m_sfxSlider;

    private void Start()
    {
        HUDControllerV2.Instance.CloseWindow(this);
        m_sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        m_sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume); //Je veut affecter la musique aussi.
        m_playerInputController.OnOpenAudioSettingsInput += () =>
        {
            if (gameObject.activeSelf)
                HUDControllerV2.Instance.CloseWindow(this);
            else
                HUDControllerV2.Instance.OpenWindow(this);
        };
    }

}
