using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformGreen : MonoBehaviour
{
    public float _jumpForce = 10f;
    public EdgeCollider2D _platformCollider;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.y <= 0f)
        {
            Rigidbody2D playerRb = col.collider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 playerVelocity = playerRb.velocity;
                playerVelocity.y = _jumpForce;
                playerRb.velocity = playerVelocity;
            }

            if (tag == "PlatformWhite")
            {
                GetComponent<PlatformWhite>().WhiteDestroy();
            }

            if (tag == "PlatformBrown")
            {
                transform.position -= new Vector3(0, 0.2f, 0);
                _platformCollider.enabled = false;
                GetComponent<PlatformBrown>().PlayAnim();
            }
        }
    }
}
