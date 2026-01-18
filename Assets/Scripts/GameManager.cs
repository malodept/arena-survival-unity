using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game")]
    public int startingHP = 3;
    public int score = 0;

    [Header("References")]
    public UIManager uiManager;

    private int _hp;
    private bool _gameOver;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Keep it simple: no DontDestroyOnLoad needed.
    }

    private void Start()
    {
        _hp = Mathf.Max(1, startingHP);
        _gameOver = false;

        if (uiManager == null)
        {
            uiManager = FindFirstObjectByType<UIManager>();
        }

        UpdateUI();
    }

    public bool IsGameOver()
    {
        return _gameOver;
    }

    public void AddScore(int amount)
    {
        if (_gameOver) return;
        score += Mathf.Max(0, amount);
        UpdateUI();
    }

    public void DamagePlayer(int amount)
    {
        if (_gameOver) return;

        _hp -= Mathf.Max(0, amount);
        _hp = Mathf.Max(0, _hp);
        UpdateUI();

        if (_hp <= 0)
        {
            _gameOver = true;
            if (uiManager != null)
            {
                uiManager.ShowGameOver(true);
            }
        }
    }

    public int GetHP()
    {
        return _hp;
    }

    private void UpdateUI()
    {
        if (uiManager != null)
        {
            uiManager.SetHP(_hp);
            uiManager.SetScore(score);
        }
    }
}
