using UnityEngine;
using System.Collections;

public class WeakPlatform : Platform
{
    [SerializeField] private Sprite _NotBrokenSprite;
    [SerializeField] private Sprite _brokenPlatform;
    [SerializeField] private int _brokenWeakPlatformSpeed = 6;
    
    private SpriteRenderer _spriteRenderer;
    private bool _isPlatformBroken = false;
    private int _platformLife;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _platformLife = _platformLifeTime;
    }

    protected override void PlayAudio()
    {
        base.PlayAudio();
    }

    void OnEnable()
    {
        Player.Instance.OnPlatformReach += DisablePlatformCoroutineStarter;
        _platformLifeTime = _platformLife;
        _spriteRenderer.sprite = _NotBrokenSprite;
        _isPlatformBroken = false;
    }

    void OnDisable()
    {
        Player.Instance.OnPlatformReach -= DisablePlatformCoroutineStarter;
    }

    private void DisablePlatformCoroutineStarter(Platform platform)
    {
        if (platform != this)
            return;

        _platformLifeTime--;

        if (_platformLifeTime <= 0)
            StartCoroutine(DisablePlatform(0.1f));
    }

    private IEnumerator DisablePlatform(float timer)
    {
        yield return new WaitForSeconds(timer);
        _spriteRenderer.sprite = _brokenPlatform;
        _isPlatformBroken = true;
    }

    private void Update()
    {
        if (_isPlatformBroken)
        {
            transform.position += Vector3.down * _brokenWeakPlatformSpeed * Time.deltaTime;
        }
    }
}