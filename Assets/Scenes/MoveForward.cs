
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private int speed;
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
