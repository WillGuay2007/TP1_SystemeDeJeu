using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private DialogueObject m_accepted;
    [SerializeField] private DialogueObject m_refused;
    [SerializeField][TextArea] private string m_nodeContent;

    public string DialogueNode => m_nodeContent;
    public DialogueObject accepted => m_accepted;
    public DialogueObject refused => m_refused;
}
