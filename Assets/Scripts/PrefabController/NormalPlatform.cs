using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlatform : MonoBehaviour
{
    private Transform _player;
    [SerializeField] AudioClip _jumpAudioClip;
    private AudioSource _jumpAudioSource;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (_player == null)
        {
            Debug.LogError("_player is null");
        }

        _jumpAudioSource = GetComponent<AudioSource>();

        if (_jumpAudioSource != null)
        {
            _jumpAudioSource.clip = _jumpAudioClip;
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

    private void Update()
    {
        CheckDestroyCondetion();
    }
}