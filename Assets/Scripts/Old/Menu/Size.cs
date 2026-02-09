using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Size : MonoBehaviour
{
    private int desiredWidth = 850;
    private int desiredHeight = 1360;
    [SerializeField] private GameObject _scoreBoardPanel;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _optionPanel;

    void Start()
    {
        int hard = PlayerPrefs.GetInt("Hardness");
        Debug.Log(hard);
        Time.timeScale = 1;
        StartCoroutine(SetResolutionCoroutine());
        _scoreBoardPanel.SetActive(false);
        _mainMenuPanel.SetActive(true);
    }

    IEnumerator SetResolutionCoroutine()
    {
        yield return new WaitForEndOfFrame();

        Screen.SetResolution(desiredWidth, desiredHeight, FullScreenMode.Windowed);
    }
    public void EnterGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void EnterScoreBoard()
    {
        _scoreBoardPanel.SetActive(true);
        _optionPanel.SetActive(false);
    }
    public void BackToMainMenu()
    {
        _scoreBoardPanel.SetActive(false);
        _optionPanel.SetActive(false);
    }
    public void OnAppQuit()
    {
        Application.Quit();
    }
    public void EnterOption()
    {
        _optionPanel.SetActive(true);
        _scoreBoardPanel.SetActive(false);
    }
}
