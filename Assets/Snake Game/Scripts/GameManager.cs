using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState currentState;

    [Header("UI Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= GameOver;
    }

    private void Start()
    {
        SetState(GameState.Start);
    }

    private void Update()
    {
        if (currentState == GameState.Playing && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        SetState(GameState.Playing);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        SetState(GameState.Paused);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        SetState(GameState.Playing);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        SetState(GameState.GameOver);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SetState(GameState.Playing);

        // Reload scene cleanly
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Helps verify button is working

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void SetState(GameState newState)
    {
        currentState = newState;

        startPanel.SetActive(newState == GameState.Start);
        gamePanel.SetActive(newState == GameState.Playing);
        pausePanel.SetActive(newState == GameState.Paused);
        gameOverPanel.SetActive(newState == GameState.GameOver);
    }
}