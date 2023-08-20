using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundMangerUI : MonoBehaviour
{
    [SerializeField] private Button _musicOn;
    [SerializeField] private Button _musicOff;
    [SerializeField] private Text _musicOnText;
    [SerializeField] private Text _musicOffText;
    private Color _OnselectedColor = new Color(0.192f, 0.718f, 0.259f);
    private Color _OffselectedColor = new Color(1f, 0.137f, 0.137f);
    private Color _originalColor = new Color(0f, 0f, 0f);
    private bool _isMuted = false;
    public SoundManager _soundManger;
    private void Start()
    {
        _soundManger = gameObject.GetComponent<SoundManager>();
        _musicOnText = GameObject.FindGameObjectWithTag("MusicButtonOn").GetComponentInChildren<Text>();
        _musicOffText = GameObject.FindGameObjectWithTag("MusicButtonOff").GetComponentInChildren<Text>();
        if (PlayerPrefs.GetInt("IsSoundOn") == 1)
            OnMusicButtonSelected();
        else
            OffMusicButtonSelected();
    }
    public void OnMusicButtonSelected()
    {
        _musicOnText.color = _OnselectedColor;
        _musicOffText.color = _originalColor;
        SoundManager.Instance.ToggleSound(false);
    }
    public void OffMusicButtonSelected()
    {
        _musicOffText.color = _OffselectedColor;
        _musicOnText.color = _originalColor;
        SoundManager.Instance.ToggleSound(true);
    }
}
