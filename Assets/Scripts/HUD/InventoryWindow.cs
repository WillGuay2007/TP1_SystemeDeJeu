using UnityEngine;

public class InventoryWindow : Window
{
    [SerializeField] private InventorySlot m_inventorySlotPrefab;


    public InventorySlot CreateSlot()
    {
        return Instantiate(m_inventorySlotPrefab, transform);
    }

}
