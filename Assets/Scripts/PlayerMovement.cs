using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    private Animator playerAnimator; // 確保這行還在

    private float minX, maxX; // 用於儲存螢幕邊界
    
    // --- 新增的變數 ---
    private bool isDragging = false; // 一個旗標，用來判斷角色當前是否被拖曳
    private Vector3 offset; // 用於儲存手指位置與角色中心的偏移量

    void Start()
    {
        // 這部分的邊界計算邏輯保持不變
        Camera mainCamera = Camera.main;
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        float playerHalfWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        minX = bottomLeft.x + playerHalfWidth;
        maxX = topRight.x - playerHalfWidth;

        playerAnimator = GetComponent<Animator>();
        if (playerAnimator == null)
        {
            Debug.LogError("Player 物件上沒有 Animator 組件！");
        }
    }

    // OnMouseDown() 是 Unity 的一個特殊函式。
    // 當使用者用滑鼠左鍵或手指「點擊」到這個物件的 Collider 時，這個函式會被自動呼叫一次。
    void OnMouseDown()
    {
        // 1. 計算偏移量：取得滑鼠/手指的世界座標，減去角色目前的位置
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // 2. 標記為開始拖曳
        isDragging = true;

        // --- 新增的動畫控制程式碼 ---
        // 3. 告訴 Animator 開始播放移動動畫
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("IsMoving", true);
        }
    }

    // OnMouseDrag() 是 Unity 的另一個特殊函式。
    // 在 OnMouseDown() 之後，只要使用者「按住並移動」滑鼠/手指，這個函式就會在每一幀被持續呼叫。
    void OnMouseDrag()
    {
        if (isDragging)
        {
            // 1. 取得滑鼠/手指的當前世界座標，並加上剛才計算的偏移量
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;

            // 2. 創建玩家的目標位置，只使用 X 軸，Y 軸保持不變
            Vector2 targetPosition = new Vector2(newPosition.x, transform.position.y);

            // 3. 套用我們之前計算好的邊界限制
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            
            // 4. 更新玩家的位置到目標位置
            transform.position = targetPosition;
            
        }
    }

    // OnMouseUp() 是 Unity 的特殊函式。
    // 當使用者「放開」滑鼠左鍵或手指時，這個函式會被自動呼叫一次。
    void OnMouseUp()
    {
        // 標記為停止拖曳
        isDragging = false;
        // 2. 告訴 Animator 播放待機動畫
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("IsMoving", false);
        }
    }

    // 碰撞邏輯保持不變
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.instance.AddScore(10);
            collision.gameObject.SetActive(false); // 將 Destroy 替換為 SetActive(false)
        }
    }
}