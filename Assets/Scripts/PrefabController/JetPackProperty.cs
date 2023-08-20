using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackProperty : MonoBehaviour
{
    private Transform _player;
    private Rigidbody2D _rb;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = _player.GetComponent<Rigidbody2D>();

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _rb.velocity == new Vector2(_rb.velocity.x, 0))
        {
            gameObject.SetActive(false);
        }
    }
}
