using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

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

        // Get the variables in scene & the class values
        settings.Fullscreen = _FullscreenToggle.isOn;
        settings.Resolution = _ScreenSizeDropdown.options[_ScreenSizeDropdown.value].text;
        settings.MusicLevel = _MusicLevelSlider.value;
        settings.SFXLevel = _SFXLevelSlider.value;

        return settings;            // return the new Prefs
    }


    public void SaveSettings() {
        PlayerPrefs settings = CreateNewPrefs();            // Create the prefs

        // Setup Serialization
        JsonSerializer json = new JsonSerializer();         
        json.NullValueHandling = NullValueHandling.Ignore;

        string FilePath = PlayerPrefsController.Instance.GetFilePath();             // Get the path of the file
        
        // Serialize the data
        using(StreamWriter sw = new StreamWriter(FilePath))
        using (JsonWriter writer = new JsonTextWriter(sw))
        {
            json.Serialize(writer, settings);
        }

        PlayerPrefsController.Instance.SetPrefs(settings);          // update the current settings
            
        StartCoroutine(DisplayUpdateMessage());             // Display the update message
    }

    

    private IEnumerator DisplayUpdateMessage() {
        _Message.SetActive(true);                   //Show the message
        yield return new WaitForSeconds(2);         // Wait for 2 seconds
        _Message.SetActive(false);              // Hide the message
    }

    private void UnloadSettingsPref() {
        PlayerPrefs settings = PlayerPrefsController.Instance.GetPrefs(); 

        _FullscreenToggle.isOn = settings.Fullscreen;
        _MusicLevelSlider.value = settings.MusicLevel;
        _SFXLevelSlider.value = settings.SFXLevel;
        SetDropdownList(settings.Resolution);
    }

    /// <summary>
    /// This method will add items to the dropdown list
    /// </summary>
    /// <param name="ActiveRes"></param>
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

