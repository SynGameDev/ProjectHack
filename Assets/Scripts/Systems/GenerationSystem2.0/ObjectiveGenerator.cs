using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using UnityEngine;

public class ObjectiveGenerator
{
    // Action List
    private string[] _SpyObjectActions = {"Install", "Hide", "Retrieve"};
    
    
    
    public List<string> GenerateObjective(string MissionType, ContractInfo Contract)
    {
        List<string> Objectives = new List<string>();

        switch (MissionType)
        {
            case "Spy":
                return GenerateSpyObjectives(Contract.Terminal[0].TerminalIP, Contract);
            default:
                return GenerateSpyObjectives(Contract.Terminal[0].TerminalIP, Contract);
        }
        
        return Objectives;
    }

    private List<string> GenerateSpyObjectives(string ip, ContractInfo Contract)
    {
        List<string> Objectives = new List<string>();

        var TotalNumObjectives = Random.Range(0, 6);
        for (int i = 0; i < TotalNumObjectives; i++)
        {
            Objectives.Add(CreateSpyObjective(ip, Contract));   
        }

        return Objectives;
    }

    private string CreateSpyObjective(string ip, ContractInfo Contract)
    {
        string Action = _SpyObjectActions[Random.Range(0, _SpyObjectActions.Length)];
        string app = "";
        string IP = ip;

        string obj = "";
        switch (Action)
        {
            case "Install":
                obj = "Install " + GetRandomApp() + " IP: " + IP;
                break;
            case "Hide":
                obj = "Hide " + GetRandomApp() + " IP: " + IP;
                break;
            case "Retrieve":
                obj = "Retrieve " + GenerateRandomFile(Contract) + " IP: " + IP;
                break;
        }
        
        

        return Action + " " + app + " " + " IP: " + IP;
    }

    private string GetRandomApp()
    {
        return ApplicationDatabase.Instance.GetSoftwareApps()[
            Random.Range(0, ApplicationDatabase.Instance.GetSoftwareApps().Count)].ApplicationName;
        
        // TODO: Replace with cracked applications
    }

    private string GenerateRandomFile(ContractInfo Contract)
    {
        return "Temp.file";
    }
}