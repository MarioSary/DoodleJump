using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    #region Singleton
    public static CameraMovement Instance;
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
    
    [SerializeField] private Transform _target;
    [SerializeField]private float _lerpSpeed = 0.2f;
    private bool _isGameOver = false;
    
    private void FixedUpdate()
    {
        if (!_isGameOver)
        {
            if (_target.transform.position.y > transform.position.y + 1f)
            {
                Vector3 newPos = new Vector3(transform.position.x, _target.transform.position.y -1f, transform.position.z);
                Vector3 smoothPos = Vector3.Lerp(transform.position, newPos, _lerpSpeed);
                transform.position = smoothPos;
            }
        }
       
    }

    public void OnGameOver()
    {
        _isGameOver = true;
    }
}
