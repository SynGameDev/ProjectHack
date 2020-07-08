using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveGameSystem : MonoBehaviour
{

    public static SaveGameSystem Instance;


    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
    
    public SaveData CreateSaveObject() {
        SaveData save = new SaveData();
        save.SaveName = GameController.Instance.GetPlayerData().PlayerName + ".synsave";
        save.PlayerInfo = GameController.Instance.GetPlayerData();

        foreach(var Contract in GameController.Instance.GetAvailableContracts()) {
            save.AvailableContracts.Add(Contract);
        }
        

        save.ActiveContract = GameController.Instance.GetActiveContract();
        save.Hour = DateTimeController.Instance.GetHour();
        save.Minute = DateTimeController.Instance.GetMin();
        // Date
        save.Day = DateTimeController.Instance.GetDay();
        save.Month = DateTimeController.Instance.GetMonth();
        save.Year = DateTimeController.Instance.GetYear();

        return save;

    }

    public void SaveGame(string name) {
        SaveData SaveGame = CreateSaveObject();

        // TODO: Display Saving Game Screen

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create("C:/Dirty Rats/" + name + ".synsave");
        bf.Serialize(file, SaveGame);


        Debug.Log("Game Saved | " + Application.persistentDataPath);

    }

    public void LoadGame(string filename) {
        var NameOfFile = filename + "synsave";
        if(File.Exists("C:/Dirty Rats/" + NameOfFile)) {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + NameOfFile, FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();

            UnloadData(save);

        }
    }


    private void UnloadData(SaveData save) {
        GameController.Instance.LoadPlayer(save.PlayerInfo);

        foreach(var contract in save.AvailableContracts) {
            GameController.Instance.AddContract(contract);
        }

        GameController.Instance.SetActiveContract(save.ActiveContract);

        DateTimeController.Instance.SetTime(save.Hour, save.Minute, save.Day, save.Month, save.Year);

        Debug.Log("Game loaded");
    }
}

[System.Serializable]
public class SaveData {

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

    // Player PC Data




}
