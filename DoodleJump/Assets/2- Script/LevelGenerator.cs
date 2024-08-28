using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    private PLayer player;
    private ScreenBoundries _screenBoundries = new ScreenBoundries();
    
    private Vector3 screenBounds;
    
    [SerializeField] private GameObject _platformPrefab;
    public int _platformCount = 100;
    private float _yHeight;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PLayer>();
        if (player == null)
        {
            Debug.LogError("Player is NULL.");
        }
        _screenBoundries.ScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
            Camera.main.transform.position.z));
        Vector3 bounds = _screenBoundries.ScreenBounds;

        // _screenBoundries = GameObject.Find("Player").GetComponent<ScreenBoundries>();
        // if (_screenBoundries == null)
        // {
        //     Debug.LogError("Screen Bounds is NULL.");
        // }
        
        float offset = 1.2f;
        _yHeight = bounds.y;
        for (int i = 0; i < _platformCount; i++)
        {
            Vector3 spawnPos = new Vector3();
            spawnPos.x = Random.Range(bounds.x + offset, bounds.x * -1 - offset);
            spawnPos.y += Random.Range(player.transform.position.y, _yHeight);
            
            Instantiate(_platformPrefab, spawnPos, quaternion.identity);
        }

        // for (int i = 0; i < _platformQuantity; i++)
        // {
        //     Vector3 spawnPos = new Vector3(Random.Range(_minDistance, _maxDistance), Random.Range(player.transform.position.y, player.transform.position.y + _yHeight), 0);
        //     Instantiate(_platformPrefab, spawnPos, quaternion.identity);
        // }
        
    }
}
