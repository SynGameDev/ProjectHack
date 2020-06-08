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

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Y)) {
            SaveGame();
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

    private void SaveGame() {
        SaveData SaveGame = CreateSaveObject();

        // TODO: Display Saving Game Screen

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + SaveGame.SaveName);
        bf.Serialize(file, SaveGame);


        Debug.Log("Game Saved");

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
