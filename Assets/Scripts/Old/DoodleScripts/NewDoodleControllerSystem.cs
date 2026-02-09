using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NewDoodleControllerSystem : MonoBehaviour
{
    // Doodle Move System Variables
    private MultieControllerMoveAction _moveAction;
    public float _moveDirection = 0;
    [SerializeReference] private float _moveSpeed;
    private Rigidbody2D _rb;
    [SerializeField] private float _doodleHorizontalSpeed;
    private SpriteRenderer _spriteRenderer;
    //Doodle Jump System Variables
    private float _jumpForce;
    [SerializeField] private float _maxHeight = 5f;
    [SerializeReference] private float _doodleGravity;
    private bool _isOnGrounded = false;
    private bool _isOnSpring = false;
    private bool _isOnJetpack = false;
    [SerializeField] GameObject _springObjectNormalParent;
    [SerializeField] GameObject _springObjectMoverParent;
    [SerializeField] GameObject _jetpackObjectNormalParent;
    [SerializeField] GameObject _jetpackObjectMoverParent;
    private float _jumpHeight;
    //Doodle Fire System Variables
    [SerializeField] private Sprite _shootSprite;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Sprite _doodleIdle;
    [SerializeField] private GameObject _shootHead;
    [SerializeField] private Transform _shootHeadTransform;
    [SerializeField] private SpriteRenderer _doodleSpriteRenderer;
    private bool _firing = false;
    private bool _readyToShot = false;
    private bool _firstShotDone = false;
    //GameOver Logic System Variable
    [SerializeField] private GameObject _playerGameObject;
    [SerializeField] private GameObject _gameOverGameObject;
    [SerializeField] private GameObject _backGroundGameObject;
    [SerializeField] private Canvas _gameOverCanvas;
    private float _moveDownSpeed = 5f;
    public bool _gameIsOver = false;
    //Audio Variables
    private AudioSource _gameOverAudioSource;
    [SerializeField] private AudioClip _gameOverAudioClip;
    private bool _isPlayed = false;
    //Simillar Variables
    [SerializeField] private Camera _mainCamera;
    private bool _isDoodleActive;

    private void Awake()
    {
        _gameIsOver = false;
        _isDoodleActive = true;
        DoodleMoveAwakeSystem();
        AudioAwakeSystem();
    }
    void Start()
    {
        DoodleMoveStartSystem();
        DoodleFireStartSystem();
    }
    void Update()
    {
        GameOverLogicUpdateSystem();
        if (_isDoodleActive)
        {
            DoodleMoveUpdateSystem();
            DoodleJumpUpdateSystem();
            DoodleFireUpdateSystem();
        }
    }
    private void AudioAwakeSystem()
    {
        _gameOverAudioSource = GetComponent<AudioSource>();
        if (_gameOverAudioSource == null)
        {
            Debug.LogError("AudioSource component not found on the GameOver Doodle!");
        }
        else
        {
            _gameOverAudioSource.clip = _gameOverAudioClip;
        }
    }
    private void DoodleJumpUpdateSystem()
    {
        Debug.Log(_isOnJetpack);
        if (_isOnJetpack)
        {
            if (_spriteRenderer.flipX)
            {
                transform.Find("OnDoodleJetPackRight").gameObject.SetActive(true);
                transform.Find("OnDoodleJetPackLeft").gameObject.SetActive(false);
            }
            if (_spriteRenderer.flipX)
            {
                transform.Find("OnDoodleJetPackLeft").gameObject.SetActive(true);
                transform.Find("OnDoodleJetPackRight").gameObject.SetActive(false);
            }
            _maxHeight = 18;
            _jumpForce = 20;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
            _jumpHeight = transform.position.y;
        }
        else if (_isOnSpring)
        {
            _maxHeight = 12;
            _jumpForce = 15;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
            _jumpHeight = transform.position.y;
            _isOnSpring = false;
        }
        else if (_isOnGrounded)
        {
            _jumpForce = 8;
            _maxHeight = 5;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _jumpForce);
            _jumpHeight = transform.position.y;
            _isOnGrounded = false;
        }
    }
    Vector2 converScreenToWorld()
    {
        Camera mainCamera = Camera.main;
        Vector2 screenPosition = new Vector2(Screen.width, Screen.height);
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "LeftBoundary")//Tag for left side of the Screen
        {
            transform.position = new Vector3(converScreenToWorld().x, transform.position.y, transform.position.z);//come from right(screenwidth)
        }
        else if (other.gameObject.tag == "RightBoundary")//Tag for Right side of the Screen
        {
            transform.position = new Vector3(-converScreenToWorld().x, transform.position.y, transform.position.z);//come from Left(-screenwidth)
        }
        if (other.gameObject.tag == "GameOver" || other.gameObject.tag == "Hole" || other.gameObject.tag == "Enemy1" || other.gameObject.tag == "Enemy2")
        {
            _gameIsOver = true;
            if(_isPlayed == false)
            {
                _gameOverAudioSource.Play();
                _isPlayed = true;
            }
            StartCoroutine(Animate());
            _mainCamera.transform.DetachChildren();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Fanar") && _rb.linearVelocity == new Vector2(_rb.linearVelocity.x, 0))
        {
            _isOnSpring = true;
        }
        if (other.collider.CompareTag("Jetpack") && _rb.linearVelocity == new Vector2(_rb.linearVelocity.x, 0))
        {
            _isOnJetpack = true;
        }
        else if (other.collider.CompareTag("normalPlatform") && _rb.linearVelocity == new Vector2(_rb.linearVelocity.x, 0) ||
                other.collider.CompareTag("moverPlatform") && _rb.linearVelocity == new Vector2(_rb.linearVelocity.x, 0))
        {
            _isOnGrounded = true;
        }
    }
    private void DoodleMoveAwakeSystem()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb ??= GetComponent<Rigidbody2D>();
    }
    private void DoodleMoveStartSystem()
    {
        _moveAction = new MultieControllerMoveAction();
        _moveAction.Enable();
        _moveAction.Move.PCHorizontal.performed += context =>
        {
            _moveDirection = context.ReadValue<float>();

        };

        _moveAction.Move.PCHorizontal.canceled += context =>
        {
            _moveDirection = 0;
        };
    }
    private void DoodleMoveUpdateSystem()
    {
        if (_moveDirection == 1)
            _spriteRenderer.flipX = false;
        else if (_moveDirection == -1)
            _spriteRenderer.flipX = true;

        _rb.linearVelocity = new Vector2(_moveDirection * _moveSpeed * _doodleHorizontalSpeed * Time.deltaTime, _rb.linearVelocity.y);
    }
    private void DoodleFireStartSystem()
    {
        _mainCamera = Camera.main;
    }
    private void DoodleFireUpdateSystem()
    {
        if (!Input.GetMouseButtonDown(0) && _firing)
        {
            StartCoroutine(WaitToChangeSpriteToIdle());
        }
        else if(Input.GetMouseButtonDown(0))
        {
            if (_firstShotDone)
            {
                _doodleSpriteRenderer.sprite = _shootSprite;
                _shootHead.SetActive(true);
            }
            _firing = true;
        }
        if (_firing)
                {
                    Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = 0f;
                    Vector3 direction = mousePosition - _shootHead.transform.position;
                    float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
                    if ((angle > 60f && angle < 360f) || (angle < -180f && angle > -300f)) { angle = 60; _readyToShot = false; }
                    else if (angle < -60f && angle > -180f) { angle = -60; _readyToShot = false; }
                    else { _readyToShot = true; }
                    _shootHead.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                    if (Input.GetMouseButtonDown(0) && _readyToShot)
                    {
                        DoodleFireSystemFireBullet();
                        _firstShotDone = true;
                    }
                }
    }
    private void DoodleFireSystemFireBullet()
    {
        if (_firstShotDone)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector2 direction = (mousePosition - _shootHeadTransform.position).normalized;
            GameObject bullet = Instantiate(_bulletPrefab, _shootHeadTransform.position, Quaternion.identity);
            bullet.GetComponent<BulletProperty>().Shoot(direction);
        }
    }
    private IEnumerator  WaitToChangeSpriteToIdle()
    {
        yield return new WaitForSeconds(10f);
        if (Input.GetMouseButtonDown(0))
            yield break;
            //Debug.Log("pressed");
        _shootHead.SetActive(false);
        _doodleSpriteRenderer.sprite = _doodleIdle;
        _firing = false;
    }
    private void GameOverLogicUpdateSystem()
    {
        if (_gameIsOver == true)
        {
            GameObject[] normalPlatformPrefabArray = GameObject.FindGameObjectsWithTag("normalPlatform");
            for (int i = 0; i < normalPlatformPrefabArray.Length; i++)
            {
                normalPlatformPrefabArray[i].SetActive(false);
            }
            GameObject[] weakPlatformPrefabArray = GameObject.FindGameObjectsWithTag("WeakPlatform");
            for (int i = 0; i < weakPlatformPrefabArray.Length; i++)
            {
                weakPlatformPrefabArray[i].SetActive(false);
            }
            if (gameObject.transform.position.y < _gameOverGameObject.transform.position.y)
            {
                _isDoodleActive = false;
            }
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 180f);
            float rotationSpeed = 90f;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    IEnumerator Animate()
    {
        while (_mainCamera.transform.position.y > _gameOverGameObject.transform.position.y)
        {
            _backGroundGameObject.transform.Translate(Vector3.down * _moveDownSpeed * Time.deltaTime);
            _playerGameObject.transform.Translate(Vector3.down * (_moveDownSpeed / 2) * Time.deltaTime);
            _mainCamera.transform.Translate(Vector3.down * _moveDownSpeed * Time.deltaTime);
            yield return null;
        }
        _gameOverCanvas.gameObject.SetActive(true);
    }
}