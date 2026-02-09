using UnityEngine;
using UnityEngine.UI;

public class PlayerNameManager : MonoBehaviour
{
    public InputField playerNameInput;
    public static string playerName;
    private int _isSavedEmpty = 0;
    string _playerName1 = "Player1";
    string _playerName2 = "Player2";
    string _playerName3 = "Player3";
    string _playerName4 = "Player4";
    string _playerName5 = "Player5";
    string _playerName6 = "Player6";
    string _playerName7 = "Player7";
    string _playerName8 = "Player8";
    string _playerName9 = "Player9";
    string _playerName10="Player10";
    int FirstValue = 0;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("_isSavedEmpty") == 0)
        {
            Debug.Log("Done");
            PlayerPrefs.SetString("ZPlayer1", _playerName1);
            PlayerPrefs.SetString("ZPlayer2", _playerName2);
            PlayerPrefs.SetString("ZPlayer3", _playerName3);
            PlayerPrefs.SetString("ZPlayer4", _playerName4);
            PlayerPrefs.SetString("ZPlayer5", _playerName5);
            PlayerPrefs.SetString("ZPlayer6", _playerName6);
            PlayerPrefs.SetString("ZPlayer7", _playerName7);
            PlayerPrefs.SetString("ZPlayer8", _playerName8);
            PlayerPrefs.SetString("ZPlayer9", _playerName9);
            PlayerPrefs.SetString("ZPlayer10", _playerName10);
            PlayerPrefs.SetInt("ZPlayer1", FirstValue);
            PlayerPrefs.SetInt("ZPlayer2", FirstValue);
            PlayerPrefs.SetInt("ZPlayer3", FirstValue);
            PlayerPrefs.SetInt("ZPlayer4", FirstValue);
            PlayerPrefs.SetInt("ZPlayer5", FirstValue);
            PlayerPrefs.SetInt("ZPlayer6", FirstValue);
            PlayerPrefs.SetInt("ZPlayer7", FirstValue);
            PlayerPrefs.SetInt("ZPlayer8", FirstValue);
            PlayerPrefs.SetInt("ZPlayer9", FirstValue);
            PlayerPrefs.SetInt("ZPlayer10",FirstValue);
            _isSavedEmpty = 1;
            PlayerPrefs.SetInt("_isSavedEmpty", _isSavedEmpty);
        }
    }

    public void SavePlayerName()
    {
        playerName = playerNameInput.text;

        if (string.IsNullOrEmpty(playerName))
        {
            Debug.LogWarning("Player name is empty or null.");
            return;
        }

        // Check if the player name already exists
        if (PlayerPrefs.HasKey(playerName))
        {
            Debug.LogWarning("Player name already exists. Please enter a unique name.");
            return;
        }
        for (int i = 0; i < 100; i++)
        {
            if (PlayerPrefs.GetString("playerName" + i) == null)
            {
                PlayerPrefs.SetString(playerName, playerName.ToString());
                PlayerPrefs.Save();
                break;
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}