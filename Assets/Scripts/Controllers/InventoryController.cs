using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    [SerializeField] private InventoryWindow m_inventoryWindow;

    private HashSet<ItemSO> m_items = new HashSet<ItemSO>();

    private Dictionary<ItemSO, InventorySlot> m_slots = new();

    private ItemController m_itemController;
    private SlotController m_slotController;

    public void SetDependencies(GameController gameController)
    {
        m_itemController = gameController.itemController;
        m_slotController = gameController.slotController;
    }

    public void Init()
    {
        m_itemController.OnPickupableCollected += AddInventoryItem;
    }

    public void InternalStart()
    {
        HUDControllerV2.Instance.OpenWindow(m_inventoryWindow);
    }

    public bool CheckIfHasItem(ItemSO item)
    {
        return m_slots.ContainsKey(item);
    }

    public HashSet<ItemSO> GetItems() => m_items;
    public float GetItemExpValue(ItemSO item) => item.expAmount;
    public void ClearExpItems()
    {
        foreach (ItemSO item in m_items.Where(i => i.expAmount > 0).ToList())
        {
            Destroy(m_slots[item].gameObject);
            m_slots.Remove(item);
        }
        m_items.RemoveWhere(item => item.expAmount > 0);
    }

    private void RemoveKey(ItemSO item)
    {
        m_items.Remove(item);
        m_slots.Remove(item);
    }

    public void AddInventoryItem(ItemSO item)
    {
        if (item.itemType != ItemSO.ItemType.Inventory) return;
        m_items.Add(item);

        if (m_slots.ContainsKey(item))
        {
            m_slots[item].AddQuantity(1);
        } else
        {
            InventorySlot slot = m_inventoryWindow.CreateSlot();
            slot.Init(m_slotController);
            slot.SetQuantity(1);
            slot.SetItem(item);
            slot.SlotDestroyed += RemoveKey;
            m_slots.Add(item, slot);
        }
    }
}