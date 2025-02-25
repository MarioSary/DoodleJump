using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundries : MonoBehaviour
{

    private float _objectWidth;
    private float _objectHeight;

    private Vector3 _screenBounds;

    /*public Vector3 ScreenBounds
    {
        get
        {
            return screenBounds;
        }
        set
        {
            screenBounds = value;
                // Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,
                //     Camera.main.transform.position.z));
        }
    }*/

    private void Start()
    {
        _screenBounds =
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        _objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, _screenBounds.x * -1 + _objectWidth, _screenBounds.x - _objectWidth);
        //viewPos.y = Mathf.Clamp(viewPos.y, _screenBoundries.y * -1 + _objectHeight, _screenBoundries.y - _objectHeight);
        transform.position = viewPos;
    }
}
