using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool _isGameStarted;
    public bool IsGameStarted => _isGameStarted;

    private  DifficultyLevelEnum _difficulty;
    public DifficultyLevelEnum DifficultyLevel => _difficulty;

    protected override void Awake()
    {
        base.Awake();
        _isGameStarted = false;

        PlatformDestructor.Instance.OnGameOver += GameOver;
    }

    private void GameOver()
    {
        
    }
}
