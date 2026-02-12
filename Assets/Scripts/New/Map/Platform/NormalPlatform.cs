using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NormalPlatform : Platform
{
    [SerializeField] private int _PlatformSpeed = 6;
    [SerializeField] private Sprite _nonBrokenSprite;
    [SerializeField] private List<Sprite> _sprites;

    private bool _isPlatformBroken = false;
    private SpriteRenderer _spriteRenderer;
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
        _spriteRenderer.sprite = _nonBrokenSprite;
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
        _spriteRenderer.sprite = _sprites[_platformLifeTime];
        if (_platformLifeTime <= 0)
            StartCoroutine(DisablePlatform(0.1f));
    }

    private IEnumerator DisablePlatform(float timer)
    {
        yield return new WaitForSeconds(timer);
        _isPlatformBroken = true;
    }

    private void Update()
    {
        if (_isPlatformBroken)
        {
            transform.position += Vector3.down * _PlatformSpeed * Time.deltaTime;
        }
    }
}
