using System;
using System.Collections;
using UnityEngine;


public class Jet : MonoBehaviour
{
    private bool _attached = false;
    private bool _isFalling = false;
    [SerializeField] private EdgeCollider2D _edgeCollider2D;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;

    private void FixedUpdate()
    {
        if (_isFalling)
        {
            _animator.SetBool("IsPlayingAnim", false);
            _audioSource.Stop();
            transform.Rotate(new Vector3(0, 0, -3f));
            transform.position -= new Vector3(0, 0.1f, 0);
            Destroy(gameObject, 3);
        }
    }

    void OnCollisionEnter2D(Collision2D Other)
    {
        if (Other.gameObject.tag == "Player" && !_attached)
        {
            if (Other.transform.childCount <= 2)
            {
                transform.parent = Other.transform;
                if (transform.parent.localScale.x < 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                Vector3 localPos = transform.localPosition;
                localPos.Set(-0.4f, -0.45f, 0);
                transform.localPosition = localPos;

                _edgeCollider2D.enabled = false;
                _animator.SetBool("IsPlayingAnim", true);
                _audioSource.Play();
                Rigidbody2D rb = Other.collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 force = rb.velocity;
                    force.y = 30;
                    rb.velocity = force;
                }
            }
            _attached = true;
        }
    }

    public void SetFall()
    {
        _isFalling = true;
    }
}
