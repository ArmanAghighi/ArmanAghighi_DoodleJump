using System;
using UnityEngine;

public class PlatformDestructor : Singleton<PlatformDestructor>
{
    public Action OnGameOver;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Arman");
        }
        if (collision.CompareTag("Platform"))
        {
            OnGameOver?.Invoke();
        }
    }
}
