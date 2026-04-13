using UnityEngine;


[CreateAssetMenu(menuName = "Items/Item")]
public class ItemSO : ScriptableObject
{
    public enum ItemType { Inventory, Consumable }

    [SerializeField] private ItemType m_itemType;

    [SerializeField] private string m_itemName;
    [SerializeField] private Sprite m_icon;
    [SerializeField] private string m_description;
    [SerializeField] private float m_damageAmount;
    [SerializeField] private float m_expAmount;
    [SerializeField] private float m_hungerAmount;
    [SerializeField] private Color m_itemColor;

    public ItemType itemType => m_itemType;
    public string itemName => m_itemName;
    public Sprite icon => m_icon;
    public string description => m_description;
    public float damageAmount => m_damageAmount;
    public float expAmount => m_expAmount;
    public float hungerAmount => m_hungerAmount;
    public Color itemColor => m_itemColor;

}
