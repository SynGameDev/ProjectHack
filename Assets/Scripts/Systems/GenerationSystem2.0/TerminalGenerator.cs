using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TerminalGenerator
{
    public TerminalInfo GenerateTerminal()
    {
        AceTechGenerator AceTech = new AceTechGenerator();            // Generator
        
        TerminalInfo Terminal = new TerminalInfo();                // New Temrinal

        Terminal.EmailAccount = AceTech.GenerateAccount();                // Setup the account
        Terminal.TemrinalName = GenerateTerminalName(Terminal.EmailAccount);
        Terminal.TerminalType = "Desktop";                    // TODO: Replace with the type of contract that is being completed.
        Terminal.TerminalIP = GenerateTerminalIP();
        Terminal.AntiVirusLevel = 0;                        // TODO: Replace Based on the difficulty of the contract
        Terminal.BackDoorInstalled = false;
        Terminal.HHD = Random.Range(0, 245);            // Setup the HD Space
        
        GameController.Instance.AddNewTerminal(Terminal);
        return Terminal;
    }

    private string GenerateTerminalName(AceTechAccount Account)
    {
        var value = Random.Range(0, 2);                // Value
        // Determine the Terminal Name base on a random value
        switch (value)
        {
            case 0:
                return Account.FirstName + "_PC";
            case 1:
                return Account.FirstName.Substring(0, 1) + Account.LastName + "_PC";
            case 2:
                return Account.Username;
            default:
                return Account.FirstName + "_PC";
        }
        
        return "Public Terminal";
    }

    private string GenerateTerminalIP()
    {
        string IP = "";
        
        var ValidIP = false;
        do
        {
            var temp1 = Random.Range(10, 1000);
            var temp2 = Random.Range(100, 1000);
            var temp3 = Random.Range(100, 1000);

            IP = temp1.ToString() + "." + temp2.ToString() + "." + temp3;
            ValidIP = ValidateIP(IP);
        } while (!ValidIP);
        
        return IP;
    }

    private bool ValidateIP(string ip)
    {
        foreach (var item in GameController.Instance.GetAllTerminals())
        {
            if (item.TerminalIP == ip)
                return false;
        }

        return true;
    }
}