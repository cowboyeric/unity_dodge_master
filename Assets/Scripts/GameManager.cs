using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // --- UI 相關 ---
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;

    // --- 遊戲邏輯變數 ---
    private int score = 0; // 分數從 0 開始
    // private bool isGameOver = false; // 我們暫時不需要這個了

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Application.targetFrameRate = 60;
    }

    // 我們在遊戲開始時就初始化一次分數顯示
    void Start()
    {
        UpdateScoreText();
    }

    // Update 函式現在是空的，因為我們不再按時間計分
    void Update()
    {
        // if (isGameOver)
        // {
        //     return;
        // }
        // score = (int)(Time.timeSinceLevelLoad * 10f);
        // scoreText.text = "Score: " + score;
    }

    // --- 新的加分函式 ---
    // 這是一個公開函式，可以被任何其他腳本呼叫
    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd; // 將傳入的分數加到總分上
        UpdateScoreText();    // 更新畫面上的分數顯示
    }

    // 將更新文字的邏輯獨立成一個函式，方便重複使用
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    // GameOver 函式保持不變，以防未來我們需要加回結束條件
    public void GameOver()
    {
        // isGameOver = true;
        Time.timeScale = 0f;
        scoreText.gameObject.SetActive(false);
        finalScoreText.text = "Score: " + score;
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}