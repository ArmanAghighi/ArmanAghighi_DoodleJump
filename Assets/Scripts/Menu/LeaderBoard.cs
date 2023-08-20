using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeaderBoard : MonoBehaviour
{
    [SerializeField] Text[] _playerScore;
    [SerializeField] Text[] _playerName;
    private void Awake()
    {

        for (int i = 9; i > 0; i--)
        {
            if (int.Parse(_playerScore[i].text) > int.Parse(_playerScore[i - 1].text))
            {
                int temp_Score = int.Parse(_playerScore[i - 1].text);
                string temp_Name = _playerName[i - 1].text;
                _playerScore[i - 1].text = _playerScore[i].ToString();
                _playerName[i - 1].text = _playerName[i].text;
                _playerScore[i].text = temp_Score.ToString();
                _playerName[i].text = temp_Name;
                PlayerPrefs.SetInt("ZPlayer" + i, int.Parse(_playerScore[i].text));
                PlayerPrefs.SetString("ZPlayer" + i, _playerName[i].text);
                PlayerPrefs.SetInt("ZPlayer" + (i - 1),int.Parse(_playerScore[i - 1].text));
                PlayerPrefs.SetString("ZPlayer" + (i - 1), _playerName[i].text);
            }
            else
                break;
        }
    }
    private void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            if(int.Parse(_playerScore[i].text) == 0)
            {
                _playerName[i].text = "Player" + (i + 1);
                _playerScore[i].text = "0";
            }
            else
            {
                _playerName[i].text = PlayerPrefs.GetString("ZPlayer" + i);
                _playerScore[i].text = PlayerPrefs.GetInt("ZPlayer" + i).ToString();
            }
        }
    }
}