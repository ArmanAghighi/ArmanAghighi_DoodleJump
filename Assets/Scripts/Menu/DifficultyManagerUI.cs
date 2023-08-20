using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DifficultyManagerUI : MonoBehaviour
{
    [SerializeField] private Sprite[] _difficultyImage;
    [SerializeField] private Image _difficultyImageRenderer;
    [SerializeField] private Text _partyText;
    [SerializeField] private Text _normalText;
    [SerializeField] private Text _halloweenText;
    [SerializeField] private Text _nightmareText;
    private Color _originalColor = new Color(0f, 0f, 0f);
    private Color _partySelectedColor = new Color(0.192f, 0.718f, 0.259f);
    private Color _normalSelectedColor = new Color(1f, 0.137f, 0.137f);
    private Color _halloweenSelectedColor = new Color(1f, 0.137f, 0.137f);
    private Color _nightmareSelectedColor = new Color(1f, 0.137f, 0.137f);
    private int _hardnessSetInMenu = 0;
    private void Awake()
    {
        _hardnessSetInMenu = PlayerPrefs.GetInt("Hardness");
        switch (_hardnessSetInMenu)
        {
            case 1:
                OnPartySelected();
                break;
            case 2:
                OnNormalSelected();
                break;
            case 3:
                OnHalloweenSelected();
                break;
            case 4:
                OnNightmareSelected();
                break;
        }
    }
    public void OnPartySelected()
    {
        _hardnessSetInMenu = 1;
        _partyText.color = _partySelectedColor;
        _normalText.color = _originalColor;
        _halloweenText.color = _originalColor;
        _nightmareText.color = _originalColor;
        _difficultyImageRenderer.sprite = _difficultyImage[_hardnessSetInMenu - 1];
        PlayerPrefs.SetInt("Hardness", _hardnessSetInMenu);
    }
    public void OnNormalSelected()
    {
        _hardnessSetInMenu = 2;
        _partyText.color = _originalColor;
        _normalText.color = _normalSelectedColor;
        _halloweenText.color = _originalColor;
        _nightmareText.color = _originalColor;
        _difficultyImageRenderer.sprite = _difficultyImage[_hardnessSetInMenu - 1];
        PlayerPrefs.SetInt("Hardness", _hardnessSetInMenu);
    }
    public void OnHalloweenSelected()
    {
        _hardnessSetInMenu = 3;
        _partyText.color = _originalColor;
        _normalText.color = _originalColor;
        _halloweenText.color = _halloweenSelectedColor;
        _nightmareText.color = _originalColor;
        _difficultyImageRenderer.sprite = _difficultyImage[_hardnessSetInMenu - 1];
        PlayerPrefs.SetInt("Hardness", _hardnessSetInMenu);
    }
    public void OnNightmareSelected()
    {
        _hardnessSetInMenu = 4;
        _partyText.color = _originalColor;
        _normalText.color = _originalColor;
        _halloweenText.color = _originalColor;
        _nightmareText.color = _nightmareSelectedColor;
        _difficultyImageRenderer.sprite = _difficultyImage[_hardnessSetInMenu - 1];
        PlayerPrefs.SetInt("Hardness", _hardnessSetInMenu);
    }
}
