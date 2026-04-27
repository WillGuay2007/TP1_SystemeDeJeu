using System.Collections.Generic;
using UnityEngine;

public class PrefabPool : MonoBehaviour, IObjectPool
{
    [SerializeField] private GameObject m_prefab;
    private Queue<GameObject> m_prefabPoolQueue = new Queue<GameObject>();

    public GameObject GetPrefab()
    {
        if (m_prefabPoolQueue.Dequeue() != null)
        {
            GameObject pooledPrefab = m_prefabPoolQueue.Dequeue();
            pooledPrefab.SetActive(true);
            return pooledPrefab;
        }
        else
        {
            GameObject newPrefab = Instantiate(m_prefab);
            return newPrefab;
        }
    }
}
