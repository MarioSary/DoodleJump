using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _doodler;

    [Header("Instantiate This Platforms")] [SerializeField]
    GameObject _greenPlatformPrefab;

    [SerializeField] GameObject _bluePlatformPrefab;
    [SerializeField] GameObject _brownPlatformPrefab;
    [SerializeField] GameObject _whitePlatformPrefab;

    [Header("Platforms' Extensions")] [SerializeField]
    GameObject Spring;

    [SerializeField] GameObject Trampoline;
    [SerializeField] GameObject Propeller;

    // for spawn variation
    private int _playerheight;

    // bound platform spawn
    private Vector3 _screenBounds;
    float offset = 0.6f; // half of the platform width
    Vector3 spawnPos = new Vector3();



    // prevent overlapping //to destroy platforms
    private List<GameObject> _platforms = new List<GameObject>();
    private GameObject _lastPlatform;
    private int _platformIndex;

    private void Start()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
            Camera.main.transform.position.z));
        GameObject firstPlatform = Instantiate(_greenPlatformPrefab, new Vector3(0, (_screenBounds.y * -1) - 2, 0),
            quaternion.identity);
        _doodler.transform.position = new Vector3(0, firstPlatform.transform.position.y + 1f, 0);
        Destroy(firstPlatform, 3f);

    }

    private void Update()
    {
        
        _playerheight = (int) _doodler.transform.position.y;
        if (_platforms.Count < 20)
        {
            switch (_playerheight)
            {
                case < 20:
                    GeneratePlatform(20);
                    break;
                case > 20:
                    GeneratePlatform(20);
                    GenerateBrown(5);
                    break;
            }
        }

        for (int i = 0; i < _platforms.Count; i++)
        {
            if (_platforms[i] != null)
            {
                if (_platforms[i].transform.position.y < Camera.main.transform.position.y - _screenBounds.y)
                {
                    Destroy(_platforms[i]);
                    _platforms.Remove(_platforms[i]);
                }
            }

        }

    }

    public void GeneratePlatform(int platformCount)
    {
        List<Vector3> platformPositions = new List<Vector3>();

        for (int i = 0; i < platformCount; i++)
        {
            spawnPos.x = Random.Range(_screenBounds.x * -1 + offset, _screenBounds.x - offset);
            if (Time.time <= 0.5)
            {
                spawnPos.y = Random.Range(_screenBounds.y * -1 + offset, _screenBounds.y + 2f);
            }
            else if (Time.time > 0.5)
            {
                spawnPos.y = Random.Range(_doodler.transform.position.y + _screenBounds.y,
                    _doodler.transform.position.y + _screenBounds.y + (Screen.height / 100f));
            }
            spawnPos = new Vector3(spawnPos.x, spawnPos.y, 0);

            GameObject newPlatform = Instantiate(_greenPlatformPrefab, spawnPos, Quaternion.identity);
            _platforms.Add(newPlatform);
            Debug.Log(_platforms.IndexOf(newPlatform));

            foreach (Vector3 platformPosition in platformPositions)
            {
                if (Vector3.Distance(platformPosition, spawnPos) < 3f) // Adjust overlap distance as needed
                {
                    return;
                }
                else
                {
                    platformPositions.Add(spawnPos);
                }
            }
        }
    }

    public void GenerateBrown(int platformCount)
    {
        for (int i = 0; i < platformCount; i++)
        {
            spawnPos.x = Random.Range(_screenBounds.x * -1 + offset, _screenBounds.x - offset);
            if (Time.time <= 0.5)
            {
                spawnPos.y = Random.Range(_screenBounds.y * -1 + offset, _screenBounds.y);
            }
            else if (Time.time > 0.5)
            {
                spawnPos.y = Random.Range(_doodler.transform.position.y + _screenBounds.y,
                    _doodler.transform.position.y + _screenBounds.y + (Screen.height / 100f));
            }

            spawnPos = new Vector3(spawnPos.x, spawnPos.y, 0);

            GameObject newBrownPlatform = Instantiate(_brownPlatformPrefab, spawnPos, Quaternion.identity);
            _platforms.Add(newBrownPlatform);
        }
    }
}

/*private bool PrevendOverlap(Vector3 spawnPos)
{
    platformColliders = Physics2D.OverlapCircleAll(transform.position, PlatformRadius);
    
    for (int i = 0; i < platformColliders.Length; i++)
    {
        Vector3 centerPoint = platformColliders[i].bounds.center;
        float width = platformColliders[i].bounds.extents.x;
        float height = platformColliders[i].bounds.extents.y;

        float leftExtent = centerPoint.x - width;
        float rightExtent = centerPoint.x + width;
        float upperExtent = centerPoint.y + height;
        float lowerExtent = centerPoint.y - height;

        if (spawnPos.x > leftExtent && spawnPos.x <= rightExtent)
        {
            if (spawnPos.y > lowerExtent && spawnPos.y <= upperExtent)
            {
                return false;
            }
        }
    }

    return true;
}

protected void OnDrawGizmos()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, PlatformRadius);
} */

