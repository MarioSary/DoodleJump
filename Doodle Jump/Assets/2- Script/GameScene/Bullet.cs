using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //To rotate
    private Vector3 mousePos;
    
    //To Move
    private Rigidbody2D _bulletRigid;
    [SerializeField] private float bulletForce;

    private void Start()
    {
        _bulletRigid = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        if (mousePos.y < transform.position.y + 1)
        {
            rotation.y = rotation.y - 90f;
        }
        _bulletRigid.velocity = new Vector2(direction.x, direction.y).normalized * bulletForce;
        float bulletRotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, bulletRotationZ);
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
