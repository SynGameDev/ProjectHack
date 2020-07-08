using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;


public class PlayerPrefsController : MonoBehaviour
{
    public static PlayerPrefsController Instance;
    public PlayerPrefs Setting;
    [SerializeField] private string FilePath;

    private void Awake() {
        // Singleton
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }

        LoadSettings();             // Load the settings
    }

    public void SetPrefs(PlayerPrefs settings) {
        Setting = settings;
    }

    public void LoadSettings() {
        if(File.Exists("C:/Dirty Rats/PlayerPrefs.synprefs")) {         // If the file exist

            PlayerPrefs prefs;
            using(StreamReader file = File.OpenText(FilePath))
            {
                JsonSerializer json = new JsonSerializer();
                prefs = (PlayerPrefs)json.Deserialize(file, typeof(PlayerPrefs));
            }

            

            SetPrefs(prefs);
            
            UnloadSettings();               // Unload the data
        } else {
            PlayerPrefs settings = new PlayerPrefs();

            settings.Fullscreen = true;
            settings.Resolution = "1920x1080";
            settings.SFXLevel = 1;
            settings.MusicLevel = 1;

            JsonSerializer serializer = new JsonSerializer();                   // Create the serializer
            serializer.NullValueHandling = NullValueHandling.Ignore;
            Directory.CreateDirectory("C:/Dirty Rats/");                        // Create the directory
            using (StreamWriter sw = new StreamWriter(FilePath))                    // Using Stream writer to create the file
                using (JsonWriter writer = new JsonTextWriter(sw))              // Json Writer to add data to the file
            {
                serializer.Serialize(writer, settings);         // Add the data to the file
            }

            Setting = settings;
            UnloadSettings(); 
        }
    }



    public PlayerPrefs GetPrefs() {
         return Setting;
    }

    public void UnloadSettings() {
        PlayerPrefs settings = GetPrefs();              // Get the current prefs
        
        // Assign the audio details
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioController>().SetAudioLevel(settings.MusicLevel);
        GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioController>().SetAudioLevel(settings.SFXLevel);

        // Setup the resolution & Fullscreen
        string[] resolution = settings.Resolution.Split('x');
        Screen.SetResolution(Convert.ToInt32(resolution[0]), Convert.ToInt32(resolution[1]), settings.Fullscreen);
        Debug.Log(resolution[0] + "x" + resolution[1]);

    }

    public string GetFilePath() => FilePath;
}
