using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object Pool Manager", menuName = "SO/Object Pool Manager")]
public class ObjectPoolManagerSO : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    private Queue<GameObject> pool = new Queue<GameObject>();

    private Transform persistentParent;

    public void Initialize(GameObject prefab)
    {
        this.prefab = prefab;
        pool.Clear();
    }

    public GameObject Get(Transform parent)
    {
        GameObject obj = null;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            obj = Instantiate(prefab);
        }

        obj.transform.SetParent(parent);

        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        if(persistentParent == null)
        {
            persistentParent = new GameObject($"Persistent Objects - {prefab.name}").transform;
            DontDestroyOnLoad(persistentParent.gameObject);
        }

        obj.SetActive(false);
        obj.transform.SetParent(persistentParent);
        pool.Enqueue(obj);
    }
}
