using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerminalInfo {
    
    
    // Terminal Details
    public string TerminalType;                     // Type of the terminal
    public string TerminalIP;                       // IP of the terminal
    
    public List<string> InstalledApplication = new List<string>();
    public List<string> HiddenApplications = new List<string>();

    // File System
    public List<TextFile> TextFileList = new List<TextFile>();

    // Anti Virus
    public int AntiVirusLevel;
    public List<string> BlockedIPs = new List<string>();

    // Application Bools
    public bool BackDoorInstalled;

    // Terminal Settings
    public int HHD;             // Storage Space

    // public void AddApp(ScriptableObject app) => InstalledApplication.Add(app);

    // Objectives
    


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