using UnityEngine;

public class Player : Singleton<Player>
{
    public float _moveDirection = 0;
    
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private MultieControllerMoveAction _moveAction;
    
    [SerializeField] private float _doodleHorizontalSpeed;
    [SerializeReference] private float _moveSpeed;
    
    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        DoodleMoveStartSystem();
    }

    void Update()
    {
        if (_moveDirection == 1)
            _spriteRenderer.flipX = false;
        else if (_moveDirection == -1)
            _spriteRenderer.flipX = true;
         _rb.linearVelocity = new Vector2(_moveDirection * _moveSpeed * _doodleHorizontalSpeed * Time.deltaTime, _rb.linearVelocity.y);
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
}