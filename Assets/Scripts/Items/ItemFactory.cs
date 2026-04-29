using UnityEngine;

public class ItemFactory : MonoBehaviour
{
    [SerializeField] private ItemDrop m_itemPrefab;
    [SerializeField] private ItemController m_itemController;
    public static ItemFactory Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            Debug.LogWarning("Tried to create a second item factory\nThere should only be one.");
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //Une seule méthode puisque la facon que j'ai design fait en sorte que la config est ce qui détermine son type.
    public ItemDrop CreateItem(ItemSO itemData, Vector3 spawnPosition)
    {
        ItemDrop item = Instantiate(m_itemPrefab, spawnPosition, Quaternion.identity);
        item.SetSO(itemData);
        item.Init(m_itemController);
        return item; 
    }
}
