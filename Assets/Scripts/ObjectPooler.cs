using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance; // 單例模式

    // --- MODIFIED ---
    // 物件池也需要知道兩種 Prefab
    public GameObject obstaclePrefabMode1;
    public GameObject obstaclePrefabMode2;
    public int amountToPool; // 初始池化數量

    private List<GameObject> pooledObjects;
    private GameObject prefabToPool; // 儲存當前模式決定要池化的 Prefab

    void Awake()
    {
        Instance = this;

        // --- NEW LOGIC ---
        // 在 Awake() 中就決定好要使用哪個 Prefab
        if (GameSettingsManager.Instance != null)
        {
            if (GameSettingsManager.Instance.currentMode == 0)
            {
                prefabToPool = obstaclePrefabMode1;
            }
            else
            {
                prefabToPool = obstaclePrefabMode2;
            }
        }
        else
        {
            // 預設
            prefabToPool = obstaclePrefabMode1;
            Debug.LogWarning("GameSettingsManager not found. ObjectPooler defaulting to Mode 1.");
        }
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        if (prefabToPool == null)
        {
            Debug.LogError("Object Pooler does not have a prefab to pool!");
            return;
        }

        // --- MODIFIED ---
        // 使用我們在 Awake() 中選擇的 prefabToPool 來創建初始物件
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(prefabToPool); 
            obj.SetActive(false);
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
        
        // 可選：如果池不夠大，動態創建一個新的
        GameObject newObj = Instantiate(prefabToPool);
        newObj.SetActive(false);
        pooledObjects.Add(newObj);
        return newObj;
    }
}