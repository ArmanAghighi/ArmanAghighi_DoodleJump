using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform _playerTransform;
    private float _maxHeight;
    private float _maxHeightSet = 0f; 
    [SerializeField] private float _smoothSpeed = 0.125f;
    private void Start()
    {
        _playerTransform = Player.Instance.transform;
    }
    void LateUpdate()
    {
        if (GameManager.Instance.IsGameStarted)
        {
            _maxHeight = (float)_playerTransform.position.y;
            if (_maxHeight > _maxHeightSet)
                _maxHeightSet = _maxHeight;
            Vector3 desiredCameraPosition = new Vector3(0, _maxHeightSet, -10f);
            Vector3 smoothedCameraPosition = Vector3.Lerp(transform.position, desiredCameraPosition, _smoothSpeed);
            transform.position = smoothedCameraPosition;
        }
    }
}
