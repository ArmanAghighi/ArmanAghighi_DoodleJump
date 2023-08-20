using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeProperty : MonoBehaviour
{
    private const float pi = 3.14f;
    private Transform _player;
    private Rigidbody2D _rb;
    private float speed = 0.2f;
    private void Awake()
    {
        _player =GameObject.Find("Doodle").GetComponent<Transform>();
        _rb = _player.GetComponent<Rigidbody2D>();
        Vector2 vector2 = new Vector2(0.3f, 0.3f);
    }
    void Update()
    {
        CheckDestroyCondetion();
        CheckForDoodle();

    }
    private void CheckDestroyCondetion()
    {
        if (_player.position.y > gameObject.transform.position.y + 5)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject,10f);
        }
    }
    bool isPrefabInCircle(float radius, float x, float y)
    {

        float area = radius * radius * pi;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(x, y), new Vector2(radius, radius), 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
            {
                //Debug.Log("Inside Force");
                _rb.velocity = Vector2.zero;
                _rb.gravityScale = 0;
                UnfreezeZAxisConstraints();
                return true;
            }
        }
        return false;
    }
    void UnfreezeZAxisConstraints()
    {
        RigidbodyConstraints2D constraints = _rb.constraints;
        constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        _rb.constraints = constraints;
    }
    private void CheckForDoodle()
    {
        if (isPrefabInCircle(1f, gameObject.transform.position.x, gameObject.transform.position.y))
        {
            _player.transform.position = Vector2.Lerp(_player.transform.position, gameObject.transform.position, speed);
            _player.transform.localScale = Vector3.Lerp(_player.transform.localScale, Vector3.zero, speed);
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 359f); 
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, targetRotation, speed );
        }
    }
}
