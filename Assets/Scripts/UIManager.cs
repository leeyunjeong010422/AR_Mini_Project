using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI ammoText;

    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject stopGameUI;

    [SerializeField] Button backButton;
    [SerializeField] Button retryButton;
    [SerializeField] Button retry1Button;
    [SerializeField] Button exitButton;
    [SerializeField] Button exit1Button;
    [SerializeField] Button toggleButton;

    [SerializeField] Sprite stopSprite;
    [SerializeField] Sprite startSprite;
    [SerializeField] Image buttonImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        backButton.onClick.AddListener(() => GameManager.instance.StartGame());
        retryButton.onClick.AddListener(() => GameManager.instance.RestartGame());
        retry1Button.onClick.AddListener(() => GameManager.instance.RestartGame());
        exitButton.onClick.AddListener(() => GameManager.instance.ExitGame());
        exit1Button.onClick.AddListener(() => GameManager.instance.ExitGame());
        toggleButton.onClick.AddListener(() => GameManager.instance.ToggleGame());

        gameOverUI.SetActive(false);
        stopGameUI.SetActive(false);

        //초기 최고 점수 표시
        bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("BestScore", 0).ToString();
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateTimerText(float timeRemaining)
    {
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
    }

    public void UpdateBestScoreText(int bestScore)
    {
        bestScoreText.text = "Best Score: " + bestScore.ToString();
    }

    public void ShowGameOverUI()
    {
        SoundManager.Instance.PlayGameOverSound();
        gameOverUI.SetActive(true);
    }

    public void SetGamePaused(bool isPaused)
    {
        stopGameUI.SetActive(isPaused);
        buttonImage.sprite = isPaused ? startSprite : stopSprite;
    }

    public void UpdateAmmoText(int currentAmmo, int maxAmmo)
    {
        ammoText.text = currentAmmo + " / " + maxAmmo;
    }
}
