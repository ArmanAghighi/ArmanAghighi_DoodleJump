using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;

    private float highestY;

    void Start()
    {
        highestY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        if (target.position.y > highestY)
        {
            highestY = target.position.y;

            Vector3 desiredPosition = new Vector3(
                transform.position.x,
                highestY,
                transform.position.z
            );

            transform.position = Vector3.Lerp(
                transform.position,
                desiredPosition,
                smoothSpeed * Time.deltaTime
            );
        }
    }
}
