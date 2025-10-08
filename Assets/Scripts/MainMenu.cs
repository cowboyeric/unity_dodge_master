using UnityEngine;
using UnityEngine.SceneManagement; // 必須引用此命名空間來管理場景

public class MainMenu : MonoBehaviour
{
    // 這個公開函式將被我們的開始按鈕呼叫
    public void StartGame()
    {
        // SceneManager.LoadScene() 用於載入指定的場景。
        // 我們需要傳入下一個場景的名稱，也就是 "GameScene"。
        // 注意：場景名稱必須與您儲存的場景檔案名稱完全一致！
        SceneManager.LoadScene("GameScene");
        
        // 額外提示：如果您的遊戲有多個關卡，可以寫成 LoadScene("Level1"), LoadScene("Level2") 等。
    }

    // 您也可以在這裡加入一個退出遊戲的函式 (主要用於 PC 平台)
    public void QuitGame()
    {
        Debug.Log("遊戲已退出！"); // 在編輯器中顯示訊息方便測試
        Application.Quit();
    }
}