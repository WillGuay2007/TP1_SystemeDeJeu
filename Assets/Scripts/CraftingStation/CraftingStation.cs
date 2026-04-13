using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] private CraftingStationWindow m_craftingStationWindow;
    [SerializeField] private GameObject m_spawnPoint;
    [SerializeField] private CraftableController m_craftableController;
    [SerializeField] private InventoryController m_inventoryController;
    [SerializeField] private CraftRecipe[] m_recipes;

    private void Awake()
    {
        m_craftableController.CraftCompleted += () => m_craftingStationWindow.DestroySlots(); //Pour pas re récupérer les items après un craft réussi. sinon infinite duplication glitch!
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        m_craftingStationWindow.CreateSlots();
        m_craftingStationWindow.GetFirstIngredientSlot().SlotChanged += CheckRecipe;
        m_craftingStationWindow.GetSecondIngredientSlot().SlotChanged += CheckRecipe;
        HUDControllerV2.Instance.OpenWindow(m_craftingStationWindow);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        CraftableSlot firstSlot = m_craftingStationWindow.GetFirstIngredientSlot();
        CraftableSlot secondSlot = m_craftingStationWindow.GetSecondIngredientSlot();
        if (firstSlot != null && firstSlot.GetItem() != null) m_inventoryController.AddInventoryItem(firstSlot.GetItem());
        if (secondSlot != null && secondSlot.GetItem() != null) m_inventoryController.AddInventoryItem(secondSlot.GetItem()); 
        m_craftingStationWindow.DestroySlots(); //Pas besoin de unsubscribe les events parce que les slots sont détruits, donc plus de listeners.
        HUDControllerV2.Instance.CloseWindow(m_craftingStationWindow);
    }

    private void CheckRecipe()
    {
        CraftableItemSO item1 = m_craftingStationWindow.GetFirstIngredientSlot().GetItem() as CraftableItemSO;
        CraftableItemSO item2 = m_craftingStationWindow.GetSecondIngredientSlot().GetItem() as CraftableItemSO;
        ResultSlot resultSlot = m_craftingStationWindow.GetResultSlot();
        if (item1 != null && item2 != null)
        {
            if (!CheckCraftingStationRecipeValidity(item1, item2)) return;
            CraftRecipe recipe = m_craftableController.CheckCombination(item1, item2);
            if (recipe != null)
            {
                //print("Received recipe data."); // Je le laisse volontairement
                resultSlot.PassCraftingData(() => HUDControllerV2.Instance.CloseWindow(m_craftingStationWindow),
                    m_craftableController, item1, item2, m_spawnPoint.transform.position); //Ca hook le click du slot.
                resultSlot.SetItem(recipe.result);
            }
            else
            {
                resultSlot.ResetCraftingData();
                resultSlot.SetItem(null);
            }
        }
        else
        {
            resultSlot.ResetCraftingData();
            resultSlot.SetItem(null);
        }
    }

    private bool CheckCraftingStationRecipeValidity(CraftableItemSO item1, CraftableItemSO item2)
    {
        foreach (CraftRecipe recipe in m_recipes)
        {
            if ((recipe.craftable1 == item1 && recipe.craftable2 == item2) || (recipe.craftable1 == item2 && recipe.craftable2 == item1))
            {
                return true;
            }
        }
        return false;
    }
}
