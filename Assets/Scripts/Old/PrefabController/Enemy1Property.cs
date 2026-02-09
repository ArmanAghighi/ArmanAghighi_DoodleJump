using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Property : MonoBehaviour
{
    private NewDoodleControllerSystem _newDoodleControllerSystem;
    private Collider2D _gameObjectCollider;
    private int _enemyHealth = 1;
    private void Awake()
    {
        _newDoodleControllerSystem = GameObject.FindObjectOfType<NewDoodleControllerSystem>();
        _gameObjectCollider = gameObject.GetComponent<Collider2D>();
    }
    private void Update()
    {
        if (_enemyHealth <= 0)
        {
            int _speedDown = 5;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 180f);
            float rotationSpeed = 90f;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            _gameObjectCollider.enabled = false;
            transform.Translate(Vector2.up * _speedDown * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            _enemyHealth--;
        }
    }
    public void setGameOver()
    {
        _newDoodleControllerSystem._gameIsOver = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            setGameOver();
        }
    }
}
