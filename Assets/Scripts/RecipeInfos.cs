using UnityEngine;

public class RecipeInfos : MonoBehaviour
{
    [SerializeField] private IconSlot firstIngredientIcon;
    [SerializeField] private IconSlot secondIngredientIcon;
    [SerializeField] private IconSlot resultIcon;

    public void SetRecipe(CraftRecipe recipe)
    {
        firstIngredientIcon.SetItem(recipe.craftable1);
        secondIngredientIcon.SetItem(recipe.craftable2);
        resultIcon.SetItem(recipe.result);
    }

}
