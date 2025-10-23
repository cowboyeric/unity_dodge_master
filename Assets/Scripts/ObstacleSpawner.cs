using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // --- MODIFIED ---
    // 我們需要知道兩種 prefab，以便計算正確的寬度
    public GameObject obstaclePrefabMode1;
    public GameObject obstaclePrefabMode2;

    public float spawnRate = 1.2f;

    private float spawnRangeX;
    private float nextSpawnTime = 0f;
    private GameObject currentObstaclePrefab; // 用來儲存當前模式選擇的 Prefab

    void Start()
    {
        // --- NEW LOGIC ---
        // 1. 根據 GameSettingsManager 選擇正確的 Prefab
        if (GameSettingsManager.Instance != null)
        {
            if (GameSettingsManager.Instance.currentMode == 0)
            {
                currentObstaclePrefab = obstaclePrefabMode1;
            }
            else
            {
                currentObstaclePrefab = obstaclePrefabMode2;
            }
        }
        else
        {
            // 如果 GameSettingsManager 不存在（例如直接從 GameScene 測試）
            // 預設使用模式一
            currentObstaclePrefab = obstaclePrefabMode1;
            Debug.LogWarning("GameSettingsManager not found. Defaulting to Mode 1.");
        }

        // --- MODIFIED ---
        // 2. 現在，使用「當前選擇的」 Prefab 來計算邊界
        Camera mainCamera = Camera.main;
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        if (currentObstaclePrefab != null)
        {
            float obstacleHalfWidth = currentObstaclePrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
            spawnRangeX = topRight.x - obstacleHalfWidth;
        }
        else
        {
            Debug.LogError("Current Obstacle Prefab is not set in Spawner!");
        }
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    // SpawnObstacle 函式保持不變，它的工作只是向物件池"要"一個物件
    void SpawnObstacle()
    {
        GameObject obstacle = ObjectPooler.Instance.GetPooledObject();

        if (obstacle != null)
        {
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector2 spawnPosition = new Vector2(randomX, 6f);

            obstacle.transform.position = spawnPosition;
            obstacle.SetActive(true);
        }
    }
}