using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperty : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D _rb;
    private float _timeToDestroy = 2f;
    [SerializeField] private AudioClip _shotSound;
    private AudioSource _audioSource;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        if (_rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the Bullet prefab!");
        }
        if (_audioSource == null)
        {
            Debug.LogError("AudioSource component not found on the Bullet prefab!");
        }
        else
        {
            _audioSource.clip = _shotSound;
        }
        _audioSource.Play();
        Destroy(gameObject, _timeToDestroy);

    }

    public void Shoot(Vector2 direction)
    {
        if (_rb != null)
        {
            // Set the velocity of the bullet based on the specified direction and speed
            _rb.linearVelocity = direction.normalized * speed * 2;
        }
        else
        {
            Debug.LogError("Rigidbody2D component is null in the Bullet script!");
        }
    }
}
