using UnityEngine;

public interface ISlot
{
    void SetItem(ItemSO item);
    ItemSO GetItem();
    void TakeOne();
    void SetHighlight(bool status);
    bool CanAcceptItem(ItemSO item);
}
