using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlatformProperty : MonoBehaviour
{
    [SerializeField] AudioClip _jumpAudioClip;
    private AudioSource _jumpAudioSource;
    private Transform _player;
    private int _speed = 4;
    private bool _direction = true;//true Right
    private float _maxX;
    private float _minX;
    private Vector2 _rightMovement;
    private Vector2 _leftMovement;
    private void Start()
    {
        _maxX = gameObject.transform.position.x + 3;
        _minX = gameObject.transform.position.x - 3;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _jumpAudioSource = GetComponent<AudioSource>();
        if (_jumpAudioSource != null)
        {
            _jumpAudioSource.clip = _jumpAudioClip;
        }
    }
    private void Update()
    {
        CheckDestroyCondetion();
        if (_direction)
        {
            _rightMovement = Vector2.right;
            transform.Translate(_rightMovement * _speed * Time.deltaTime);
            if (gameObject.transform.position.x > _maxX)
                _direction = false;
        }
        if (!_direction)
        {
            _leftMovement = Vector2.left;
            transform.Translate(_leftMovement * _speed * Time.deltaTime);
            if (gameObject.transform.position.x < _minX)
                _direction = true;
        }
    }
    private void CheckDestroyCondetion()
    {
        if (_player.position.y > gameObject.transform.position.y + 5)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && other.relativeVelocity.y <= 0f)  
        {
            _jumpAudioSource.Play();
        }
    }
}
