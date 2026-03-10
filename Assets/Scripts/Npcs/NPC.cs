using UnityEngine;

public class NPC : MonoBehaviour
{
    public enum NpcType
    {
        Normal,
        Red,
        White,
        Black
    }

    [SerializeField] public NpcType npcType;
    [SerializeField] public DialogueObject m_dialogueObject;

    [SerializeField] private NpcController m_npcController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            m_npcController.EnterNpc(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            m_npcController.ExitNpc(this);
        }
    }
}