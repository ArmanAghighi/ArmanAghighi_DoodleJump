using UnityEngine;

public class PlatformDestructor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
