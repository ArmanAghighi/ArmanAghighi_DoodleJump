using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBackgroundEventSystem : MonoBehaviour
{
    //BackgroundMovement Variables
    [SerializeField] private Transform _backgroundTransform;
    private float _smoothSpeed = 0.125f;
    private float _maxHeight;
    private float _maxHeightSet = 0f;
    private NewDoodleControllerSystem _gameOverCheck;

    Vector2 converScreenToWorld()
    {
        Camera mainCamera = Camera.main;
        Vector2 screenPosition = new Vector2(Screen.width, Screen.height);
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    private void Start()
    {
        _gameOverCheck = FindObjectOfType<NewDoodleControllerSystem>();
        GameObject Boundaries = new GameObject("Boundaries");
        CreateLeftBoundry();
        CreateRightBoundry();
        GameOverBoundry();
        Boundaries.transform.SetParent(GameObject.Find("BackGround").transform);
    }
    void LateUpdate()
    {
        if (!_gameOverCheck._gameIsOver)//Debug.Log("Over");  Done
        {
            _maxHeight = (float)_backgroundTransform.position.y;
            if (_maxHeight > _maxHeightSet)
                _maxHeightSet = _maxHeight;
            Vector3 desiredCameraPosition = new Vector3(0, _maxHeightSet, 0);
            Vector3 smoothedBackgroundPosition = Vector3.Lerp(transform.position, desiredCameraPosition, _smoothSpeed);
            transform.position = smoothedBackgroundPosition;
        }
    }
    private void CreateLeftBoundry()
    {
        GameObject leftEdgeObject = new GameObject("LeftBoundry");
        leftEdgeObject.tag = "LeftBoundary";
        EdgeCollider2D edgeCollider = leftEdgeObject.AddComponent<EdgeCollider2D>();
        edgeCollider.isTrigger = true;
        Vector2[] points = new Vector2[2];
        points[0] = new Vector2(-converScreenToWorld().x - 0.5f, -converScreenToWorld().y + 0.5f);
        points[1] = new Vector2(-converScreenToWorld().x - 0.5f, converScreenToWorld().y);
        edgeCollider.points = points;
        leftEdgeObject.transform.position = new Vector2(0.0f, 0.0f);
        leftEdgeObject.transform.SetParent(gameObject.transform);
        leftEdgeObject.transform.SetParent(GameObject.Find("Boundaries").transform);
    }
    private void CreateRightBoundry()
    {
        GameObject rightEdgeObject = new GameObject("RightBoundry");
        rightEdgeObject.tag = "RightBoundary";
        EdgeCollider2D edgeCollider = rightEdgeObject.AddComponent<EdgeCollider2D>();
        edgeCollider.isTrigger = true;
        Vector2[] points = new Vector2[2];
        points[0] = new Vector2(converScreenToWorld().x + 0.5f, -converScreenToWorld().y + 0.5f);
        points[1] = new Vector2(converScreenToWorld().x + 0.5f, converScreenToWorld().y);
        edgeCollider.points = points;
        rightEdgeObject.transform.position = new Vector2(0.0f, 0.0f);
        rightEdgeObject.transform.SetParent(gameObject.transform);
        rightEdgeObject.transform.SetParent(GameObject.Find("Boundaries").transform);
    }
    private void GameOverBoundry()
    {
        GameObject downEdgeObject = new GameObject("GameOverBoundry");
        downEdgeObject.tag = "GameOver";
        EdgeCollider2D edgeCollider = downEdgeObject.AddComponent<EdgeCollider2D>();
        edgeCollider.isTrigger = true;
        Vector2[] points = new Vector2[2];
        points[0] = new Vector2(-converScreenToWorld().x - 0.5f, -converScreenToWorld().y);
        points[1] = new Vector2(converScreenToWorld().x + 0.5f, -converScreenToWorld().y);
        edgeCollider.points = points;
        downEdgeObject.transform.position = new Vector2(0.0f, 0.0f);
        downEdgeObject.transform.SetParent(gameObject.transform);
        downEdgeObject.transform.SetParent(GameObject.Find("Boundaries").transform);
    }

}
