using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerminalGenerator 
{
    public TerminalInfo NewTerminal;
    
    public void CreateTerminal(int dif)
    {
        NewTerminal = new TerminalInfo();
        NewTerminal.TerminalIP = GenerateIP();
        NewTerminal.HHD = GenerateSpace();
        NewTerminal.AntiVirusLevel = GenerateAntiVirus(dif);
        

        NewTerminal.BackDoorInstalled = false;

    }

    private string GenerateIP()
    {
        var ip_1 = UnityEngine.Random.Range(100, 999).ToString();
        var ip_2 = UnityEngine.Random.Range(100, 999).ToString();
        var ip_3 = UnityEngine.Random.Range(100, 999).ToString();

        var ip = ip_1 + ip_2 + ip_3;

        return ip;

    }

    private int GenerateAntiVirus(int dif)
    {
        if(dif == 0)
        {
           var num = UnityEngine.Random.Range(0, 25);

            if(num <= 10)
            {
                return 0;
            } else if(num <= 20)
            {
                return 1;
            } else if(num == 21 || num == 22)
            {
                return 2;
            } else if(num == 23)
            {
                return 3;
            } else
            {
                return 4;
            }
        } else if(dif == 1)
        {
            var num = UnityEngine.Random.Range(0, 51);

            if(num <= 10)
            {
                return 0;
            } else if(num <= 30)
            {
                return 1;
            } else if(num <= 40)
            {
                return 2;
            } else if(num <= 48)
            {
                return 3;
            } else
            {
                return 4;
            }
        } else
        {
            var num = UnityEngine.Random.Range(0, 100);

            if(num <= 10)
            {
                return 0;
            } else if(num <= 20)
            {
                return 1;
            } else if(num <= 45)
            {
                return 2;
            } else if(num <= 70)
            {
                return 3;
            } else
            {
                return 4;
            }
        }



        return 0;
    }

    private int GenerateSpace()
    {
        return UnityEngine.Random.Range(10, 1000);
    }


}
