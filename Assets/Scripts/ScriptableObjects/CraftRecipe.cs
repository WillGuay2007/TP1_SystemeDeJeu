using UnityEngine;

[CreateAssetMenu(menuName = "Items/Craftable/Recipes")]

public class CraftRecipe : ScriptableObject
{
    [SerializeField] private float m_craftingTime;
    [SerializeField] private CraftableItemSO m_craftable1;
    [SerializeField] private CraftableItemSO m_craftable2;
    [SerializeField] private CraftableItemSO m_result;
    public float craftingTime => m_craftingTime;
    public ItemSO craftable1 => m_craftable1;
    public ItemSO craftable2 => m_craftable2;
    public CraftableItemSO result => m_result;
}
