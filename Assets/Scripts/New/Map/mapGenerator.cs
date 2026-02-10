using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private MapGeneratorData _mapGeneratorData;
    
    [Header("Pool Values"),Space(5)]
    [SerializeField] private int _poolSize;
    [SerializeField] private Transform _platformPoolParent;

    [Header("Platforms Typs"),Space(5)]
    [SerializeField] private List<PlatformTypes> _platforms;

    private List<GameObject> _pool = new List<GameObject>();
    private Transform _myTransform;

    private int _index = 0;
    private float _startHeight;
    private float _lastPlatformYPosition;
    private bool _canMove = true;

    void Awake()
    {
        _myTransform = transform;    
    }

    void Start()
    {
        _startHeight = _mapGeneratorData.Segments[_index].StartPosition;
        _lastPlatformYPosition = _startHeight;

        CreatePool();
    }

    void Update()
    {
        if (!_canMove)
            _canMove = true;

        if (_canMove)
            MoveUp();

        SpawnSegmentPlatforms(_mapGeneratorData.Segments[_index]);
    }

    private void MoveUp()
    {
        if (_myTransform.position.y > _mapGeneratorData.Segments[_index].EndPosition)
        {
            _canMove = false;
            _index++;

            if (_index >= _mapGeneratorData.Segments.Count)
            {
                enabled = false;
                return;
            }

            _startHeight = _mapGeneratorData.Segments[_index].StartPosition;
            _lastPlatformYPosition = _startHeight;
        }
    }

    private void SpawnSegmentPlatforms(MapSegments segment)
    {
        float spawnLimitY = _myTransform.position.y + 20;

        while (_lastPlatformYPosition < spawnLimitY &&
            _lastPlatformYPosition < segment.EndPosition)
        {
            float space = Random.Range(
                _mapGeneratorData.MinimumSpace,
                segment.MaximumSpace
            );

            _lastPlatformYPosition += space;

            SegmentData data = GetWeightedPlatform(segment);
            if (data == null || data.PlatformData == null)
                continue;

            GameObject platform = GetFromPool(data.PlatformData);
            if (platform == null)
                continue;

            platform.transform.position = new Vector3(
                Random.Range(-2.5f, 2.5f),
                _lastPlatformYPosition,
                1f
            );

            platform.transform.SetParent(_platformPoolParent);
            platform.SetActive(true);
        }
    }

    private SegmentData GetWeightedPlatform(MapSegments segment)
    {
        int roll = Random.Range(0, _mapGeneratorData.maxChance);
        int cumulative = 0;

        foreach (var data in segment.PlatformData)
        {
            cumulative += data.PlatformPercentage;
            if (roll < cumulative)
                return data;
        }

        return null;
    }

    private void CreatePool()
    {
        _pool.Clear();

        foreach (var item in _platforms)
        {
            int count = _poolSize;

            if (!item.PlatformData.HasAbility)
                count *= 5;
            else
                count = Mathf.Max(1, count / 2);

            for (int i = 0; i < count; i++)
            {
                GameObject go = Instantiate(item.PlatformData.Prefab, _platformPoolParent);
                go.SetActive(false);
                _pool.Add(go);
            }
        }
    }

    private GameObject GetFromPool(PlatformData platformData)
    {
        foreach (var obj in _pool)
        {
            if (!obj.activeSelf && obj.name.Contains(platformData.Prefab.name))
                return obj;
        }

        return null;
    }
}

[System.Serializable]
public class PlatformTypes
{
    public PlatformEnum Type;
    public PlatformData PlatformData;
}