using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("HUD")]
    public Text hpText;
    public Text scoreText;

    [Header("Buttons")]
    public Button quitButton;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public Button restartButton;
    public Button menuButton;

    private void Start()
    {
        // Optional auto-wiring by names (helps avoid manual mistakes)
        if (hpText == null)
        {
            var go = GameObject.Find("HPText");
            if (go != null) hpText = go.GetComponent<Text>();
        }
        if (scoreText == null)
        {
            var go = GameObject.Find("ScoreText");
            if (go != null) scoreText = go.GetComponent<Text>();
        }
        if (gameOverPanel == null)
        {
            var go = GameObject.Find("GameOverPanel");
            if (go != null) gameOverPanel = go;
        }

        if (quitButton != null) quitButton.onClick.AddListener(Quit);
        if (restartButton != null) restartButton.onClick.AddListener(Restart);
        if (menuButton != null) menuButton.onClick.AddListener(BackToMenu);

        ShowGameOver(false);
    }

    private void Update()
    {
        // Keep a keyboard fallback (still keep the Quit button visible)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void SetHP(int hp)
    {
        if (hpText != null) hpText.text = "HP: " + hp.ToString();
    }

    public void SetScore(int score)
    {
        if (scoreText != null) scoreText.text = "Score: " + score.ToString();
    }

    public void ShowGameOver(bool show)
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(show);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
