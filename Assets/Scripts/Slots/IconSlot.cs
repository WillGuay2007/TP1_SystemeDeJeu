using UnityEngine;
using UnityEngine.EventSystems;

public class IconSlot : Slot
{
    public override bool CanAcceptItem(ItemSO item)
    {
        return false;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        return;
    }

    //Ce slot est juste pour le display.
}
