using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveGameSystem : MonoBehaviour
{
    public static SaveGameSystem Instance;

    private void Awake()
    {
        // Singleton instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public SaveData CreateSaveObject()
    {
        // Create the save object
        SaveData save = new SaveData();
        save.SaveName = GameController.Instance.GetPlayerData().PlayerName + ".synsave";
        save.PlayerInfo = GameController.Instance.GetPlayerData();

        // Foreach available contract add it to the 
        save.AvailableContracts = GameController.Instance.GetAvailableContracts();
        // Get the active contract
        save.ActiveContract = GameController.Instance.GetActiveContract();
        
        // Current Time
        save.Hour = DateTimeController.Instance.GetHour();
        save.Minute = DateTimeController.Instance.GetMin();
        // Date
        save.Day = DateTimeController.Instance.GetDay();
        save.Month = DateTimeController.Instance.GetMonth();
        save.Year = DateTimeController.Instance.GetYear();

        // Mission Details
        save.Sequences = MissionDatabase.Instance.GetSequences();
        save.Missions = MissionDatabase.Instance.GetMissions();
        save.CurrentMission = MissionDatabase.Instance.GetCurrentMission();
        save.CurrentSequence = MissionDatabase.Instance.GetCurrentSequence();
        
        // Return the curernt data
        return save;
    }

    public void SaveGame(string name)
    {
        SaveData SaveGame = CreateSaveObject();                // Generate a new save object

        // TODO: Display Saving Game Screen

        BinaryFormatter bf = new BinaryFormatter();            // New Formatter
        FileStream file = File.Create("C:/Dirty Rats/" + name + ".synsave");        // Create the save file
        bf.Serialize(file, SaveGame);                // Serailze the object

        Debug.Log("Game Saved | " + Application.persistentDataPath); 
    }

    public void LoadGame(string filename)
    {
        var NameOfFile = filename + "synsave";            // Get the name of the load game
        
        // Make sure that the file is there & load that file
        if (File.Exists("C:/Dirty Rats/" + NameOfFile))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + NameOfFile, FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();

            UnloadData(save);            // unload the data
        }
    }

    private void UnloadData(SaveData save)
    {
        GameController.Instance.LoadPlayer(save.PlayerInfo);            // Setup the player class

        // TODO: Add Setter in Game Controller to replace for loop
        foreach (var contract in save.AvailableContracts)
        {
            GameController.Instance.AddContract(contract);
        }
        

        GameController.Instance.SetActiveContract(save.ActiveContract);        // Set the available contract

        // Set the data & time
        DateTimeController.Instance.SetTime(save.Hour, save.Minute, save.Day, save.Month, save.Year);

       
        // Load Sequences & Missions
        MissionDatabase.Instance.LoadSequence(save.Sequences, save.CurrentSequence);
        MissionDatabase.Instance.LoadMissions(save.Missions, save.CurrentMission);
        
        
        Debug.Log("Game loaded");
        
        
    }
}

[System.Serializable]
public class SaveData
{
    public string SaveName;

    // Player Details
    public PlayerStatus PlayerInfo;

    // Contract Information
    public List<ContractInfo> AvailableContracts = new List<ContractInfo>();

    public ContractInfo ActiveContract;

    // Time
    public int Hour;

    public float Minute;

    // Date Settings
    public int Day;

    public int Month;
    public int Year;

    // Database Data
    public List<string> Sequences = new List<string>();
    public List<Mission> Missions = new List<Mission>();
    public string CurrentSequence;
    public string CurrentMission;
}