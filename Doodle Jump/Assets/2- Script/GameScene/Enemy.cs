using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    private bool _toRight = true;
    [SerializeField] private SpriteRenderer enemySprite;
    [SerializeField] private Sprite[] walkSprites = new Sprite[2];
    [SerializeField] private AudioClip[] enemyAudios = new AudioClip[2];

    void FixedUpdate()
    {
        StartCoroutine(EnemyMoveRoutine());
    }

    IEnumerator EnemyMoveRoutine()
    {
        Vector3 direction = new Vector3(0.7f, 0, 0);
        // Move to right
        if (_toRight)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
            yield return new WaitForSeconds(1);
            if (gameObject.name == "EnemyPenguin")
            {
                enemySprite.sprite = walkSprites[0];
            }
            _toRight = false;
        }
        // Move to left
        else 
        {
            transform.Translate(-direction * _speed * Time.deltaTime);
            yield return new WaitForSeconds(1);
            if (gameObject.name == "EnemyPenguin")
            {
                enemySprite.sprite = walkSprites[1];
            }
            _toRight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Damage")
        {
            AudioSource.PlayClipAtPoint(enemyAudios[0], transform.position);
            Rigidbody2D rb = col.GetComponentInParent<Rigidbody2D>();
            BoxCollider2D doodlerCollider = col.GetComponentInParent<BoxCollider2D>();
            doodlerCollider.enabled = false;
            if (rb != null)
            {
                Vector2 force = rb.velocity;
                force.y = -10;
                rb.velocity = force;
            }
            col.GetComponentInParent<PLayer>().OnHit();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(enemyAudios[1], transform.position);
            Destroy(gameObject);
        }
    }
}
