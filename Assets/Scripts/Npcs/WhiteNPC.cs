using System.Linq;
using UnityEngine;

public class WhiteNPC : NPC
{
    [SerializeField] InventoryController m_inventoryController;
    [SerializeField] ExperienceController m_experienceController;
    public override void Interact()
    {
        foreach (ItemSO item in m_inventoryController.GetItems().Where(i => i.expAmount > 0))
        {
            m_experienceController.AddExperience(item.expAmount * 2);
        }

        m_inventoryController.ClearExpItems();

        //Print volontaire
        //print("All quest items exchanged for experience");
    }
}
