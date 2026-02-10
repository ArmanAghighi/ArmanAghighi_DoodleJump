using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPlatform : MonoBehaviour
{

}
/*
    [SerializeField] private Sprite _breakingWeakPlatform;//new broken sprite
    [SerializeField] private BoxCollider2D _weakPlatformBoxCollider2d;//box collider of weak platform
    [SerializeField] private int _brokenWeakPlatformSpeed = 5;
    private SpriteRenderer _spriteRenderer;
    private bool _brokenWeakPlatform = false;//is the platform broken?
    private Transform _backGroundTransform;//check where the back ground Y is to deswtroy platform
    [SerializeField] private int _destroyPointTransform = 10;// Y < backgroundY - 10
    [SerializeField] AudioClip _destroyWeakClip;
    private AudioSource _destroyAudioSource;
    private void Start()
    {
        _destroyAudioSource = GetComponent<AudioSource>();
        if (_destroyAudioSource != null)
        {
            _destroyAudioSource.clip = _destroyWeakClip;
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _weakPlatformBoxCollider2d = GetComponent<BoxCollider2D>();
        GameObject backGroundTransform = GameObject.FindGameObjectWithTag("BackGround");
        if (backGroundTransform != null)
        {
            _backGroundTransform = backGroundTransform.transform;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player") && other.relativeVelocity.y <= 0) 
        {
            _destroyAudioSource.Play();
            _spriteRenderer.sprite = _breakingWeakPlatform;
            _weakPlatformBoxCollider2d.enabled = false;
            _brokenWeakPlatform = true;
        }
        if (other.gameObject.tag == "GameOver")
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (_brokenWeakPlatform)
        {
            transform.position += Vector3.down * _brokenWeakPlatformSpeed * Time.deltaTime;
        }
        if (gameObject.transform.position.y < _backGroundTransform.transform.position.y - _destroyPointTransform)
        {
            Destroy(gameObject);
        }
    }
*/