using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TMP_Text m_textLabel;
    [SerializeField] private GameObject m_dialogueBox;
    [SerializeField] private float m_typewriterSpeed;
    private Coroutine m_activeDialogue;

    public void ShowDialogue(DialogueObject data)
    {
        if (m_activeDialogue != null) return;
        m_activeDialogue = StartCoroutine(StartDialogue(data));
    }

    private IEnumerator StartDialogue(DialogueObject data)
    {
        m_dialogueBox.SetActive(true);
        foreach (string dialogueLine in data.Dialogue)
        {
            yield return null;
            yield return StartCoroutine(TypewriterEffect(dialogueLine));
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }
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
        m_dialogueBox.SetActive(false);
        m_activeDialogue = null;
    }
}
