using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton for easy access

    private int currentScore = 0;
    private int highScore = 0;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Load high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        UpdateUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;

        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + currentScore;

        if (highScoreText != null)
            highScoreText.text = "Highest Score: " + highScore;
    }

    public int GetCurrentScore() => currentScore;
    public int GetHighScore() => highScore;
}
