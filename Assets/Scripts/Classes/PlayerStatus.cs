using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{


    public string PlayerName;

    public List<ContractInfo> CompletedContracts = new List<ContractInfo>();

    // Daily Stats
    public List<ContractInfo> ContractsCompletedToday = new List<ContractInfo>();
    
}
