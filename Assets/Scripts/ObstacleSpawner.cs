using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // 用來存放我們的障礙物 Prefab
    public float spawnRate = 1.2f;      // 每隔多少秒生成一個障礙物

    // spawnRangeX 現在是私有的，因為我們將從程式碼中自動計算它
    private float spawnRangeX;

    private float nextSpawnTime = 0f; // 下一次生成障礙物的時間

    // 新增 Start 函式來做初始化
    void Start()
    {
        // 獲取主攝影機的邊界
        Camera mainCamera = Camera.main;
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        // 獲取障礙物 Prefab 的一半寬度
        // 確保 obstaclePrefab 有被正確賦值，否則會報錯
        if (obstaclePrefab != null)
        {
            float obstacleHalfWidth = obstaclePrefab.GetComponent<SpriteRenderer>().bounds.extents.x;

            // 計算出最大的生成X座標
            spawnRangeX = topRight.x - obstacleHalfWidth;
        }
        else
        {
            Debug.LogError("障礙物 Prefab 沒有被設定在 Spawner 上！");
        }
    }

    // Update 函式不需要任何改變
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    // SpawnObstacle 函式也不需要任何改變
    void SpawnObstacle()
    {
        // 從物件池獲取一個物件
        GameObject obstacle = ObjectPooler.Instance.GetPooledObject();

        if (obstacle != null) // 確保成功獲取到物件
        {
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector2 spawnPosition = new Vector2(randomX, 6f);

            obstacle.transform.position = spawnPosition;
            obstacle.SetActive(true); // 啟用物件，讓它出現
        }
    }
}
