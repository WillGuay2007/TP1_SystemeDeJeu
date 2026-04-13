using UnityEngine;

public class RedNPC : NPC
{
    [SerializeField] private InventoryController m_inventoryController;
    [SerializeField] private DialogueController m_dialogueController;
    [SerializeField] private DialogueObject m_hasItemDialogue;
    [SerializeField] private DialogueObject m_noItemDialogue;
    [SerializeField] private ItemSO m_itemCheck;
    public override void Interact()
    {
        if (m_inventoryController.CheckIfHasItem(m_itemCheck))
        {
            m_dialogueController.ShowDialogue(m_hasItemDialogue);
        }
        else
        {
            m_dialogueController.ShowDialogue(m_noItemDialogue);
        }
    }
}
