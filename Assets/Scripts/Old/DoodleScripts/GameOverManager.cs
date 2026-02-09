using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    [SerializeField] Text _yourScore;
    [SerializeField] Text _yourHighScore;
    [SerializeField] Text _yourName;
    public Transform leaderboardParent;
    public bool _isGameOver;
    private int _inGameOverHigherScore;
    private string _temp = "temp";
    private void Awake()
    {
        // Find the GameObject with the tag "MyScriptHolder" in Scene 1
        GameObject scriptHolder = GameObject.FindWithTag("PlayerNameMainMenu");
    }
    private void Start()
    {
        GameIsOver();
    }

    void Update()
    {
        _isGameOver = FindObjectOfType<NewDoodleControllerSystem>()._gameIsOver;
        if (_isGameOver)
        {
            GameIsOver();
        }
    }

    public void GameIsOver()
    {
        if (_isGameOver)
        {
            _inGameOverHigherScore = GameObject.Find("Menu").GetComponent<UIManager>()._setHigherScore;
            _yourHighScore.text = PlayerPrefs.GetInt("HighestScore").ToString();
            _yourScore.text = _inGameOverHigherScore.ToString();
            if (PlayerNameManager.playerName != "Player Name")
            {
                _yourName.text = PlayerNameManager.playerName;
            }
            else
            {
                PlayerNameManager.playerName = "Doodle";
            }
            PlayerPrefs.SetInt("NewRecord", int.Parse(_yourScore.text));
            PlayerPrefs.SetString("NewName", PlayerNameManager.playerName);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}



/*Debug.Log("old playere10 score" + PlayerPrefs.GetInt("Player10"));
Debug.Log("your Score :" + _inGameOverHigherScore);
if (_inGameOverHigherScore > PlayerPrefs.GetInt("Player10"))
{
    PlayerPrefs.SetInt("ZPlayer10", _inGameOverHigherScore);
    Debug.Log("New playere10 score" + PlayerPrefs.GetInt("Player10"));
    PlayerPrefs.SetString("ZPlayer10", PlayerNameManager.playerName);
    for (int i = 10; i >= 1; i--)
    {
        if (PlayerPrefs.GetInt("ZPlayer" + i) > PlayerPrefs.GetInt("ZPlayer" + (i - 1)))
        {
            PlayerPrefs.SetInt(_temp, PlayerPrefs.GetInt("ZPlayer" + (i - 1)));
            PlayerPrefs.SetString(_temp, PlayerPrefs.GetString("ZPlayer" + (i - 1)));
            PlayerPrefs.SetInt("ZPlayer" + (i - 1), PlayerPrefs.GetInt("ZPlayer" + i));
            PlayerPrefs.SetString("ZPlayer" + (i - 1), PlayerPrefs.GetString("ZPlayer" + i));
            PlayerPrefs.SetInt("ZPlayer" + i, PlayerPrefs.GetInt(_temp));
            PlayerPrefs.SetString("ZPlayer" + i, PlayerPrefs.GetString(_temp));
        }
        else
        {

            Debug.Log("PlayerName :" + PlayerPrefs.GetString("ZPlayer" + i) + " .Score is :" + PlayerPrefs.GetInt("ZPlayer" + i));
            break;
        }

    }
}*/

