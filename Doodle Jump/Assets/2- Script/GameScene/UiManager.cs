using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    #region Singleton
    public static UiManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion
    
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _menuScoreText;
    [SerializeField] private Text _highScreText;
    
    [SerializeField] private GameObject _gameOverPanel;
    private void Start()
    {
        _scoreText.text = String.Format("0");
        _highScreText.text = String.Format("0");

    }
    public void UpdateScore(int score)
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
        _scoreText.text = score.ToString();
    }

    public void UpdateHighScore(int highScore)
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void OnPause()
    {
        Time.timeScale = 0;
    }

    public void OnResume()
    {
        Time.timeScale = 1;
    }

    public void OnGameOver()
    {
        _gameOverPanel.SetActive(true);
        _highScreText.text = "Your High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        _menuScoreText.text = "Your Score: " + PlayerPrefs.GetInt("Score", 0).ToString();

    }
}
