using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringProperty : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _animator.Play("SpringAnim");
        }
    }
}
