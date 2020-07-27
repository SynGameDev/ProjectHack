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
    public AceTechAccount ContractOwner;                    // Person / company offering the contract
    public string ContractMessage;                  // What needs to be completed in the contract
    public string ContractSubject;

    public int ContractDifficulty;

    public List<TerminalInfo> Terminal = new List<TerminalInfo>();

    // Contract Status
    public string ContractStatus;                   // Offered, Accepted, Declined, Completed
    public float TimeToExpire = 4;
    public float TimeToComplete = 4;

    public List<string> Objective = new List<string>();
    public List<string> MainObjectives = new List<string>();
    public List<string> ActionLog = new List<string>();               // Record Data Completed on terminal
    
    

}
