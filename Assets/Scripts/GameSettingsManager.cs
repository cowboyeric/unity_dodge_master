using UnityEngine;
using TMPro; // 引用 TextMeshPro 命名空間

public class GameSettingsManager : MonoBehaviour
{
    public static GameSettingsManager Instance; // 單例模式

    public int currentMode = 0; // 0 = 模式一, 1 = 模式二
    public TextMeshProUGUI modeToggleButtonText; // 連結到模式切換按鈕上的文字

    void Awake()
    {
        // 實現單例模式：確保只有一個 GameSettingsManager 實例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 場景切換時不銷毀
        }
        else
        {
            Destroy(gameObject); // 如果已有實例，銷毀自己
        }
    }

    // 當模式切換按鈕被點擊時，會呼叫這個函式
    public void ToggleGameMode()
    {
        currentMode = 1 - currentMode; // 在 0 和 1 之間切換
        UpdateModeButtonText(); // 更新按鈕上的文字顯示
    }

    // 更新按鈕上顯示的文字
    public void UpdateModeButtonText()
    {
        if (modeToggleButtonText != null)
        {
            modeToggleButtonText.text = "MODE " + (currentMode + 1);
        }
    }
}