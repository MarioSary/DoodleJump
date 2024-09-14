using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    [SerializeField] private GameObject doodler;
    private bool _gameOver;
    
    private void Update()
    {
        if (_gameOver)
        {
            //CameraMovement.Instance.OnDeath();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnGameOver()
    {
        _gameOver = true;
        Destroy(doodler.gameObject, 5);
    }
}
