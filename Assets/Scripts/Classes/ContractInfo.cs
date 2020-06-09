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

    public TerminalInfo _Terminal;

    // Contract Status
    public string ContractStatus;                   // Offered, Accepted, Declined, Completed

    
    

}
