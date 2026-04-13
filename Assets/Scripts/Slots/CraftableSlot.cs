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
            //Je le garde parce que c'est important sinon il y'a des confusions
            print("Tried to assign a non craftable item to a craft slot.");
        }
    }
}
