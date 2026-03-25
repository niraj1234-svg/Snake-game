using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private int score;
    private int highScore;

    private const string HIGH_SCORE_KEY = "HighScore";

    private void OnEnable()
    {
        GameEvents.OnFoodCollected += AddScore;
        GameEvents.OnGameOver += SaveHighScore;
    }

    private void OnDisable()
    {
        GameEvents.OnFoodCollected -= AddScore;
        GameEvents.OnGameOver -= SaveHighScore;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        UpdateUI();
    }

    private void AddScore()
    {
        score++;

        if (score > highScore)
            highScore = score;

        UpdateUI();
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"Score : {score}";
        highScoreText.text = $"High Score : {highScore}";
    }
}