using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGenerator : MonoBehaviour
{
    private NewDoodleControllerSystem _controllerEvents;
    [SerializeField] private Transform _playerObject;
    [SerializeField] private int _distanceForEachSession = 200;
    private int _hardness = 0;
    private int _gameObjectGeneratorSpeed = 20;
    private int[] _defficultyLevel = new int[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
    private int _isOnDefficultyLevel = 1;
    private bool _waitForPlayer = false;
    private float _minYDeploy = 0.5f;
    private float _maxYDeploy = 3.3f;
    private float _xDeploy;
    private float _yDeploy;
    [SerializeField] private GameObject[] _instantiatedPlatform;
    //1._normalPlatformSimple-2._normalPlatformSpring-3._normalPlatformRocket-4._MoverPlatformSimple-5._MoverPlatformSpring-6._MoverPlatformRocket-7._weakPlatformSimple
    private float[] _instantiatedPlatformChanceDeployment;
    [SerializeField] private GameObject[] _instantiatedEnemy;
    //1._Enemy1-2.Enemy2-3.Hole
    const float pi = Mathf.PI;
    float _minXDeploy = 1.1f;
    private void Awake()
    {
        _controllerEvents = GameObject.FindObjectOfType<NewDoodleControllerSystem>();
        preWarm();
        if (PlayerPrefs.HasKey("Hardness"))
            _hardness = PlayerPrefs.GetInt("Hardness");
        else
            _hardness = 1;
        _playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (_playerObject == null)
        {
            Debug.LogError("no PlayerObject attached !");
        }
    }
    void Start()
    {
        if (_hardness == 1)
        {
            StartCoroutine(SpawnEnemy(_hardness));
        }
        else if (_hardness == 2)
        {
            StartCoroutine(SpawnEnemy(_hardness));
        }
        else if (_hardness == 3)
        {
            StartCoroutine(SpawnEnemy(_hardness));
        }
        else if (_hardness == 4)
        {
            StartCoroutine(SpawnEnemy(_hardness));
        }
        _yDeploy = _minYDeploy;
    }

    void Update()
    {
        //if(!_controllerEvents._gameIsOver)
        //{
            SetDefficulty();
            Instantiation();
        //}
    }
    bool isPrefabInCircle(float radius, float x, float y)
    {
        float area = radius * radius * pi;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(x, y), new Vector2(radius, radius), 0f);
        string[] tagsToCheck = new string[2] { "normalPlatform", "moverPlatform" };

        foreach (Collider2D collider in colliders)
        {
            for (int i = 0; i < tagsToCheck.Length; i++)
            {
                if (collider.CompareTag(tagsToCheck[i]))
                {
                    return true;
                }
            }
        }
        return false;
    }
    private void SetDefficulty()
    {
        for (int i = 0; i < _defficultyLevel.Length; i++)
        {
            if (gameObject.transform.position.y > (_isOnDefficultyLevel * _distanceForEachSession) / _hardness) 
            {
                if (gameObject.transform.position.y - _playerObject.transform.position.y < 20 && _waitForPlayer)
                {
                    _isOnDefficultyLevel +=1;
                    _yDeploy += 0.4375f;
                    _waitForPlayer = false;
                    preWarm();
                    break;
                }
                if (gameObject.transform.position.y - _playerObject.transform.position.y >= 20)
                {
                    _waitForPlayer = true;
                    if(_isOnDefficultyLevel == _defficultyLevel[7])
                        _gameObjectGeneratorSpeed = 40;
                    break;
                }
            }
            else if (gameObject.transform.position.y > (_distanceForEachSession/ _hardness) * 8)
            {
                if(gameObject.transform.position.y > _playerObject.transform.position.y + 50 && !_waitForPlayer)
                {
                    _waitForPlayer = true;
                }
                else
                {
                    _waitForPlayer = false;
                    _isOnDefficultyLevel = _defficultyLevel[7];
                    _yDeploy = _maxYDeploy;
                    _gameObjectGeneratorSpeed = 20;

                }
                break;
            }
            break;
        }
    }
    private void Instantiation()
    {
        float _lastX = 0;
        float _lastY = 0;
        _xDeploy = Random.Range(-2.6f, +2.6f);
        if(Mathf.Abs(_xDeploy - _lastX) < 1)
        {
            _xDeploy = Random.Range(-2.6f, +2.6f);
        }
        else
        {
            if (!_waitForPlayer && !isPrefabInCircle(1, _xDeploy, _yDeploy + gameObject.transform.position.y))
            {
                switch (_isOnDefficultyLevel)
                {
                    case 1:
                        Instantiate(CalculateDeploymentChancePlatfor(60f, 10f, 1f, 10f, 1f, 1f, 17f), new Vector3(_xDeploy, _yDeploy + transform.position.y, 0), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(CalculateDeploymentChancePlatfor(55f, 8f, 1f, 15f, 1f, 1f, 19f), new Vector3(_xDeploy, _yDeploy + transform.position.y, 0), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(CalculateDeploymentChancePlatfor(50f, 6f, 2f, 20f, 2f, 2f, 18f), new Vector3(_xDeploy, _yDeploy + transform.position.y, 0), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(CalculateDeploymentChancePlatfor(45f, 4f, 2f, 25f, 3f, 2f, 19f), new Vector3(_xDeploy, _yDeploy + transform.position.y, 0), Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(CalculateDeploymentChancePlatfor(40f, 3f, 3f, 30f, 4f, 3f, 17f), new Vector3(_xDeploy, _yDeploy + transform.position.y, 0), Quaternion.identity);
                        break;
                    case 6:
                        Instantiate(CalculateDeploymentChancePlatfor(35f, 2f, 3f, 35f, 6f, 3f, 16f), new Vector3(_xDeploy, _yDeploy + transform.position.y, 0), Quaternion.identity);
                        break;
                    case 7:
                        Instantiate(CalculateDeploymentChancePlatfor(30f, 1f, 4f, 40f, 8f, 4f, 13f), new Vector3(_xDeploy, _yDeploy + transform.position.y, 0), Quaternion.identity);
                        break;
                    case 8:
                        Instantiate(CalculateDeploymentChancePlatfor(25f, 1f, 5f, 45f, 10f, 5f, 9f), new Vector3(_xDeploy, _yDeploy + transform.position.y, 0), Quaternion.identity);
                        break;
                }
                _lastX = _xDeploy;
                _lastY = _yDeploy;
            }
        }
        if (_playerObject.transform.position.y < gameObject.transform.position.y - 2 && !_waitForPlayer)
            gameObject.transform.Translate(Vector2.up * _gameObjectGeneratorSpeed * Time.deltaTime);
    }
    IEnumerator SpawnEnemy(int Hardness)
    {
        while (true)
        {
            int _enemyRandomizer = Random.Range(0, _instantiatedEnemy.Length);
            float _enemyX = Random.Range(-2.6f, 2.6f);
            Instantiate(_instantiatedEnemy[_enemyRandomizer], new Vector3(_enemyX, gameObject.transform.position.y, 0), Quaternion.identity);
            if (Hardness == 1)
                yield return new WaitForSeconds(9f);
            if (Hardness == 2)
                yield return new WaitForSeconds(6f);
            if (Hardness == 3)
                yield return new WaitForSeconds(4f);
            if (Hardness == 4)
                yield return new WaitForSeconds(2f);
        }
    }
    private GameObject CalculateDeploymentChancePlatfor(float NPSimple, float NPSpring, float NPRocket, float MPSimple, float MPSpring, float MPRocket, float WPSimple)
    {
        _instantiatedPlatformChanceDeployment = new float[7] { NPSimple, NPSpring, NPRocket, MPSimple, MPSpring, MPRocket , WPSimple };
        float _totalinstantiatedPlatformChanceDeployment = 0f;
        foreach (float weight in _instantiatedPlatformChanceDeployment)
        {
            _totalinstantiatedPlatformChanceDeployment += weight;
        }
        float _randomChance = Random.Range(0f, _totalinstantiatedPlatformChanceDeployment);

        GameObject _selectedPlatform = null;

        float cumulativeWeight = 0f;

        for (int i = 0; i < _instantiatedPlatform.Length; i++)
        {
            cumulativeWeight += _instantiatedPlatformChanceDeployment[i];

            if (_randomChance <= cumulativeWeight)
            {
                _selectedPlatform = _instantiatedPlatform[i];
                break;
            }
        }
        return _selectedPlatform;
    }
    Vector2 converScreenToWorld()
    {
        Camera mainCamera = Camera.main;
        Vector2 screenPosition = new Vector2(Screen.width, Screen.height);
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    int findTheBestNumberForInstantiation()
    {
        Camera mainCamera = Camera.main;
        Vector2 screenPosition = new Vector2(Screen.width, Screen.height);
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        int numberOfPlatformToBeInstantiate = ((int)worldPosition.x + (int)worldPosition.y);
        return numberOfPlatformToBeInstantiate + 3;
    }
    void preWarm()
    {
        for (int i = 0; i < findTheBestNumberForInstantiation(); i++)
        {
            float newXForPreWarm = UnityEngine.Random.Range(-converScreenToWorld().x + 1, converScreenToWorld().x - 1);
            float newYForPreWarm = UnityEngine.Random.Range(gameObject.transform.position.y, gameObject.transform.position.y + 15 );
            if (!isPrefabInCircle(1 + _yDeploy, newXForPreWarm, newYForPreWarm))
            {
                int whatToInstantiateInPrefab = UnityEngine.Random.Range(0, 6);
                GameObject NormalPlatform = Instantiate(_instantiatedPlatform[whatToInstantiateInPrefab], new Vector3(newXForPreWarm, newYForPreWarm, 0), gameObject.transform.rotation);
            }
        }
    }
}
