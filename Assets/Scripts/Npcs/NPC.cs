using UnityEngine;

public abstract class NPC : MonoBehaviour
{

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
        if (other.CompareTag("Player"))
        {
            m_npcController.ExitNpc(this);
        }
    }

    public abstract void Interact();
}