using UnityEngine;

public class PLayer : MonoBehaviour
{
    private Vector3 _screenBounds;
    private float _objectWidth = 0.5f;
    private float _objectHeight = 1f;
    
    [SerializeField] private float speed;
    private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D doodlerCollider;
    
    private int _score = 0;
    private int _highScore = 0;
    
    private bool _rightDir = true;
    [SerializeField] private SpriteRenderer doodlerSprite;
    [SerializeField] Sprite[] playerJumpSprite = new Sprite[2];

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip deathAudio;

    private bool _isGameOver = false;
    private float _gameOverDistance;


    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        _screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        _gameOverDistance = Camera.main.transform.position.y + (_screenBounds.y + _objectHeight); // is 6f
        Debug.Log("Screen Y is :" + _screenBounds.y); // is 5f
        Debug.Log("_gameOverDistance is :" + _gameOverDistance); // is 5f
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
            doodlerSprite.sprite = playerJumpSprite[0];
            doodlerCollider.enabled = false;
        }

        if (direction.y < 0)
        {
            doodlerSprite.sprite = playerJumpSprite[1];
            if (!_isGameOver)
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
        if (_highScore < _score)
        {
            UiManager.Instance.UpdateHighScore(_score);
        }
        PlayerPrefs.Save();
    }
}
