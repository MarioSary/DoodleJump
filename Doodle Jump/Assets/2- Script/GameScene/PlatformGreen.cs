using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformGreen : MonoBehaviour
{
    // to enqueue platforms
    //[SerializeField]private PoolObjectType type;
    
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    
    public Sprite[] playerJumpSprite = new Sprite[2];
    private AudioSource _playerAudioSource;
    [SerializeField] private AudioClip _playerJumpAudio;
    public Animator _animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected void OnCollisionEnter2D(Collision2D col)
    {
        var sprite = col.transform.GetComponent<SpriteRenderer>();
        sprite.sprite = playerJumpSprite[0];
        //Debug.Log(sprite.sprite.name);
        if (col.relativeVelocity.y <= 0f)
        {
            Rigidbody2D playerRb = col.collider.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 playerVelocity = playerRb.velocity;
                playerVelocity.y = jumpForce;
                playerRb.velocity = playerVelocity;
            }

            if (_animator != null)
            {
                _animator.SetBool("IsPlayingAnim", true);
            }
            
            _playerAudioSource = col.collider.GetComponent<AudioSource>();
            _playerAudioSource.clip = _playerJumpAudio;
            _playerAudioSource.Play();
            

            if (tag == "PlatformWhite")
            {
                GetComponent<PlatformWhite>().WhiteDestroy();
            }

            if (tag == "PlatformBrown")
            {
                GetComponent<PlatformBrown>().Deactivate();
            }
        }
    }

    protected void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            var sprite = other.transform.GetComponent<SpriteRenderer>();
            sprite.sprite = playerJumpSprite[1];
            //Debug.Log(sprite.sprite.name);
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.CompareTag("Deactivator"))
    //     {
    //         Debug.Log("Enter");
    //         ObjectPooler.Instance.poolDictionary[type].Enqueue(gameObject);
    //     }
    // }
}
