using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    [SerializeField] private CraftingStationWindow m_craftingStationWindow;
    [SerializeField] private GameObject m_spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        HUDControllerV2.Instance.OpenWindow(m_craftingStationWindow);
        m_craftingStationWindow.SetSpawnPoint(m_spawnPoint);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        HUDControllerV2.Instance.CloseWindow(m_craftingStationWindow);
    }
}
