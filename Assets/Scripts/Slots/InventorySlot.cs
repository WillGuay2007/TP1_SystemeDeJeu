using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : Slot
{
    private int m_quantity = 0;

    [SerializeField] private TextMeshProUGUI m_quantityText;

    public void SetQuantity(int quantity) { m_quantity = quantity; UpdateSlot(); }
    public void AddQuantity(int quantity) { m_quantity += quantity; UpdateSlot(); }
    public int GetQuantity() => m_quantity;

    protected override void UpdateSlot()
    {
        if (itemData == null)
        {
            base.UpdateSlot();
            m_quantityText.text = "x0";
            return;
        }
        base.UpdateSlot();
        m_quantityText.text = "x" + m_quantity;
    }

    public override void TakeOne()
    {
        m_quantity--;
        UpdateSlot();
        //if (m_quantity <= 0)
        //{
        //    DestroySlot();
        //} je sais pas si je va le remettre plus tard.
    }

    public override void SetItem(ItemSO item)
    {
        if (item != null && item == itemData)
        {
            AddQuantity(1);
        }
        else
        {
            base.SetItem(item);
        }
    }

    public override bool CanAcceptItem(ItemSO item)
    {
        if (itemData == null) return true;
        if (item == itemData)
        {
            return true;
        }
        return false;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (m_quantity <= 0 && !m_slotController.HasSelection()) return; //Pour select seulement si c'est pour en replacer.
        base.OnPointerClick(eventData);
    }
}
