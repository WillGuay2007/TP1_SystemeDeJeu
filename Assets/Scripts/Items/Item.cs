using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected ItemController m_itemController;
    private bool m_isCollected;

    private void OnTriggerEnter(Collider collider)
    {
        PlayerController playerController = collider.GetComponent<PlayerController>();
        if (playerController != null && m_isCollected == false)
        {
            Collect();
            Destroy(gameObject);
        }
    }

    private void Collect()
    {
        m_isCollected = true;
        m_itemController.CollectItem(this);
    }
}
