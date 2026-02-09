using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private bool isSoundOn = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isSoundOn = PlayerPrefs.GetInt("IsSoundOn", 1) == 1;

        ApplySoundState();
    }

    private void ApplySoundState()
    {
        AudioListener.pause = !isSoundOn;
    }

    public void ToggleSound(bool isSoundOff)
    {
        isSoundOn = !isSoundOff;

        ApplySoundState();

        PlayerPrefs.SetInt("IsSoundOn", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}