using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer : MonoBehaviour
{
    private Vector3 _screenBounds;
    private float _objectWidth = 0.5f;
    [SerializeField] private float _speed;
    private Rigidbody2D _rb;
    private int _score = 0;
    private int _highScore = 0;
    private bool _rightDir = true;


    private void Start()
    {
        _screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }
    private void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 direction = _rb.velocity;
        direction.x = horizontalInput * _speed;
        _rb.velocity = direction;
        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        if (horizontalInput > 0 && !_rightDir)
        {
            Flip();
        }
        if (horizontalInput < 0 && _rightDir)
        {
            Flip();
        }

        if (transform.position.x > _screenBounds.x + _objectWidth || transform.position.x < -_screenBounds.x - _objectWidth)
        {
            Debug.Log("right");
            Vector3 viewPos = transform.position;
            viewPos.x = transform.position.x * -1;
            transform.position = viewPos;
        }

        if (direction.y < 0)
        {
            JetFall();
        }
    }

    void Flip()
    {
        Vector3 _playerLocalscale = gameObject.transform.localScale;
        _playerLocalscale.x *= -1;
        gameObject.transform.localScale = _playerLocalscale;
        _rightDir = !_rightDir;
    }

    void JetFall()
    {
        if (transform.childCount > 1)
        {
            transform.GetChild(1).GetComponent<Jet>().SetFall();
            transform.GetChild(1).parent = null;
        }
    }

    public void AddScore(int points)
    {
        _score = points;
        UiManager.Instance.UpdateScore(_score);
        PlayerPrefs.SetInt("Score", _score);
        if (_highScore < _score)
        {
            UiManager.Instance.UpdateHighScore(_score);
        }
        PlayerPrefs.Save();
    }
}
