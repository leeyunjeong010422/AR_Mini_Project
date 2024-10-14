using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Michsky.UI.ModernUIPack;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int score = 0;
    private int bestScore = 0;
    private float timeRemaining = 60f;
    private bool isStopped = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    private void Start()
    {
        SoundManager.Instance.SetStartBGM();
        StartCoroutine(GameTimer());
    }

    public void AddScore(int points)
    {
        score += points;
        UIManager.instance.UpdateScoreText(score);  //���� ����
    }

    private IEnumerator GameTimer()
    {
        while (timeRemaining >= 0)
        {
            UIManager.instance.UpdateTimerText(timeRemaining);  //���� �ð� ����
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        EndGame();
    }

    private void EndGame()
    {
        SoundManager.Instance.StopBGM();
        UIManager.instance.ShowGameOverUI();  //���� ���� ǥ��
        Time.timeScale = 0f;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            UIManager.instance.UpdateBestScoreText(bestScore);  //�ְ� ���� ����
        }
    }

    public void RestartGame()
    {
        SoundManager.Instance.PlayMouseClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        SoundManager.Instance.PlayMouseClickSound();
        Application.Quit();
    }

    public void ToggleGame()
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

    public void StopGame()
    {
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayMouseClickSound();
        Time.timeScale = 0f;
        isStopped = true;
        UIManager.instance.SetGamePaused(true);  //���� �Ͻ����� ���� ǥ��
    }

    public void StartGame()
    {
        SoundManager.Instance.SetStartBGM();
        SoundManager.Instance.PlayMouseClickSound();
        Time.timeScale = 1f;
        isStopped = false;
        UIManager.instance.SetGamePaused(false);  //���� �簳 ���� ǥ��
    }
}
