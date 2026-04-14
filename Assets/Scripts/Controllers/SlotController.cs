using UnityEngine;

public class SlotController : MonoBehaviour
{

    private ISlot m_lastSlotSelected;

    public bool HasSelection() => m_lastSlotSelected != null;

    public void SetDependencies(GameController gameController)
    {

    }

    public void Init()
    {

    }

    public void InternalStart()
    {

    }

    public void SelectSlot(ISlot slot)
    {
        if (m_lastSlotSelected != null)
        {
            if (m_lastSlotSelected == slot)
            {
                m_lastSlotSelected.SetHighlight(false);
                m_lastSlotSelected = null;
                return;
            }

            ItemSO item = m_lastSlotSelected.GetItem();
            if (slot.CanAcceptItem(item))
            {
                AudioManager.Instance.PlayAudio(AudioManager.Sounds.DropItemInSlot);
                m_lastSlotSelected.TakeOne();
                slot.SetItem(item);
                m_lastSlotSelected.SetHighlight(false);
                m_lastSlotSelected = null;
                return;
            }
            else
            {
                //Je l'ai gardé pour aide visuel sinon la personne pourrait penser que c'est un bug vu que ca accept pas. Meme si ca dit de ne pas laisser de print.
                print("Slot cannot accept item");
                return;
            }
        }
        m_lastSlotSelected = slot;
        slot.SetHighlight(true);
    }

}
