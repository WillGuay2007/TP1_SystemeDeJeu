using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private HashSet<string> m_questItems = new HashSet<string>();

    private List<string> m_hungerItems = new List<string>();

    private Dictionary<string, float> m_itemValues = new Dictionary<string, float>();

    private void Awake()
    {
        ItemController.OnQuestItemCollected += AddQuestItem;
        ItemController.OnConsumableCollected += AddHungerItem;

        HealthController.OnDeath += PrintInventory;
        //LA RAISON POURQUOI ON LES UNSUBSCRIBE PAS C'EST PARCE QUE LES CONTROLLERS NE SERONT JAMAIS DÉTRUIT. CA SERAIT INUTILE.
    }

    //Tu n'a pas mentionné les special items dans l'énoncé alors je l'ai pas fait pour l'inventaire

    public bool CheckIfHasItem(string itemName)
    {
        return m_itemValues.ContainsKey(itemName);
    }

    public HashSet<string> GetQuestItems()
    {
        return m_questItems;
    }

    public float GetItemValue(string itemName)
    {
        return m_itemValues[itemName];
    }

    public void ClearQuestItems()
    {
        m_questItems.Clear();
    }

    private void AddQuestItem(string name, float value)
    {
        m_questItems.Add(name);
        m_itemValues[name] = value;
    }

    private void AddHungerItem(string name, float value)
    {
        m_hungerItems.Add(name);
        m_itemValues[name] = value;
    }

    private void PrintInventory()
    {
        print("=== QUEST ITEMS ===");

        foreach (string item in m_questItems)
        {
            print(item);
        }

        print("=== HUNGER ITEMS ===");

        foreach (string item in m_hungerItems)
        {
            print(item);
        }

        print("=== ITEM VALUES ===");

        foreach (var pair in m_itemValues)
        {
            print(pair.Key + " -> " + pair.Value);
        }

        ConvertHungerItems();
    }

    private void ConvertHungerItems()
    {
        Dictionary<string, int> groupedItems = new Dictionary<string, int>();

        foreach (var item in m_hungerItems)
        {
            if (!groupedItems.ContainsKey(item)) groupedItems[item] = 0;
            groupedItems[item]++;
        }

        print("=== GROUPED HUNGER ITEMS ===");

        foreach (var pair in groupedItems)
        {
            print(pair.Key + " : " + pair.Value);
        }
    }
}