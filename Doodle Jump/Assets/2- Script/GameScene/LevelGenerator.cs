using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    #region ObjectPooling

    /*private PLayer _pLayer;

    private Vector3 _screenBounds;
    float offset = 0.6f; // half of the platform width
    Vector3 spawnPos = new Vector3();
    private float _posY = -4f;
    
    private int _playerMaxHeight = 0;

    //object pooling
    private List<GameObject> deactivateList;



    private void Start()
    {
        _pLayer = GameObject.Find("Player").GetComponent<PLayer>();
        if (_pLayer == null)
        {
            Debug.LogError("Player is NULL.");
        }
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
            Camera.main.transform.position.z));
    }

    private void Update()
    {
        //calculate the score just by going up
         if ((int)_pLayer.transform.position.y > _playerMaxHeight)
         {
             _playerMaxHeight =  (int) _pLayer.transform.position.y;
         }
         _pLayer.AddScore(_playerMaxHeight);
    }

    private void FixedUpdate()
    {
        while (ObjectPooler.Instance.poolObjects.Count > 0)
        {
            switch (_playerMaxHeight)
            {
            case var i when i < 50:
                    GeneratePlatform(PoolObjectType.Green);
                    GeneratePlatform(PoolObjectType.Brown);
                    break;
            case var i when i >=50 && i <= 100:
                    GeneratePlatform(PoolObjectType.Green);
                    GeneratePlatform(PoolObjectType.Brown);
                    GeneratePlatform(PoolObjectType.Blue);
                    break;
            case var i when i >=100 && i <= 200:
                    GeneratePlatform(PoolObjectType.Green);
                    GeneratePlatform(PoolObjectType.Brown);
                    GeneratePlatform(PoolObjectType.Blue);
                    GeneratePlatform(PoolObjectType.White);
                    break;
            }
        }
    }

    void GeneratePlatform(PoolObjectType type)
    {
        spawnPos.x = Random.Range(_screenBounds.x * -1 + offset, _screenBounds.x - offset);
        spawnPos = new Vector3(spawnPos.x, _posY, 0);
        GameObject newPlatform = ObjectPooler.Instance.SpawnFromPool(type, spawnPos, Quaternion.identity);
        _posY += Random.Range(0.6f, 1f);
    }

    Vector3 SpawnPos(float yPos)
    {
        spawnPos.x = Random.Range(_screenBounds.x * -1 + offset, _screenBounds.x - offset);
        spawnPos = new Vector3(spawnPos.x, _posY, 0);
        _posY += yPos;
        return spawnPos;
    }*/

    #endregion
    
    
    #region MyLowPerformanceCode

    #region Singleton

    public static LevelGenerator Instance;
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
    
    
    private PLayer _pLayer;
    [Header("Instantiate This Platforms")] 
    [SerializeField] GameObject _greenPlatformPrefab;
    [SerializeField] GameObject _bluePlatformPrefab;
    [SerializeField] GameObject _brownPlatformPrefab;
    [SerializeField] GameObject _whitePlatformPrefab;
    [SerializeField] GameObject[] powerUpsPrefab = new GameObject[4];
    [SerializeField] GameObject[] enemyPrefab = new GameObject[3];

    // for spawn variation
    private int _playerheight;

    // bound platform spawn
    private Vector3 _screenBounds;
    float offset = 0.6f; // half of the platform width
    Vector3 spawnPos = new Vector3();
    private float _posY = -4f;
    private int _playerMaxHeight = 0;

    //to destroy platforms
    private List<GameObject> _greenPlatforms = new List<GameObject>();
    private List<GameObject> _otherPlatforms = new List<GameObject>();

    private bool _isDied = false;

    

    private void Start()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
            Camera.main.transform.position.z));
        _pLayer = GameObject.Find("Player").GetComponent<PLayer>();
        if (_pLayer == null)
        {
            Debug.LogError("Player is NULL.");
        }
    }

    private void Update()
    {
        if (!_isDied)
        {
            _playerheight = (int) _pLayer.transform.position.y;
            if (_greenPlatforms.Count < 20)
            {
                switch (_playerheight)
                {
                    case int i when i < 50:
                        GeneratePlatform(20);
                        GenerateBrown(2);
                        break;
                    case int i when i >=50 && i <= 100:
                        GeneratePlatform(15);
                        GenerateBrown(3);
                        break;
                    case int i when i >=100 && i <= 200:
                        GeneratePlatform(10);
                        GenerateBrown(2);
                        break;
                }
            }
            //calculate the score just by going up
            if ((int)_pLayer.transform.position.y > _playerMaxHeight)
            {
                _playerMaxHeight =  (int) _pLayer.transform.position.y;
            }
            _pLayer.AddScore(_playerMaxHeight);
        }
        
        for (int i = 0; i < _greenPlatforms.Count; i++)
        {
            if (_greenPlatforms[i] != null)
            {
                if (_greenPlatforms[i].transform.position.y < Camera.main.transform.position.y - _screenBounds.y)
                {
                    Destroy(_greenPlatforms[i]);
                    _greenPlatforms.Remove(_greenPlatforms[i]);
                }
            }
        }
        for (int i = 0; i < _otherPlatforms.Count; i++)
        {
            if (_otherPlatforms[i] != null)
            {
                if (_otherPlatforms[i].transform.position.y < Camera.main.transform.position.y - _screenBounds.y)
                {
                    Destroy(_otherPlatforms[i]);
                    _otherPlatforms.Remove(_otherPlatforms[i]);
                }
            }
        }
    }

    public void GeneratePlatform(int platformCount)
    {
        for (int i = 0; i < platformCount; i++)
        {
            spawnPos.x = Random.Range(_screenBounds.x * -1 + offset, _screenBounds.x - offset);
            spawnPos = new Vector3(spawnPos.x, _posY, 0);
            
            GameObject newPlatform = Instantiate(_greenPlatformPrefab, spawnPos, Quaternion.identity);
            newPlatform.transform.parent = transform.GetChild(0);
            _greenPlatforms.Add(newPlatform);
            
            float lastPlatformY = newPlatform.transform.position.y;
            
            if (_playerheight <= 20)
            {
                _posY += 0.6f;
                //GeneratePowerUps(1);
            }

            if (_playerheight > 20 && _playerheight <= 40)
            {
                _posY += Random.Range(0.2f, 2f);
                if (_posY > lastPlatformY + 0.5f && _posY <= lastPlatformY + 2f)
                {
                    GenerateBrown(2);
                    GeneratePowerUps(1);
                }
            }

            if (_playerheight > 40 && _playerheight <= 80)
            {
                _posY += Random.Range(0.2f, 3f);
                if (_posY > lastPlatformY + 1f && _posY <= lastPlatformY + 2f)
                {
                    GenerateBrown(1);
                }
                if (_posY > lastPlatformY + 2f && _posY <= lastPlatformY + 3f)
                {
                    GenerateWhite(Random.Range(2,3));
                    GeneratePowerUps(1);
                }
            }

            if (_playerheight > 80)
            {
                _posY += Random.Range(0.2f, 4f);
                if (_posY > lastPlatformY + 1f && _posY <= lastPlatformY + 2f)
                {
                    GenerateBrown(1);
                }
                if (_posY > lastPlatformY + 2f && _posY <= lastPlatformY + 3f)
                {
                    GenerateWhite(Random.Range(1,3));
                }

                if (_posY > lastPlatformY + 3f && _posY <= lastPlatformY + 4f)
                {
                    GenerateBlue(Random.Range(1, 3));
                    GeneratePowerUps(1);
                    GenerateEnemy(1);
                }
            }
        }
    }

    public void GenerateBrown(int platformCount)
    {
        for (int i = 0; i < platformCount; i++)
        {
            spawnPos.x = Random.Range(_screenBounds.x * -1 + offset, _screenBounds.x - offset);
            spawnPos = new Vector3(spawnPos.x, _posY, 0);

            GameObject newBrownPlatform = Instantiate(_brownPlatformPrefab, spawnPos, Quaternion.identity);
            newBrownPlatform.transform.parent = transform.GetChild(1);
            _otherPlatforms.Add(newBrownPlatform);
            _posY += Random.Range(0.4f, 0.8f);
        }
    }

    public void GenerateWhite(int platformCount)
    {
        for (int i = 0; i < platformCount; i++)
        {
            spawnPos.x = Random.Range(_screenBounds.x * -1 + offset, _screenBounds.x - offset);
            spawnPos = new Vector3(spawnPos.x, _posY, 0);

            GameObject newWhitePlatform = Instantiate(_whitePlatformPrefab, spawnPos, Quaternion.identity);
            newWhitePlatform.transform.parent = transform.GetChild(2);
            _otherPlatforms.Add(newWhitePlatform);
            _posY += Random.Range(0.4f, 0.8f);
        }
    }
    public void GenerateBlue(int platformCount)
    {
        for (int i = 0; i < platformCount; i++)
        {
            spawnPos.x = Random.Range(_screenBounds.x * -1 + offset, _screenBounds.x - offset);
            spawnPos = new Vector3(spawnPos.x, _posY, 0);

            GameObject newBluePlatform = Instantiate(_bluePlatformPrefab, spawnPos, Quaternion.identity);
            newBluePlatform.transform.parent = transform.GetChild(3);
            _otherPlatforms.Add(newBluePlatform);
            _posY += Random.Range(0.6f, 1f);
        }
    }

    public void GeneratePowerUps(int powerUpCount)
    {
        for (int i = 0; i < powerUpCount; i++)
        {
            spawnPos.x = Random.Range(_screenBounds.x * -1 + offset, _screenBounds.x - offset);
            spawnPos = new Vector3(spawnPos.x, _posY, 0);

            GameObject newPowerUp = Instantiate(powerUpsPrefab[Random.Range(0,4)], spawnPos, Quaternion.identity);
            newPowerUp.transform.parent = transform.GetChild(4);
            _otherPlatforms.Add(newPowerUp);
            _posY += Random.Range(2, 4);
        }
    }

    public void GenerateEnemy(int enemyCount)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            spawnPos.x = Random.Range((_screenBounds.x * -1) + 1, _screenBounds.x - 1);
            spawnPos = new Vector3(spawnPos.x, _posY, 0);

            GameObject newEnemy = Instantiate(enemyPrefab[Random.Range(0,3)], spawnPos, Quaternion.identity);
            newEnemy.transform.parent = transform.GetChild(5);
            _otherPlatforms.Add(newEnemy);
            _posY += Random.Range(3, 4);
        }
    }

    public void OnGameOver()
    {
        _isDied = true;
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    #endregion

    
    
}

