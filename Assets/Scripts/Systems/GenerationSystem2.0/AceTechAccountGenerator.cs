using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AceTechGenerator
{
    public AceTechAccount GenerateAccount()
    {
        AceTechAccount Account = new AceTechAccount();
        List<string> GeneratedNames = GenerateNames();

        Account.FirstName = GeneratedNames[0];
        Account.LastName = GeneratedNames[1];
        Account.Username = GeneratedNames[2];
        
        // TODO: Decide whether to use a current account or make a new one.
        
        // TODO: Generate Sent Emails
        // TODO: Generate Received Emails
        // TODO: Generate From Emails
        
        return Account;
    }

    public List<string> GenerateNames()
    {
        List<string> Names = new List<string>();
        NamesData Data = new NamesData();

        using (StreamReader r = new StreamReader(Application.streamingAssetsPath + "Databases/NamesDatabase.json"))
        {
            Data = JsonUtility.FromJson<NamesData>(r.ReadToEnd());
        }

        Names.Add(Data.FirstNames[Random.Range(0, Data.FirstNames.Count)]);
        Names.Add(Data.LastNames[Random.Range(0, Data.LastNames.Count)]);
        Names.Add(Data.Usernames[Random.Range(0, Data.Usernames.Count)]);                // TODO: Loop until username isn't used
        return Names;
    }
}