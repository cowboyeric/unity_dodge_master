using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 我們不再需要 moveSpeed 和 Rigidbody 了，因為是直接跟隨手指
    // private Rigidbody2D rb;      
    // public float moveSpeed = 7f; 

    private float minX, maxX; // 用於儲存螢幕邊界
    private Animator playerAnimator; // 用於控制動畫

    void Start()
    {
        // 這部分的邊界計算邏輯保持不變，非常重要
        Camera mainCamera = Camera.main;
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        float playerHalfWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        minX = bottomLeft.x + playerHalfWidth;
        maxX = topRight.x - playerHalfWidth;

        // 新增的程式碼
        playerAnimator = GetComponent<Animator>();
        if (playerAnimator == null)
        {
            Debug.LogError("Player 物件上沒有 Animator 組件！");
        }
    }

    void Update()
    {
        // 記錄玩家上一幀的位置
        Vector2 previousPosition = transform.position;

        // Input.GetMouseButton(0) 在電腦上代表滑鼠左鍵，在手機上代表第一根手指的觸控
        if (Input.GetMouseButton(0))
        {
            // 1. 獲取滑鼠/觸控的螢幕座標 (單位是像素)
            Vector3 inputPosition = Input.mousePosition;

            // 2. 將螢幕座標轉換為遊戲世界座標
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(inputPosition);

            // 3. 創建玩家的目標位置
            // 我們只關心 X 軸的移動，Y 軸保持玩家原有的高度不變
            Vector2 targetPosition = new Vector2(worldPosition.x, transform.position.y);

            // 4. 套用我們之前計算好的邊界限制
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

            // 5. 直接更新玩家的位置到目標位置
            transform.position = targetPosition;
        }

        // --- 新增的動畫控制邏輯 ---
        if (playerAnimator != null)
        {
            // 判斷玩家是否有足夠的水平移動
            // 我們檢查當前位置和上一幀位置的水平差異
            // 如果差異很小 (小於一個閾值)，就認為沒有移動
            bool isCurrentlyMoving = Mathf.Abs(transform.position.x - previousPosition.x) > 0.01f;

            // 設定 Animator 的 IsMoving 參數
            playerAnimator.SetBool("IsMoving", isCurrentlyMoving);
        }
    }

    // 由於我們不再使用物理系統移動 (Rigidbody)，FixedUpdate 函式可以被安全地移除或留空
    // private void FixedUpdate()
    // {
    // }

    // OnCollisionEnter2D 碰撞邏輯保持不變，它依然正常工作
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.instance.AddScore(10);
            collision.gameObject.SetActive(false); // 將 Destroy 替換為 SetActive(false)
        }
    }
}