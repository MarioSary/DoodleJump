using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDoodler : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private SpriteRenderer doodlerSprite;
    [SerializeField] Sprite[] jumpSprite = new Sprite[2];
    


    private void Start()
    {
       _rb = GetComponent<Rigidbody2D>();
       _rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }

    private void Update()
    {
        if (_rb.velocity.y > 0)
        {
            doodlerSprite.sprite = jumpSprite[0];
        }
        if (_rb.velocity.y < 0)
        {
            doodlerSprite.sprite = jumpSprite[1];
        }
    }
}
