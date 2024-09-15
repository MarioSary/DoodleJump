using System;
using UnityEngine;

public class PLayer : MonoBehaviour
{
    //Bounds
    private Vector3 _screenBounds;
    private float _objectWidth = 0.5f;
    private float _objectHeight = 1f;
    
    //Movements
    [SerializeField] private float speed;
    private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D doodlerCollider;
    
    //Score system
    private int _score = 0;
    private int _highScore = 0;
    
    //Change sprites
    private bool _rightDir = true;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] doodlerSprites = new Sprite[3];
    
    //Shoot
    [SerializeField] private GameObject doodlerShooter;

    // Audios
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathAudio;

    // Game over
    private bool _isGameOver = false;
    private bool _isHit = false;
    private float _gameOverDistance;


    private void Start()
    {
        _screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        _gameOverDistance = Camera.main.transform.position.y + (_screenBounds.y + _objectHeight); // is 6f
        //Debug.Log("Screen Y is :" + _screenBounds.y); // is 5f
        //Debug.Log("_gameOverDistance is :" + _gameOverDistance); // is 5f
        
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            spriteRenderer.sprite = doodlerSprites[2];
            doodlerShooter.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            doodlerShooter.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        PlayerMovement();

        //for game over
        if (transform.position.y < Camera.main.transform.position.y - _gameOverDistance && !_isGameOver)
        {
            Debug.Log("GameOver");
            audioSource.clip = deathAudio;
            audioSource.Play();
            transform.position = new Vector3(transform.position.x, transform.position.y + (_gameOverDistance * 2), 0);
            doodlerCollider.enabled = false;
            _isGameOver = true;
            GameManager.Instance.OnGameOver();
            CameraMovement.Instance.OnGameOver();
            LevelGenerator.Instance.OnGameOver();
            UiManager.Instance.OnGameOver();
        }
    }

    void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 direction = _rb.velocity;
        direction.x = horizontalInput * speed * Time.deltaTime;
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
        if (direction.y > 0)
        {
            spriteRenderer.sprite = doodlerSprites[0];
            doodlerCollider.enabled = false;
        }

        if (direction.y < 0)
        {
            spriteRenderer.sprite = doodlerSprites[1];
            if (!_isGameOver && !_isHit)
            {
                doodlerCollider.enabled = true; 
            }
            JetFall();
        }

        if (transform.position.x > _screenBounds.x + _objectWidth || transform.position.x < -_screenBounds.x - _objectWidth)
        {
            Debug.Log("right");
            Vector3 viewPos = transform.position;
            viewPos.x = transform.position.x * -1;
            transform.position = viewPos;
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
        if (transform.childCount > 2)
        {
            transform.GetChild(2).GetComponent<Jet>().SetFall();
            transform.GetChild(2).parent = null;
        }
    }

    public void AddScore(int points)
    {
        _score = points;
        UiManager.Instance.UpdateScore(_score);
        if (_highScore < _score)
        {
            UiManager.Instance.UpdateHighScore(_score);
        }
        PlayerPrefs.Save();
    }

    public void OnHit()
    {
        _isHit = true;
    }
}
