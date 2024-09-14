using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private Text _highScreText;
    [SerializeField] private GameObject _gameOverPanel;
    private void Start()
    {
        _scoreText.text = String.Format("0");
        _highScreText.text = String.Format("0");
    }
    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void UpdateHighScore(int highScore)
    {
        PlayerPrefs.SetInt("highscore", highScore);
    }
}
