using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class CraftableController : MonoBehaviour
{

    private const string CRAFTING_PREFIX_TEXT = "Crafting progress: ";

    [SerializeField] private List<CraftRecipe> m_possibleRecipes = new List<CraftRecipe>();
    [SerializeField] private CraftingWindow m_craftingWindow;
    public event Action CraftCompleted;
    private ItemController m_itemController;
    private PlayerInputController m_playerInputController;
    private Coroutine m_craftingCoroutine = null;

    public void SetDependencies(GameController gameController)
    {
        m_itemController = gameController.itemController;
        m_playerInputController = gameController.playerInputController;
    }

    public void Init()
    {
        m_playerInputController.OnMoveInput += InterruptCrafting;
    }

    public void InternalStart()
    {

    }

    private void InterruptCrafting()
    {
        if (m_craftingCoroutine != null) StopCoroutine(m_craftingCoroutine);
        m_craftingCoroutine = null;
        if (m_craftingWindow.gameObject.activeSelf)
        {
            HUDControllerV2.Instance.CloseWindow(m_craftingWindow);
        }
    }

    public CraftRecipe CheckCombination(CraftableItemSO obj1, CraftableItemSO obj2)
    {
        foreach (CraftRecipe recipe in m_possibleRecipes)
        {
            if ((recipe.craftable1 == obj1 && recipe.craftable2 == obj2) || (recipe.craftable1 == obj2 && recipe.craftable2 == obj1))
            {
                return recipe;
            }
        }
        return null;
    }

    public void StartCrafting(CraftableItemSO obj1, CraftableItemSO obj2, Vector3 spawnPosition)
    {
        if (m_craftingCoroutine != null)
        {
            //print("Cannot start crafting.\nReason: Crafting is already in progress.");
            return;
        }
        CraftRecipe recipe = CheckCombination(obj1, obj2);
        if (recipe != null)
        {
           HUDControllerV2.Instance.OpenWindow(m_craftingWindow);
           m_craftingCoroutine = StartCoroutine(CraftCoroutine(recipe, spawnPosition));
        } 
    }

    private IEnumerator CraftCoroutine(CraftRecipe recipeToFollow, Vector3 spawnPosition)
    {
        float elapsedTime = 0f;
        float craftTime = recipeToFollow.craftingTime;
        float progressFraction = 0f;
        while (true)
        {
            elapsedTime += Time.deltaTime;
            progressFraction = elapsedTime / craftTime;
            m_craftingWindow.SetCraftProgressText(CRAFTING_PREFIX_TEXT + (progressFraction * 100).ToString("F2") + "%");
            m_craftingWindow.SetCraftSliderValue(progressFraction);
            if (elapsedTime >= craftTime)
            {
                m_itemController.SpawnItem(recipeToFollow.result, spawnPosition);
                AudioManager.Instance.PlaySound(AudioManager.Sounds.ItemCrafted);
                HUDControllerV2.Instance.CloseWindow(m_craftingWindow);
                CraftCompleted?.Invoke();
                m_craftingCoroutine = null;

                yield break;
            }
            yield return null;
        }
    }
}
