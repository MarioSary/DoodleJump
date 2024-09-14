using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    [SerializeField] private Transform _target;
    [SerializeField]private float _lerpSpeed = 0.2f;

    private void FixedUpdate()
    {
        if (_target.transform.position.y > transform.position.y + 1f)
        {
            Vector3 newPos = new Vector3(transform.position.x, _target.transform.position.y -1f, transform.position.z);
            Vector3 smoothPos = Vector3.Lerp(transform.position, newPos, _lerpSpeed);
            transform.position = smoothPos;
        }

        // if (_target.transform.position.y < transform.position.y - 4f)
        // {
        //     transform.position -= new Vector3(0, 0.5f, 0);
        // }
        
    }
}
