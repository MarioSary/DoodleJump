using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformGreen : MonoBehaviour
{
    public float jumpForce = 10f;
    public EdgeCollider2D platformCollider;
    public string poolTag;


    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.relativeVelocity.y <= 0f)
        {
            Rigidbody2D playerRb = col.collider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 playerVelocity = playerRb.velocity;
                playerVelocity.y = jumpForce;
                playerRb.velocity = playerVelocity;
            }

            if (tag == "PlatformWhite")
            {
                GetComponent<PlatformWhite>().WhiteDestroy();
            }

            if (tag == "PlatformBrown")
            {
                transform.position -= new Vector3(0, 0.2f, 0);
                platformCollider.enabled = false;
                GetComponent<PlatformBrown>().PlayAnim();
            }
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Deactivator"))
        {
            Debug.Log("Enter");
            ObjectPooler.Instance.poolDictionary[poolTag].Enqueue(gameObject);
            gameObject.SetActive(false);
        }
    }
}
