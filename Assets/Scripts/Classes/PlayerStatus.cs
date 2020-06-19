using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatus
{


    public int PlayerDay;
    public string PlayerName;
    //public Sprite PlayerIcon = null;
    public int coin = 2;

    // PC Data
    public int StorageSpace = 30;
    public int RamSpace = 352;
    // TODO: Add User Applications

    public List<ContractInfo> CompletedContracts = new List<ContractInfo>();
    
    // Daily Stats
    public List<ContractInfo> ContractsCompletedToday = new List<ContractInfo>();
    public List<ContractInfo> ContractsOffered = new List<ContractInfo>();

    public List<TerminalInfo> UnlockedTerminal = new List<TerminalInfo>();
    
}
