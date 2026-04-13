using UnityEngine;
public class CraftingStationWindow : Window
{
    [SerializeField] private RectTransform m_slotsPanel;
    [SerializeField] private SlotController m_slotController;
    [SerializeField] private CraftableController m_craftableController;
    [SerializeField] private CraftableSlot m_craftableSlotPrefab;
    [SerializeField] private ResultSlot m_resultSlotPrefab;

    private CraftableSlot m_firstIngredientSlot;
    private CraftableSlot m_secondIngredientSlot;
    private ResultSlot m_resultSlot;

    //Pense pas que c'est de la logique, c'est de l'UI.
    public void CreateSlots()
    {
        m_firstIngredientSlot = Instantiate(m_craftableSlotPrefab, m_slotsPanel);
        m_secondIngredientSlot = Instantiate(m_craftableSlotPrefab, m_slotsPanel);
        m_resultSlot = Instantiate(m_resultSlotPrefab, m_slotsPanel);

        m_firstIngredientSlot.Init(m_slotController);
        m_secondIngredientSlot.Init(m_slotController);
        //Pas besoin de init le result slot.
    }

    public CraftableSlot GetFirstIngredientSlot() => m_firstIngredientSlot;
    public CraftableSlot GetSecondIngredientSlot() => m_secondIngredientSlot;
    public ResultSlot GetResultSlot() => m_resultSlot;

    public void DestroySlots()
    {
        if (m_firstIngredientSlot != null) Destroy(m_firstIngredientSlot.gameObject);
        if (m_secondIngredientSlot != null) Destroy(m_secondIngredientSlot.gameObject);
        if (m_resultSlot != null) Destroy(m_resultSlot.gameObject);
        m_firstIngredientSlot = null;
        m_secondIngredientSlot = null;
        m_resultSlot = null;
    }
}
