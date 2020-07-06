using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [Header("Menu Options")]
    [SerializeField] private bool _IsFullscreen;
    [SerializeField] private TMP_Dropdown _ScreenSizeDropdown;
    [SerializeField] private Slider _MusicLevelSlider;
    [SerializeField] private Slider _SFXLevelSlider;

    public PlayerPrefs CreateNewPrefs() {
        PlayerPrefs settings = new PlayerPrefs();

        settings.Fullscreen = _IsFullscreen;
        settings.WindowSize = _ScreenSizeDropdown.value;
        settings.MusicLevel = _MusicLevelSlider.value;
        settings.SFXLevel = _SFXLevelSlider.value;

        return settings;
    }

    public void SaveSettings() {
        PlayerPrefs settings = CreateNewPrefs();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerPrefs.synprefs");
        bf.Serialize(file, settings);

        // TODO: Display Updated Settings Information

    }
}
