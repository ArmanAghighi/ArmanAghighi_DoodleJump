using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    
    //PlayerScore Variables
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _HighestScoreText;
    private int _HighestScore;
    private int _playerHeight;
    public int _setHigherScore = 0;
    //Pause Variables
    [SerializeField] private GameObject _pausePanel;
    private bool _isPaused = false;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("HighestScore"))
        {
            PlayerPrefs.SetInt("HighestScore", 0);
        }
        else
            _HighestScore = PlayerPrefs.GetInt("HighestScore");
        SetScoreToTextAwakeSystem();
    }
    private void Update()
    {
        SetScoreToTextUpdateSystem();
        PausePanelUpdateSystem();
        SetHighestScoreToHighestTextUpdateSystem();
    }
    private void SetScoreToTextAwakeSystem()
    {
        Transform PlayerObject = _playerObject.GetComponent<Transform>();
    }
    private void SetScoreToTextUpdateSystem()
    {
        _playerHeight = (int)_playerObject.transform.position.y;
        if (_playerHeight > _setHigherScore)
            _setHigherScore = _playerHeight;
        if (_playerHeight < 0)
            _playerHeight = 0;
        _scoreText.text = _setHigherScore.ToString();
    }
    private void SetHighestScoreToHighestTextUpdateSystem()
    {
        if(_HighestScore < _setHigherScore)
        {
            PlayerPrefs.SetInt("HighestScore", _setHigherScore);
            _HighestScoreText.text = _setHigherScore.ToString();
        }
        else
        {
            _HighestScoreText.text = PlayerPrefs.GetInt("HighestScore").ToString();
        }
    }
    private void PausePanelUpdateSystem()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
            {
                _pausePanel.SetActive(true);
                _isPaused = true;
                Time.timeScale = 0;
            }
            else
            {
                _pausePanel.SetActive(false);
                _isPaused = false;
                Time.timeScale = 1;
            }
        }
    }
    public void ResumePause()
    {
        _pausePanel.SetActive(false);
        _isPaused = false;
        Time.timeScale = 1;
    }
    public void Pause()
    {
        _pausePanel.SetActive(true);
        _isPaused = true;
        Time.timeScale = 0;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

}
