using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _dooleRigidBody2D;
    private float _jumpHeight = 100;
    private bool _isOnGrounded = false;
    private int _jumpForce=150;
    private float _maxHeight = 0;

    void Update()
    {
        if (_isOnGrounded)
        {
            _jumpForce = 600;
            _dooleRigidBody2D.linearVelocity = new Vector2(_dooleRigidBody2D.linearVelocity.x, _jumpForce);
            _jumpHeight = transform.position.y;
            _isOnGrounded = false;

        }
        if (transform.position.y - _jumpHeight >= _maxHeight)
        {
            _dooleRigidBody2D.gravityScale = 70;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "platformInMenu")
        {
            _isOnGrounded = true;
        }
    }
}
