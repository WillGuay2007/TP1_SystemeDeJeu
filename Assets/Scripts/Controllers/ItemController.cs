using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private InventoryController m_inventoryController;
    public static event Action<string, float, float, float> OnSpecialItemCollected;
    public static event Action<string, float> OnConsumableCollected;
    public static event Action<string, float> OnQuestItemCollected;
    private List<Item> m_items;

    public void CollectItem(Item item)
    {
        if (item is ConsumableItem consumableItem)
        {
            OnConsumableCollected?.Invoke(consumableItem.itemName, consumableItem.hungerAmount);
        }
        else if (item is QuestItem questItem) {
            OnQuestItemCollected?.Invoke(questItem.itemName, questItem.experienceAmount);
        }
        else if (item is SpecialItem specialItem)
        {
            OnSpecialItemCollected?.Invoke(specialItem.itemName, specialItem.damageAmount, specialItem.hungerAmount, specialItem.expAmount);
        }
    }

    public void AddItem(Item item)
    {
        m_items.Add(item);
    }
}
