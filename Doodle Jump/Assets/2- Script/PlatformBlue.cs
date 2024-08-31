using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformBlue : PlatformGreen
{
    // Blue Platforms Movement
    [SerializeField] private float _speed = 2f;
    private bool _toRight = true;
    private float Offset = 0.6f;

    void FixedUpdate()
    {
        BluePlatformMovement();
    }

    void BluePlatformMovement()
    {
        Vector3 movementBounds = Camera.main.ScreenToWorldPoint(transform.position);

        // Move to right
        if (_toRight)
        {
            if (transform.position.x < -movementBounds.x - Offset)
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            else
                _toRight = false;
        }
        // Move to left
        else 
        {
            if (transform.position.x > movementBounds.x + Offset)
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            else
                _toRight = true;
        }
    }
    
    
    
    /*private float _moveSpeed = 2f;
    private bool _toRight = true;
    private float _offset =0.6f;
    private Vector3 movementBounds;

    private void Start()
    {
        movementBounds = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void Update()
    {
        
        
        //move to right
        if (_toRight)
        {
            if (transform.position.x < -movementBounds.x - _offset)
            {
                transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
            }
            else
            {
                _toRight = false;
            }
        }
        //move to left
        else if (!_toRight)
        {
            if (transform.position.x > movementBounds.x + _offset)
            {
                transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
            }
            else
            {
                _toRight = true;
            }
        }

    }*/
}
