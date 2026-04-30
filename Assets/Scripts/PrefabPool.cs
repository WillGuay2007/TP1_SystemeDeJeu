using System.Collections.Generic;
using UnityEngine;

//Generic parce que je voulais que le code dans turret aye pas besoin de caster. C'est plus clean comme ca
//Je pense que j'aurai pu rendre la classe elle meme generique mais je me complique pas la vie trop trop
//Je suis conscient qu'il peut y'avoir des problemes si la pool est mal utilisé par les autres modules (Exemple: Mauvais type).
public class PrefabPool : MonoBehaviour
{
    [SerializeField] private GameObject m_prefab;
    private Queue<GameObject> m_prefabPoolQueue = new Queue<GameObject>();

    public T GetPrefab<T>() where T : MonoBehaviour
    {
        if (m_prefabPoolQueue.Count > 0)
        {
            GameObject pooledPrefab = m_prefabPoolQueue.Dequeue();
            pooledPrefab.SetActive(true);
            return pooledPrefab.GetComponent<T>();
        }
        else
        {
            GameObject newPrefab = Instantiate(m_prefab);
            T component = newPrefab.GetComponent<T>();
            if (component == null)             {
                Debug.LogError("The prefab does not contain a component of type " + typeof(T).Name + ".");
                Destroy(newPrefab);
                return null;
            }
            return component;
        }
    }

    public void SimulateDestroy(GameObject prefab)
    {
        prefab.SetActive(false);
        m_prefabPoolQueue.Enqueue(prefab);
    }
}
