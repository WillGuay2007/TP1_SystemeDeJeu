using System;
using UnityEngine;

public class ItemDrop : MonoBehaviour, ICollectible
{
    [SerializeField] private ItemSO m_itemInfos;
    public ItemSO itemInfos => m_itemInfos;
    private Action<ItemSO> m_onCollected;

    public void Init(ItemController controller)
    {
        m_onCollected = controller.CollectItem;
        GetComponent<MeshRenderer>().material.color = itemInfos.itemColor;
    }

    public void SetSO(ItemSO itemInfos) => m_itemInfos = itemInfos;

    public void Collect()
    {
        m_onCollected?.Invoke(itemInfos);
        Destroy(gameObject);
    }
}
