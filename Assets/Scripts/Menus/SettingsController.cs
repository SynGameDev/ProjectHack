using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    [Header("Menu Options")]
    [SerializeField] private Toggle _FullscreenToggle;
    [SerializeField] private TMP_Dropdown _ScreenSizeDropdown;
    [SerializeField] private Slider _MusicLevelSlider;
    [SerializeField] private Slider _SFXLevelSlider;
    [SerializeField] private GameObject _Message;

    [Header("Dropdown Settings")]
    public List<string> DropdownItem = new List<string>();

    private void Start() {
        UnloadSettingsPref();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadSceneAsync(0);
        }
    }

    public PlayerPrefs CreateNewPrefs() {
        PlayerPrefs settings = new PlayerPrefs();

        settings.Fullscreen = _FullscreenToggle.isOn;
        settings.WindowSize = _ScreenSizeDropdown.options[_ScreenSizeDropdown.value].text;
        settings.MusicLevel = _MusicLevelSlider.value;
        settings.SFXLevel = _SFXLevelSlider.value;

        return settings;
    }


    public void SaveSettings() {
        PlayerPrefs settings = CreateNewPrefs();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerPrefs.synprefs");
        bf.Serialize(file, settings);

        StartCoroutine(DisplayUpdateMessage());
        PlayerPrefsController.Instance.SetPrefs(settings);
        UnloadSettingsPref();
        PlayerPrefsController.Instance.UnloadSettings();
    }

    

    private IEnumerator DisplayUpdateMessage() {
        _Message.SetActive(true);
        yield return new WaitForSeconds(2);
        _Message.SetActive(false);
    }

    private void UnloadSettingsPref() {
        PlayerPrefs settings = PlayerPrefsController.Instance.GetPrefs(); 

        _FullscreenToggle.isOn = settings.Fullscreen;
        _MusicLevelSlider.value = settings.MusicLevel;
        _SFXLevelSlider.value = settings.SFXLevel;
        SetDropdownList(settings.WindowSize);
    }


    private void SetDropdownList(string ActiveRes) {
        List<string> options = new List<string>();
        options.Add(ActiveRes);

        foreach(var option in DropdownItem) {
            if(option != ActiveRes) {
                options.Add(option);
            }
        }

        _ScreenSizeDropdown.AddOptions(options);


    }
}
