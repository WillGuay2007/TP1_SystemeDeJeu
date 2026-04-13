using UnityEngine;

public class NormalNPC : NPC
{

    [SerializeField] private DialogueObject m_dialogueObject;
    [SerializeField] protected DialogueController m_dialogueController;

    public override void Interact()
    {
        m_dialogueController.ShowDialogue(m_dialogueObject);
    }
}
