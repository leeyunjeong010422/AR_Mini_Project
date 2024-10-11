using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] Button stopButton;

    private int score = 0;
    private int bestScore = 0;
    private float timeRemaining = 10f;

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
        stopButton.onClick.AddListener(StopGame);

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
            PlayerPrefs.SetInt("BestScore", bestScore); //새로운 최고 기록 저장
            bestScoreText.text = "Best Score: " + bestScore.ToString(); //최고 점수 텍스트 갱신
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

    private void StopGame()
    {
        if (stopGameUI.activeSelf)
        {
            stopGameUI.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            stopGameUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        stopGameUI.SetActive(false);
    }
}
