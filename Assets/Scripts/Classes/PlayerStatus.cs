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
    public int RankedPoints = 0;

    // PC Data
    public int StorageSpace = 30;
    public int RamSpace = 352;
    // TODO: Add User Applications

    public List<ContractInfo> CompletedContracts = new List<ContractInfo>();
    
    // Daily Stats
    public List<ContractInfo> ContractsCompletedToday = new List<ContractInfo>();
    public List<ContractInfo> ContractsOffered = new List<ContractInfo>();

    public List<TerminalInfo> UnlockedTerminal = new List<TerminalInfo>();

    public List<EndDayClass> EndOfDayStats = new List<EndDayClass>();

    // Terminal Data
    public float DownloadSpeedMutliplier = 1;
    public float ContractSpace = 10;
    public float ContractExpireMultiplier = 1;
    public float ContractCompleteMultiplier = 1;


    // Player Applications
    public int bruteForceLevel = 0;
    public int SQLInjectionLevel = 0;
    public int PhishLevel = 0;
    
}
