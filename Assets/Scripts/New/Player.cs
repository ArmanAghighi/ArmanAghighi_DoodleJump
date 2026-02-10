using System;
using UnityEngine;

public class Player : Singleton<Player>
{
    
    private Rigidbody2D _rb;
    private float _moveDirection = 0;
    private SpriteRenderer _spriteRenderer;
    private MultieControllerMoveAction _moveAction;
    
    [SerializeField , Range(1 , 20)] private int _moveSpeed;
    [SerializeField , Range(1 , 20)] private int _doodleBaseJumpForce;
    [SerializeField , Range(1 , 20)] private int _doodleHorizontalSpeed;

    [SerializeField] private GameObject _weapon;
    private SpriteRenderer _weaponSpriteRenderer;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _weaponSpriteRenderer = _weapon.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        DoodleMoveStartSystem();
    }

    void Update()
    {
        if (_moveDirection > 0)
        {
            _spriteRenderer.flipX = false;
            _weaponSpriteRenderer.flipY = false;
        }
        else if (_moveDirection < 0)
        {
            _spriteRenderer.flipX = true;
            _weaponSpriteRenderer.flipY = true;
        }
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(
            _moveDirection * _moveSpeed * _doodleHorizontalSpeed * Time.deltaTime,
            _rb.linearVelocity.y
        );
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Platform"))
            return;

        if (_rb.linearVelocity.y > 0)
            return;

        Platform platform = collision.collider.GetComponent<Platform>();
        if (platform == null || platform.GetPlatformData == null)
            return;

        Jump(platform.GetPlatformData.Force);  
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

    private void Jump(float force)
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
        _rb.AddForce(Vector2.up * force * _doodleBaseJumpForce, ForceMode2D.Impulse);
    }
}