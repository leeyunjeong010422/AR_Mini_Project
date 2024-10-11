using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] Button retryButton;
    [SerializeField] Button retry1Button;
    [SerializeField] Button exitButton;
    [SerializeField] Button exit1Button;
    [SerializeField] Button backButton;
    [SerializeField] GameObject stopGameUI;
    
    [SerializeField] Button toggleButton;
    [SerializeField] Sprite stopSprite;
    [SerializeField] Sprite startSprite;
    [SerializeField] Image buttonImage;

    private int score = 0;
    private int bestScore = 0;
    private float timeRemaining = 10f;
    private bool isStopped = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        gameOverUI.SetActive(false);
        stopGameUI.SetActive(false);

        retryButton.onClick.AddListener(RestartGame);
        retry1Button.onClick.AddListener(RestartGame);
        exitButton.onClick.AddListener(ExitGame);
        exit1Button.onClick.AddListener(ExitGame);
        backButton.onClick.AddListener(StartGame);
        toggleButton.onClick.AddListener(ToggleGame);

        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best Score: " + bestScore.ToString();
    }

    private void Start()
    {
        StartCoroutine(GameTimer());
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString();
    }

    private IEnumerator GameTimer()
    {
        while (timeRemaining >= 0)
        {
            timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        EndGame();
    }

    private void EndGame()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            bestScoreText.text = "Best Score: " + bestScore.ToString();
        }
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void ToggleGame()
    {
        if (isStopped)
        {
            StartGame();
        }
        else
        {
            StopGame();
        }
    }

    private void StopGame()
    {
        stopGameUI.SetActive(true);
        Time.timeScale = 0f;
        isStopped = true;
        buttonImage.sprite = startSprite;
    }

    private void StartGame()
    {
        stopGameUI.SetActive(false);
        Time.timeScale = 1f;
        isStopped = false;
        buttonImage.sprite = stopSprite;
    }
}
