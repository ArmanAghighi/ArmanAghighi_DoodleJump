using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool _isGameStarted;
    public bool IsGameStarted => _isGameStarted;

    private  DifficultyLevel _difficulty;
    public DifficultyLevel DifficultyLevel => _difficulty;

    [SerializeField] private MapGeneratorData _mapGeneratorData;

    protected override void Awake()
    {
        base.Awake();
        _isGameStarted = false;

        BuildWarmUpMap();
    }

    private void BuildWarmUpMap()
    {
        
    }
}
