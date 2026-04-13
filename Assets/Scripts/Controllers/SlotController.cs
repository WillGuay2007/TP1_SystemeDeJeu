using UnityEngine;

public class SlotController : MonoBehaviour
{

    private ISlot m_lastSlotSelected;

    public bool HasSelection() => m_lastSlotSelected != null;

    public void SetDependencies(GameController gameController)
    {

    }

    public void Init()
    {

    }

    public void InternalStart()
    {

    }

    public void SelectSlot(ISlot slot)
    {
        if (m_lastSlotSelected != null)
        {
            if (m_lastSlotSelected == slot)
            {
                m_lastSlotSelected.SetHighlight(false);
                m_lastSlotSelected = null;
                return;
            }

            ItemSO item = m_lastSlotSelected.GetItem();
            if (slot.CanAcceptItem(item))
            {
                m_lastSlotSelected.TakeOne();
                slot.SetItem(item);
                m_lastSlotSelected.SetHighlight(false);
                m_lastSlotSelected = null;
                return;
            }
            else
            {
                //print("Cannot put item in slot. Slot is invalid for this type of item.");
                return;
            }
        }
        m_lastSlotSelected = slot;
        slot.SetHighlight(true);
    }

}
