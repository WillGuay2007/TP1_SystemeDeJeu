using UnityEngine;

public class CraftableSlot : Slot
{
    public override bool CanAcceptItem(ItemSO item)
    {
        if (itemData != null) return false;
        if (item is CraftableItemSO)
        {
            return true;
        }
        return false;
    }


    public override void SetItem(ItemSO item)
    {
        if (item == null || item is CraftableItemSO)
        {
            base.SetItem(item);
        }
        else
        {
            //print("Tried to assign a non craftable item to a craft slot.");
        }
    }
}
