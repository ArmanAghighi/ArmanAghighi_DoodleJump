using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackProperty : MonoBehaviour
{
    private Transform _player;
    private Rigidbody2D _rb;
    private void Awake()
    {
        _player = Player.Instance.transform;
        _rb = _player.GetComponent<Rigidbody2D>();

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _rb.linearVelocity == new Vector2(_rb.linearVelocity.x, 0))
        {
            gameObject.SetActive(false);
        }
    }
}
