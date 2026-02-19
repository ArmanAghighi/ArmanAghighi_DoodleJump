using System;
using UnityEngine;

public class Player : Singleton<Player>
{
    public Action<Platform> OnPlatformReach;

    private Rigidbody2D _rb;
    private float _moveDirection = 0;
    private SpriteRenderer _spriteRenderer;

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

         if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos = Input.mousePosition;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            worldPos.z = 0f; // مهم برای 2D

            if (worldPos.y > transform.position.y)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        throw new NotImplementedException();
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(
            _moveDirection * _moveSpeed * _doodleHorizontalSpeed * Time.fixedDeltaTime,
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
        OnPlatformReach?.Invoke(platform);
    }

    public void OnLeftButtonDown()
    {
        _moveDirection = -1;
    }

    public void OnRightButtonDown()
    {
        _moveDirection = 1;
    }

    public void OnLeftButtonUp()
    {
        _moveDirection = 0;
    }

    public void OnRightButtonUp()
    {
        _moveDirection = 0;
    }

    private void Jump(float force)
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
        _rb.AddForce(Vector2.up * force * _doodleBaseJumpForce, ForceMode2D.Impulse);
    }
}
