using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_instance;
    private AudioSource m_audioSource;
    [SerializeField] private AudioClip[] m_audios;

    public static AudioManager Instance
    {
        get
        {
            if (m_instance == null) Debug.Log("AudioManager is null");
            return m_instance;
        }
    }

    public void PlaySound(string audioName)
    {
        //Pour respecter le OCP
        foreach(AudioClip clip in m_audios)
        {
            if (clip.name == audioName)
            {
                m_audioSource.PlayOneShot(clip);
                return;
            }
        }
    }

    public void SetSFXVolume(float volume)
    {
        m_audioSource.volume = volume;
    }

    private void Awake()
    {
        m_instance = this;
        m_audioSource = GetComponent<AudioSource>();
    }

}
