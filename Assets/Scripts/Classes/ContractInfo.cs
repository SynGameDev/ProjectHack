using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ContractInfo
{
    // Controller Settings
    [System.NonSerialized]
     public GameObject ContractButton;

    // Contract Details
    public int ContractID;
    public string ContractName;                     // Name of contract
    public string ContractOwner;                    // Person / company offering the contract
    public string ContractMessage;                  // What needs to be completed in the contract
    public string ContractSubject;

    // Terminal Details
    public string TerminalType;                     // Type of the terminal
    public string TerminalIP;                       // IP of the terminal

    // Contract Status
    public string ContractStatus;                   // Offered, Accepted, Declined, Completed

    public List<string> InstalledApplication = new List<string>();
    public List<string> HiddenApplications = new List<string>();

    // Terminal Settings
    public int HHD;             // Storage Space

    // public void AddApp(ScriptableObject app) => InstalledApplication.Add(app);

    // Objectives
    public List<string> Objective = new List<string>();
    public List<string> ActionLog = new List<string>();               // Record Data Completed on terminal

    public ScriptableObject GetApplication(string AppID) {
        foreach(var id in InstalledApplication) {
            if(id == AppID) {
                return ApplicationDatabase.Instance.GetApplication(id);
            }
        }

        foreach(var id in HiddenApplications) {
            if(id == AppID) 
                return ApplicationDatabase.Instance.GetApplication(id);
        }

        return null;
    }
    

}
