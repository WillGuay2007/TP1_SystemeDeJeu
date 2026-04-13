using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour, ISlot, IPointerClickHandler
{
    private Color HIGHLIGHT_COLOR = Color.green;

    [SerializeField] private Image m_itemImage; //Moi j'aurai mis un getcomponent mais je fais serializefield vu que c'est demandé
    [SerializeField] private Image m_background;
    protected ItemSO itemData;

    public event Action SlotChanged;
    public event Action<ItemSO> SlotDestroyed;

    protected SlotController m_slotController;
    private Color m_originalColor;

    public void Init(SlotController slotController) => m_slotController = slotController;

    private void Awake()
    {
        m_originalColor = m_background.color;
    }

    public virtual void SetItem(ItemSO item)
    {
        itemData = item;
        UpdateSlot();
        SlotChanged?.Invoke();
    }

    public void DestroySlot()
    {
        SlotDestroyed?.Invoke(itemData);
        SetItem(null);
        Destroy(gameObject);
    }

    public void SetHighlight(bool status)
    {
        m_background.color = status ? HIGHLIGHT_COLOR : m_originalColor;
    }

    public ItemSO GetItem()
    {
        return itemData;
    }

    public virtual void TakeOne()
    {
        SetItem(null);
    }

    protected virtual void UpdateSlot()
    {
        if (itemData == null)
        {
            m_itemImage.sprite = null;
            return;
        }
        m_itemImage.sprite = itemData.icon;
    }

    public abstract bool CanAcceptItem(ItemSO item);

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        m_slotController.SelectSlot(this);
    }
}
