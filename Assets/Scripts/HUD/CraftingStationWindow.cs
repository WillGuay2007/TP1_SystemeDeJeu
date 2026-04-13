using UnityEngine;

//TODO: DEPLACER LA LOGIQUE HORS DE LA WINDOW (Si jai le temps)
public class CraftingStationWindow : Window
{
    [SerializeField] private SlotController m_slotController;
    [SerializeField] private CraftableController m_craftableController;
    [SerializeField] private CraftableSlot m_craftableSlotPrefab;
    [SerializeField] private ResultSlot m_resultSlotPrefab;
    private GameObject m_spawnPoint;

    private CraftableSlot m_firstIngredientSlot;
    private CraftableSlot m_secondIngredientSlot;
    private ResultSlot m_resultSlot;

    private void Awake()
    {
        m_firstIngredientSlot = Instantiate(m_craftableSlotPrefab, transform);
        m_secondIngredientSlot = Instantiate(m_craftableSlotPrefab, transform);
        m_resultSlot = Instantiate(m_resultSlotPrefab, transform);

        m_firstIngredientSlot.Init(m_slotController);
        m_secondIngredientSlot.Init(m_slotController);
        //Pas besoin de init le result slot.

        m_firstIngredientSlot.SlotChanged += CheckRecipe;
        m_secondIngredientSlot.SlotChanged += CheckRecipe;
    }

    public void SetSpawnPoint(GameObject spawnPoint)
    {
        m_spawnPoint = spawnPoint;
    }

    private void CheckRecipe()
    {
        CraftableItemSO item1 = m_firstIngredientSlot.GetItem() as CraftableItemSO;
        CraftableItemSO item2 = m_secondIngredientSlot.GetItem() as CraftableItemSO;
        if (item1 != null && item2 != null)
        {
            CraftRecipe recipe = m_craftableController.CheckCombination(item1, item2);
            if (recipe != null)
            {
                //print("Received recipe data."); // Je le laisse volontairement
                m_resultSlot.PassCraftingData(() => HUDControllerV2.Instance.CloseWindow(this),
                    m_craftableController, item1, item2, m_spawnPoint.transform.position); //Ca hook le click du slot.
                m_resultSlot.SetItem(recipe.result);
            } 
            else
            {
                m_resultSlot.ResetCraftingData();
                m_resultSlot.SetItem(null);
            }
        } else
        {
            m_resultSlot.ResetCraftingData();
            m_resultSlot.SetItem(null);
        }
    }

    public void ResetSlots()
    {
        m_firstIngredientSlot.SetItem(null);
        m_secondIngredientSlot.SetItem(null);
        m_resultSlot.SetItem(null);
    }
}
