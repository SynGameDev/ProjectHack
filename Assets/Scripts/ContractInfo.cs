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
    

}
