using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _doodler;

    [Header("Instantiate This Platforms")] 
    [SerializeField] GameObject _greenPlatformPrefab;

    [SerializeField] GameObject _bluePlatformPrefab;
    [SerializeField] GameObject _brownPlatformPrefab;
    [SerializeField] GameObject _whitePlatformPrefab;

    [Header("Platforms' Extensions")] 
    [SerializeField] GameObject Spring;
    [SerializeField] GameObject Trampoline;
    [SerializeField] GameObject Propeller;

    // for spawn variation
    private int _playerheight;

    // bound platform spawn
    private Vector3 _screenBounds;
    float offset = 0.6f; // half of the platform width
    Vector3 spawnPos = new Vector3();
    private float _posY = -4f;
    private float lastPlatformY;
    private GameObject _firstPlatform;
    
    //to destroy platforms
    private List<GameObject> _greenPlatforms = new List<GameObject>();
    private List<GameObject> _bluePlatforms = new List<GameObject>();


    private void Start()
    {
        Debug.Log("Screen size is" + Screen.width + "And Screen Bound is" + _screenBounds.x);
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
            Camera.main.transform.position.z));
        
        _firstPlatform = Instantiate(_greenPlatformPrefab, new Vector3(0, (_screenBounds.y * -1) - 2, 0),Quaternion.identity);
        _doodler.transform.position = new Vector3(0, _firstPlatform.transform.position.y + 1f, 0);
        Destroy(_firstPlatform, 2f);
    }

    private void Update()
    {
        _playerheight = (int) _doodler.transform.position.y;
        Debug.Log(_playerheight);
        if (_greenPlatforms.Count < 20)
        {
            switch (_playerheight)
            {
                case < 50:
                    GeneratePlatform(10);
                    GenerateBrown(2);
                    break;
                
                case >= 50:
                    GeneratePlatform(5);
                    GenerateBrown(3);
                    break;
            }
            
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
        for (int i = 0; i < _bluePlatforms.Count; i++)
        {
            if (_bluePlatforms[i] != null)
            {
                if (_bluePlatforms[i].transform.position.y < Camera.main.transform.position.y - _screenBounds.y)
                {
                    Destroy(_bluePlatforms[i]);
                    _bluePlatforms.Remove(_greenPlatforms[i]);
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
            _greenPlatforms.Add(newPlatform);
            
            float lastPlatformY = newPlatform.transform.position.y;
            
            if (_playerheight <= 20)
            {
                _posY += 0.6f;
            }

            if (_playerheight > 20 && _playerheight <= 40)
            {
                _posY += Random.Range(0.2f, 3f);
                if (_posY > lastPlatformY + 0.5f && _posY <= lastPlatformY + 2f)
                {
                    GenerateBrown(2);
                }
            }

            if (_playerheight > 40 && _playerheight <= 80)
            {
                _posY += Random.Range(0.2f, 4f);
                if (_posY > lastPlatformY + 1f && _posY <= lastPlatformY + 2f)
                {
                    GenerateBrown(1);
                }
                if (_posY > lastPlatformY + 2f && _posY <= lastPlatformY + 3f)
                {
                    GenerateWhite(Random.Range(2,3));
                }
            }

            if (_playerheight > 80)
            {
                _posY += Random.Range(0.2f, 5f);
                if (_posY > lastPlatformY + 1f && _posY <= lastPlatformY + 2f)
                {
                    GenerateBrown(1);
                }
                if (_posY > lastPlatformY + 2f && _posY <= lastPlatformY + 3f)
                {
                    GenerateWhite(Random.Range(2,3));
                }

                if (_posY > lastPlatformY + 3f && _posY <= lastPlatformY + 4f)
                {
                    GenerateBlue(Random.Range(2, 4));
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

            Instantiate(_brownPlatformPrefab, spawnPos, Quaternion.identity);
            _posY += Random.Range(0.4f, 0.8f);
        }
    }

    public void GenerateWhite(int platformCount)
    {
        for (int i = 0; i < platformCount; i++)
        {
            spawnPos.x = Random.Range(_screenBounds.x * -1 + offset, _screenBounds.x - offset);
            spawnPos = new Vector3(spawnPos.x, _posY, 0);

            Instantiate(_whitePlatformPrefab, spawnPos, Quaternion.identity);
            _posY += Random.Range(0.4f, 0.8f);
        }
    }
    public void GenerateBlue(int platformCount)
    {
        for (int i = 0; i < platformCount; i++)
        {
            spawnPos.x = Random.Range(_screenBounds.x * -1 + offset, _screenBounds.x - offset);
            spawnPos = new Vector3(spawnPos.x, _posY, 0);

            GameObject newBrownPlatform = Instantiate(_bluePlatformPrefab, spawnPos, Quaternion.identity);
            _bluePlatforms.Add(newBrownPlatform);
            _posY += Random.Range(0.6f, 1f);
        }
    }
}

