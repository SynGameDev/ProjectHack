using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractInfo
{
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

    public List<ScriptableObject> InstalledApplication = new List<ScriptableObject>();

    // Terminal Settings
    public int HHD;             // Storage Space

    // public void AddApp(ScriptableObject app) => InstalledApplication.Add(app);
    

}
