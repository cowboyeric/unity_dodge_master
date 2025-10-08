using UnityEngine;

public class Destoryer : MonoBehaviour
{
    // 這是一個特殊的 Unity 內建函式
    // 當有其他 Collider 2D 進入到附加此腳本的物件的觸發器 (Trigger) 範圍時，
    // Unity 會自動呼叫此函式
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            other.gameObject.SetActive(false); // 將 Destroy 替換為 SetActive(false)
        }
    }
}
