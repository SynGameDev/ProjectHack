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


    // Player Application Level
    public int DownloadLevel = 0;
    public int ContractSpaces = 0;
    public int ExpireTimerLevel = 0;
    public int CompleteTimerLevel = 0;
    public int BruteForceLevel = 0;
    public int SQLLevel = 0;
    public int PhishLevel = 0;


    
}
