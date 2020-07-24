using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission
{
    // Sequence Details
    public string Sequence;
    public int MissionID;
    // Mission Details
    public string MissionName;
    public string MissionOwner;
    public List<TerminalInfo> ContractTerminals = new List<TerminalInfo>();
    public string ContractMessage;

    // Mission Status
    public string MissionStatus;

    // Contract
    public ContractInfo MissionContract;
}
