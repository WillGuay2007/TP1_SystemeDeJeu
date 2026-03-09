using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Inventory Data")]

public class PlayerInventory : ScriptableObject
{
    [Header("Items")]
    public int NumberOfQuestItems = 0;
    public int NumberOfSpecialItems = 0;
    public int NumberOfConsumableItem = 0;
    public int TotalItems = 0;

}
