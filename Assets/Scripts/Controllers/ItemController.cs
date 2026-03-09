using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private float m_spawnRadius;
    [SerializeField] private int m_itemSpawnCount;
    [SerializeField] private InventoryController m_inventoryController;
    public static event Action<float, float, float> OnSpecialItemCollected;
    public static int itemCount;
    public static event Action<float> OnConsumableCollected;
    public static event Action<float> OnQuestItemCollected;
    private List<GameObject> m_items;

    private void Awake()
    {
        SpawnItems(m_spawnRadius, m_itemSpawnCount);
    }

    private void SpawnItems(float radius, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            itemCount++;
        }
    }

    public void CollectItem(Item item)
    {
        if (item is ConsumableItem consumableItem)
        {
            OnConsumableCollected?.Invoke(consumableItem.hungerAmount);
        }
        else if (item is QuestItem questItem) {
            OnQuestItemCollected?.Invoke(questItem.experienceAmount);
        }
        else if (item is SpecialItem specialItem)
        {
            OnSpecialItemCollected?.Invoke(specialItem.damageAmount, specialItem.hungerAmount, specialItem.expAmount);
        }
    }
}
