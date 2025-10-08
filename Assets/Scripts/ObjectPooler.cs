using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance; // 單例模式，方便存取

    public GameObject objectToPool; // 要被池化的物件 (我們的 Obstacle Prefab)
    public int amountToPool; // 初始池化數量 (例如 20)

    private List<GameObject> pooledObjects;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false); // 預先創建，但先不啟用
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        // 從池中尋找一個未啟用的物件
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // 如果池中所有物件都在使用中，(可選)創建一個新的並加入池中
        // GameObject obj = Instantiate(objectToPool);
        // obj.SetActive(false);
        // pooledObjects.Add(obj);
        // return obj;
        return null; // 或者返回 null，表示池已用盡
    }
}