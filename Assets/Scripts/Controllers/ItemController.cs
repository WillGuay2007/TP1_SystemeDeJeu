using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private ItemDrop m_itemDropPrefab;
    public event Action<ItemSO> OnConsumableCollected;
    public event Action<ItemSO> OnPickupableCollected;

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

    public ItemDrop SpawnItem(ItemSO item, Vector3 position)
    {
        ItemDrop itemDrop = Instantiate(m_itemDropPrefab, position, Quaternion.identity);
        itemDrop.SetSO(item);
        itemDrop.Init(this);
        return itemDrop;
    }
}
