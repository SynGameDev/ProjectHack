using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerminalInfo {
    
    
    // Terminal Details
    public string TemrinalName;
    public string TerminalType;                     // Type of the terminal
    public string TerminalIP;                       // IP of the terminal
    
    public List<ApplicationClass> InstalledApplication = new List<ApplicationClass>();
    public List<ApplicationClass> HiddenApplications = new List<ApplicationClass>();

    // File System
    public List<TextFile> TextFileList = new List<TextFile>();
    public EmailAccount EmailAccount;

    // Anti Virus
    public int AntiVirusLevel;
    public List<string> BlockedIPs = new List<string>();

    // Application Bools
    public bool BackDoorInstalled;

    // Terminal Settings
    public int HHD;             // Storage Space

    // public void AddApp(ScriptableObject app) => InstalledApplication.Add(app);

    // Objectives
    


    public ApplicationClass GetApplication(string AppID) {
        foreach(var id in InstalledApplication) {
            if(id.ApplicationID == AppID) {
                return id;
            }
        }

        foreach(var id in HiddenApplications) {
            if(id.ApplicationID == AppID) 
                return id;
        }

        return null;
    }
}