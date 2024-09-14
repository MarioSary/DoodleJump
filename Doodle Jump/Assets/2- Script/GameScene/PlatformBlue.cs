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
}
