using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TMP_Text m_textLabel;
    [SerializeField] private GameObject m_dialogueBox;
    [SerializeField] private Button m_acceptButton;
    [SerializeField] private Button m_declineButton;
    [SerializeField] private float m_typewriterSpeed;

    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;


    private Coroutine m_activeDialogue;

    [SerializeField] private DialogueObject m_dialogueObject;
    private void Awake()
    {
        m_acceptButton.onClick.AddListener(OnAccepted);
        m_declineButton.onClick.AddListener(OnDenied);
        ShowDialogue(m_dialogueObject);
    }

    private void OnDestroy()
    {
        m_acceptButton.onClick.RemoveAllListeners();
        m_declineButton.onClick.RemoveAllListeners();
    }

    public void ShowDialogue(DialogueObject data)
    {
        if (m_activeDialogue != null) return;
        m_activeDialogue = StartCoroutine(StartDialogue(data));
    }

    private IEnumerator StartDialogue(DialogueObject data)
    {
        OnDialogueStarted?.Invoke();
        m_dialogueBox.SetActive(true);

        SetButtonTransparency(m_acceptButton, true);
        SetButtonTransparency(m_declineButton, true);

        yield return StartCoroutine(TypewriterEffect(data.DialogueNode));
        if (data.refused == null || data.accepted == null)
        {
            yield return new WaitForSeconds(1);
            CloseDialogue();
            yield break;
        }
        else
        {
            SetButtonTransparency(m_acceptButton, false);
            SetButtonTransparency(m_declineButton, false);
        }
    }

    private void SetButtonTransparency(Button button, bool isTransparent)
    {
        Color col = button.GetComponent<Image>().color;
        if (isTransparent)
        {
            col.a = 0;
            button.enabled = false;
        }
        button.GetComponent<Image>().color = col;
    }

    private void OnDenied()
    {

    }

    private void OnAccepted()
    {

    }

    private IEnumerator TypewriterEffect(string textToWrite)
    {
        m_textLabel.text = string.Empty;
        int currentIndex = 0;
        float t = 0f;
        while (currentIndex < textToWrite.Length && !Input.GetKeyDown(KeyCode.E))
        {
            t += Time.deltaTime * m_typewriterSpeed;
            currentIndex = Mathf.FloorToInt(t);
            currentIndex = Mathf.Clamp(currentIndex, 0, textToWrite.Length);
            m_textLabel.text = textToWrite.Substring(0, currentIndex);
            yield return null;
        }
        m_textLabel.text = textToWrite;
    }

    private void CloseDialogue()
    {
        OnDialogueEnded?.Invoke();
        m_dialogueBox.SetActive(false);
        m_activeDialogue = null;
    }
}
