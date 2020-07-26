using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadDatabaseInfo : MonoBehaviour
{
    private string DataPath = Application.streamingAssetsPath + "/Databases/";

    private void Awake()
    {
        
    }

    private void Start()
    {
        GetApplicationData();
        LoadTerminals();
        GetEmailDatabases();
        GetSequenceData();
        GetMissionData();
    }
    

    private void LoadTerminals()
    {
        var Database = new TerminalDatabase();            // Create a new class

        using (StreamReader r = new StreamReader(DataPath + "TerminalDatabase.json"))
        {
            Database = JsonUtility.FromJson<TerminalDatabase>(r.ReadToEnd());            // Open the file
        }

        // Loop through each terminal, if it doesn't contain the terminal then add it to the list
        foreach (var item in Database.Terminals)
        {
            if(!GameController.Instance.GetAllTerminals().Contains(item))
                GameController.Instance.AddNewTerminal(item);
        }

    }

    private void GetEmailDatabases()
    {
        // Create the account & DB Class to deserialize the data
        AceTechAccount NewAccount = new AceTechAccount();
        AceTechAccountsDB DB = new AceTechAccountsDB();

        // Open up the database
        using (StreamReader r = new StreamReader(DataPath + "AceTechAccounts.json"))
        {
            DB = JsonUtility.FromJson<AceTechAccountsDB>(r.ReadToEnd());
        }

        // Loop through each account 
        foreach (var item in DB.Accounts)
        {
            
            // Check that the account information isn't already in the databases and add the email account to the database
            if (AceTechAccountController.Instance.FindAccByEmail(item.Username + item.Server) == null)
            {
                AceTechAccountController.Instance.AddAccount(NewAccount);
                // Setup the user account
                NewAccount.FirstName = item.FirstName;
                NewAccount.LastName = item.LastName;
                NewAccount.Email = item.Username + item.Server;
                NewAccount.Username = item.Username;
                NewAccount.Password = item.Password;

                // Setup the draft emails
                NewAccount.SentEmails = GetSentEmails(NewAccount);
                NewAccount.ReceivedEmails = GetReceivedEmails(NewAccount);
                NewAccount.DraftEmails = GetDraftEmails(NewAccount);
            }

        }
    }

    private List<Email> GetSentEmails(AceTechAccount acc)
    {
        var eml = new EmailList();
        var data = new List<Email>();
        using (StreamReader r = new StreamReader(DataPath + "EmailDatabase.json"))
        {
            eml = JsonUtility.FromJson<EmailList>(r.ReadToEnd());
        }

        foreach (var item in eml.Emails)
        {
            if(item.FromUser == acc.Email && !item.Draft)
                data.Add(item);
        }

        return data;
    }

    private List<Email> GetReceivedEmails(AceTechAccount acc)
    {
        var eml = new EmailList();
        var data = new List<Email>();
        foreach (var item in eml.Emails)
        {
            if(item.ToUser == acc.Email)
                data.Add(item);
        }

        return data;
    }

    private List<Email> GetDraftEmails(AceTechAccount acc)
    {
        var eml = new EmailList();
        var data = new List<Email>();
        foreach (var item in eml.Emails)
        {
            if(item.FromUser == acc.Email && item.Draft)
                data.Add(item);
        }

        return data;
    }

    private void GetApplicationData()
    {
        var ApplicationDB = new ApplicationDB();

        using (StreamReader r = new StreamReader(DataPath + "ApplicationDatabase.json"))
        {
            ApplicationDB = JsonUtility.FromJson<ApplicationDB>(r.ReadToEnd());
        }

        foreach (var item in ApplicationDB.Applications)
        {
            if(item.CrackedApplication)
                ApplicationDatabase.Instance.AddCrackedApps(item);
        }
        
        ApplicationDatabase.Instance.AddAllApps(ApplicationDB.Applications);
    }

    private void GetSequenceData()
    {
        var Sequences = new SequenceDatabase();
        using (StreamReader r = new StreamReader(DataPath + "SequenceDatabase.json"))
        {
            Sequences = JsonUtility.FromJson<SequenceDatabase>(r.ReadToEnd());
            
        }

        foreach (var item in Sequences.Sequences)
        {
            if (!MissionDatabase.Instance.FindSequence(item))
                MissionDatabase.Instance.AddSequence(item);
        }
        
        
    }

    private void GetMissionData()
    {
        var sequences = MissionDatabase.Instance.GetSequences();
        
        
        foreach (var item in sequences)
        {
            GetMissionInfo(item);
        }
    }

    private void GetMissionInfo(string seq)
    {
        var MissionDB = new MissionDatabases();
        using (StreamReader r =
            new StreamReader(Application.streamingAssetsPath + "/Sequences/" + seq + "/MissionDatabases.json"))
        {
            MissionDB = JsonUtility.FromJson<MissionDatabases>(r.ReadToEnd());
        }

        foreach (var item in MissionDB.Missions)
        {
            using (StreamReader r =
                new StreamReader(Application.streamingAssetsPath + "/Sequences/" + seq + "/" + item + ".json"))
            {
                Mission mis = new Mission();
                mis = JsonUtility.FromJson<Mission>(r.ReadToEnd());
                if(!MissionDatabase.Instance.FindMission(mis)) 
                    MissionDatabase.Instance.AddMission(mis);
            }
        }
    } 
}


public class TerminalDatabase
{
    public List<TerminalInfo> Terminals = new List<TerminalInfo>();
}

public class AceTechAccountsDB
{
    public List<AceTechAccount> Accounts = new List<AceTechAccount>();
}

public class EmailList
{
    public List<Email> Emails = new List<Email>();
}

public class ApplicationDB
{
    public List<ApplicationClass> Applications = new List<ApplicationClass>();
}

public class SequenceDatabase
{
    public List<string> Sequences = new List<string>();
}

public class MissionDatabases
{
    public List<string> Missions = new List<string>();
}

public class NamesDatabase
{
    public List<string> FirstName = new List<string>();
    public List<string> LastName = new List<string>();
    public List<string> Username = new List<string>();
}
