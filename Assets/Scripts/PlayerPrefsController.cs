using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    public static PlayerPrefsController Instance;
    private PlayerPrefs Setting;

    private void Awake() {
        if(Instance != null) {
            Instance  = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    public void SetPrefs(PlayerPrefs settings) {
        Setting = settings;
    }

    public void LoadSettings() {
        if(File.Exists(Application.persistentDataPath + "/PlayerPrefs.synprefs")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerPrefs.synprefs", FileMode.Open);
            PlayerPrefs settings = (PlayerPrefs)bf.Deserialize(file);
            file.Close();

            PlayerPrefsController.Instance.SetPrefs(settings);
            PlayerPrefsController.Instance.UnloadSettings();
        } else {
            PlayerPrefs settings = new PlayerPrefs();

            settings.Fullscreen = true;
            settings.WindowSize = "1920x1080";
            settings.SFXLevel = 1;
            settings.MusicLevel = 1;

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/PlayerPrefs.synprefs");
            bf.Serialize(file, settings);

        }
    }

    public PlayerPrefs GetPrefs() => Setting;

    public void UnloadSettings() {
        PlayerPrefs settings = PlayerPrefsController.Instance.GetPrefs();
        
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioController>().SetAudioLevel(settings.MusicLevel);
        GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioController>().SetAudioLevel(settings.SFXLevel);

        string[] resolution = settings.WindowSize.Split('x');
        Screen.SetResolution(Convert.ToInt32(resolution[1]), Convert.ToInt32(resolution[2]), settings.Fullscreen);

    }
}
