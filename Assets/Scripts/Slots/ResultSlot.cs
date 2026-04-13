using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResultSlot : Slot
{
    private bool m_hasReceivedCraftingData;
    private Action m_onCraftStarted;
    private CraftableController m_craftableController;
    private CraftableItemSO m_item1;
    private CraftableItemSO m_item2;
    private Vector3 m_spawnPosition;

    public override bool CanAcceptItem(ItemSO item)
    {
        return false;
    }

    public void PassCraftingData(Action onCraftStarted, CraftableController craftableController, CraftableItemSO item1, CraftableItemSO item2, Vector3 spawnPosition)
    {
        m_hasReceivedCraftingData = true;
        m_onCraftStarted = onCraftStarted;
        m_craftableController = craftableController;
        m_item1 = item1;
        m_item2 = item2;
        m_spawnPosition = spawnPosition;
    }

    public void ResetCraftingData()
    {
        m_hasReceivedCraftingData = false;
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (!m_hasReceivedCraftingData)
        {
            //print("The selected result slot has not received crafting data.");
            return;
        }
        m_onCraftStarted?.Invoke();
        m_craftableController.StartCrafting(m_item1, m_item2, m_spawnPosition);
    }
}
