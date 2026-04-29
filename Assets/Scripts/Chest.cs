using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] ItemSO m_loot;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DropLoot();
        }
    }

    private void DropLoot()
    {
        ItemFactory.Instance.CreateItem(m_loot, transform.position);
        Destroy(gameObject);
    }
}
