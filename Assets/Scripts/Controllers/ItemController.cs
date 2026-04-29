using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private const int START_ITEMS_GRID_X_OFFSET = -10;
    private const int START_ITEMS_GRID_Z_OFFSET = -10;
    private const int START_ITEMS_ROW_LENGTH = 3;
    private const float ITEM_TOUCH_FLOOR_Y_POS = 0.625f;

    public event Action<ItemSO> OnConsumableCollected;
    public event Action<ItemSO> OnPickupableCollected;
    [SerializeField] private ItemSO[] m_startItems;

    public void SetDependencies(GameController gameController)
    {

    }

    public void Init()
    {
        foreach (ItemDrop drop in FindObjectsByType<ItemDrop>(FindObjectsSortMode.None))
        {
            drop.Init(this);
        }
    }

    public void InternalStart()
    {
        CreateStartItems();
    }

    public void CollectItem(ItemSO item)
    {
        if (item.itemType == ItemSO.ItemType.Inventory)
        {
            OnPickupableCollected?.Invoke(item);
            AudioManager.Instance.PlayAudio(AudioManager.Sounds.PickItem);
        }
        else if (item.itemType == ItemSO.ItemType.Consumable)
        {
            OnConsumableCollected?.Invoke(item);
            AudioManager.Instance.PlayAudio(AudioManager.Sounds.ConsumeItem);
        }
    }

    private void CreateStartItems()
    {
        for (int itemTypeIndex = 0; itemTypeIndex < m_startItems.Length; itemTypeIndex++)
        {
            ItemSO itemData = m_startItems[itemTypeIndex];
            for (int rowIndex = 0; rowIndex < START_ITEMS_ROW_LENGTH; rowIndex++)
            {
                Vector3 itemSpawnPosition = new Vector3(
                    START_ITEMS_GRID_X_OFFSET + itemTypeIndex,
                    ITEM_TOUCH_FLOOR_Y_POS,
                    START_ITEMS_GRID_X_OFFSET + rowIndex
                    );

                ItemFactory.Instance.CreateItem(itemData, itemSpawnPosition);
            }
        }
    }
}
