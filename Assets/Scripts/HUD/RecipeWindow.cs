using System.Collections.Generic;
using UnityEngine;

public class RecipeWindow : Window
{

    [SerializeField] private RecipeInfos recipePrefab;

    public void Init(List<CraftRecipe> recipes)
    {
        foreach (CraftRecipe recipe in recipes)
        {
            RecipeInfos recipeInfo = Instantiate(recipePrefab, transform);
            recipeInfo.SetRecipe(recipe);
        }
    }

}
