using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBrown : PlatformGreen
{
    private EdgeCollider2D _collider;
    private PlatformEffector2D _platformEffector;

    private void Start()
    {
        _collider = GetComponent<EdgeCollider2D>();
        _platformEffector = GetComponent<PlatformEffector2D>();
    }

    public void Deactivate()
    {
        _collider.enabled = false;
        _platformEffector.enabled = false;
    }
    
    public void BrownDestroy()
    {
        Destroy(gameObject);
    }
    
}
