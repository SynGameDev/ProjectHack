using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerminal 
{
    public TerminalInfo GenerateContract()
    {
        return CreateTerminal("Desktop");
    }

    private TerminalInfo CreateTerminal(string type)
    {
        TerminalInfo NewTerminal = new TerminalInfo();                // Create the new terminal
        
        
        NewTerminal.TerminalIP = GenerateIP();
        NewTerminal.TerminalType = type;
        NewTerminal.TemrinalName = GenerateTerminalName(type);
        NewTerminal.HHD = UnityEngine.Random.Range(2, 230);

        return NewTerminal;
    }

    private string GenerateIP()
    {
        string num_1 = UnityEngine.Random.Range(10, 1000).ToString();
        string num_2 = UnityEngine.Random.Range(100, 1000).ToString();
        string num_3 = UnityEngine.Random.Range(100, 1000).ToString();

        return num_1 + "." + num_2 + "." + num_3;
    }

    private string GenerateTerminalName(string type)
    {
        return "";
    }

}
