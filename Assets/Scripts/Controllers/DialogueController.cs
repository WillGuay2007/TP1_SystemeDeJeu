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
    [SerializeField] private Button m_skipButton;
    [SerializeField] private float m_typewriterSpeed;

    private DialogueObject m_currentDialogueObject;

    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;


    private Coroutine m_activeDialogue;

    private void Awake()
    {
        m_acceptButton.onClick.AddListener(OnAccepted);
        m_declineButton.onClick.AddListener(OnDenied);
        m_skipButton.onClick.AddListener(OnSkip);
    }

    private void OnDestroy()
    {
        m_acceptButton.onClick.RemoveAllListeners();
        m_declineButton.onClick.RemoveAllListeners();
    }

    public void ShowDialogue(DialogueObject data)
    {
        if (m_activeDialogue != null) return;
        m_activeDialogue = StartCoroutine(StartDialogue(data, true));
    }

    private IEnumerator StartDialogue(DialogueObject data, bool isFirstCall)
    {
        if (isFirstCall) {
            print("[DIALOGUE CONTROLLER] -> Dialogue started");
            OnDialogueStarted?.Invoke();
        }
        m_dialogueBox.SetActive(true);

        SetButtonTransparency(m_acceptButton, true, "");
        SetButtonTransparency(m_declineButton, true, "");

        yield return StartCoroutine(TypewriterEffect(data.DialogueNode));
        if (data.refused == null || data.accepted == null)
        {
            yield return new WaitForSeconds(1);
            CloseDialogue();
            yield break;
        }
        else
        {
            m_currentDialogueObject = data;
            SetButtonTransparency(m_acceptButton, false, "Yes");
            SetButtonTransparency(m_declineButton, false, "No");
        }
    }

    private void SetButtonTransparency(Button button, bool isTransparent, string newText)
    {
        Color col = button.GetComponent<Image>().color;
        if (isTransparent)
        {
            col.a = 0;
            button.enabled = false;
        }
        else { button.enabled = true; col.a = 1; }

        button.GetComponentInChildren<TMP_Text>().text = newText;
        button.GetComponent<Image>().color = col;
    }

    private void OnDenied()
    {
        StartCoroutine(StartDialogue(m_currentDialogueObject.refused, false));
    }

    private void OnAccepted()
    {
        StartCoroutine(StartDialogue(m_currentDialogueObject.accepted, false));
    }

    private void OnSkip()
    {
        CloseDialogue();
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
        print("[DIALOGUE CONTROLLER] -> Dialogue ended");
        StopAllCoroutines();
        OnDialogueEnded?.Invoke();
        m_currentDialogueObject = null;
        m_dialogueBox.SetActive(false);
        m_activeDialogue = null;
    }
}
