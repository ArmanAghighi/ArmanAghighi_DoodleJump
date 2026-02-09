using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Property : MonoBehaviour
{
    private NewDoodleControllerSystem _newDoodleControllerSystem;
    [SerializeField] Sprite _enemyShotedSprite;
    private int _enemy2Health = 2;
    private SpriteRenderer _enemy2SpriteRenderer;
    private Collider2D _gameObjectCollider;
    private void Awake()
    {
        _newDoodleControllerSystem = GameObject.FindObjectOfType<NewDoodleControllerSystem>();
        _enemy2SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _gameObjectCollider = gameObject.GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (_enemy2Health == 1)
        {
            _enemy2SpriteRenderer.sprite = _enemyShotedSprite;
        }
        if (_enemy2Health <= 0)
        {
            int _speedDown = 5;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 180f);
            float rotationSpeed = 90f;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            _gameObjectCollider.enabled = false;
            transform.Translate(Vector2.up * _speedDown * Time.deltaTime);
        }
    }
    public void SetGameOver()
    {
        _newDoodleControllerSystem._gameIsOver = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetGameOver();
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            _enemy2Health--;
            Destroy(other.gameObject);
        }
    }
}
