using UnityEngine;

public class InventoryController : MonoBehaviour
{

    [SerializeField] private PlayerInventory m_inventory;

    private void Awake()
    {
        //RESET A CHAQUE DEBUT DE JEU
        m_inventory.NumberOfQuestItems = 0;
        m_inventory.NumberOfSpecialItems = 0;
        m_inventory.NumberOfConsumableItem = 0;

        ItemController.OnConsumableCollected += (float _) => AddConsumableItem();
        ItemController.OnQuestItemCollected += (float _) => AddQuestItem();
        ItemController.OnSpecialItemCollected += (float _, float _, float _) => AddSpecialItem();
        HealthController.OnDeath += PrintInventory;
    }

    //TODO: make cleaner later
    private void AddConsumableItem()
    {
        m_inventory.TotalItems++;
        m_inventory.NumberOfConsumableItem++;
    }

    private void AddQuestItem()
    {
        m_inventory.TotalItems++;
        m_inventory.NumberOfQuestItems++;
    }

    private void AddSpecialItem()
    {
        m_inventory.TotalItems++;
        m_inventory.NumberOfSpecialItems++;
    }

    private void PrintInventory()
    {
        print("Number of consumable items collected: " + m_inventory.NumberOfConsumableItem);
        print("Number of quest items collected: " + m_inventory.NumberOfQuestItems);
        print("Number of special items collected: " + m_inventory.NumberOfSpecialItems);
        print("Number of total items: " + m_inventory.TotalItems);
    }
}
