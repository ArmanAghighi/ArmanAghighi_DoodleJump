using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    private float smoothSpeed = 0.125f;
    private float maxHeight;
    private float maxHeightSet = 0f; 
    private NewDoodleControllerSystem gameOverCheck;
    private void Start()
    {
        gameOverCheck = FindObjectOfType<NewDoodleControllerSystem>();
    }
    void LateUpdate()
    {
        if (!gameOverCheck._gameIsOver)//Debug.Log("Over");  Done
        {
            maxHeight = (float)objectToFollow.position.y;
            if (maxHeight > maxHeightSet)
                maxHeightSet = maxHeight;
            Vector3 desiredCameraPosition = new Vector3(0, maxHeightSet, -10f);
            Vector3 smoothedCameraPosition = Vector3.Lerp(transform.position, desiredCameraPosition, smoothSpeed);
            transform.position = smoothedCameraPosition;
        }
    }
}