using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class NewContractSystem 
{

    public ContractInfo CreateNewContract()
    {
        // Generators
        AceTechGenerator AceTech = new AceTechGenerator();
        TerminalGenerator TerminalGen = new TerminalGenerator();
        ObjectiveGenerator ObjectiveGen = new ObjectiveGenerator();
        
        // Classes
        ContractInfo Contract = new ContractInfo();            // Setup the new contract
        AceTechAccount Account = new AceTechAccount();

        // Set the basic details
        Contract.ContractID = GameController.Instance.GetNextContractID();
        Contract.ContractName = "temp" + Contract.ContractID;
        Contract.ContractOwner = AceTech.GenerateAccount();
        Contract.ContractMessage = "Hey,\n Please complete the below missions \n thanks";
        Contract.ContractSubject = MissionType();
        Contract.Terminal.Add(TerminalGen.GenerateTerminal());
        Contract.MainObjectives = ObjectiveGen.GenerateObjective(Contract.ContractSubject, Contract);
        // Set the difficulty
        Contract.ContractDifficulty = 1;
        // Setup the status
        Contract.ContractStatus = "Pending";
        
        return Contract;
    }

    public string MissionType()
    {
        string type = "";
        string[] MissionTypes = {"Spy", "Data Retrieval", "Brute Force", "Phishing", "DOS"};            // Type of missions
        return MissionTypes[Random.Range(0, MissionTypes.Length)];
    }
}
