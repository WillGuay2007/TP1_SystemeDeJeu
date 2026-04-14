using System;
using UnityEngine;

public class ItemDrop : MonoBehaviour, ICollectible
{
    [SerializeField] private ItemSO m_itemInfos;
    [SerializeField] private MeshRenderer m_meshRenderer;
    public ItemSO itemInfos => m_itemInfos;
    private Action<ItemSO> m_onCollected;

    public void Init(ItemController controller)
    {
        m_onCollected = controller.CollectItem;
        m_meshRenderer.material.color = itemInfos.itemColor;
    }

    public void SetSO(ItemSO itemInfos) => m_itemInfos = itemInfos;

    public void Collect()
    {
        m_onCollected?.Invoke(itemInfos);
        Destroy(gameObject);
    }
}
