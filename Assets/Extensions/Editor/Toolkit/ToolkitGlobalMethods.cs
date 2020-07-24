using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
public static class ToolkitGlobalMethods
{
    public static string DataPath = Application.streamingAssetsPath;
    
    
    /// <summary>
    /// Creates a Line in the editpr
    /// </summary>
    /// <param name="height">Height of the line</param>
    /// <param name="color">Color of the line</param>
    public static void DrawLine(int height, Color color)
    {
        Rect rect = EditorGUILayout.GetControlRect(false, height);
        rect.height = height;
        EditorGUI.DrawRect(rect, color);
    }

    public static string DropdownValueToString(int index, string[] list)
    {
        return list[index];
    }
    
    // --- DATA LIST --- //

    
    /// <summary>
    /// Gets the Database for the applications
    /// </summary>
    /// <returns></returns>
    public static List<ApplicationClass> GetAllApplications()
    {
        ApplicationDatabase AppDB = new ApplicationDatabase();
        using (StreamReader r = new StreamReader(DataPath + "/Databases/ApplicationDatabase.json"))
        {
            string d = r.ReadToEnd();
            AppDB = JsonUtility.FromJson<ApplicationDatabase>(d);
        }

        return AppDB.Applications;
    }

    public static string[] GetApplicationArray()
    {
        List<string> data = new List<string>();
        foreach (var item in GetAllApplications())
        {
            data.Add(item.ApplicationName);
        }

        return data.ToArray();
    }

    public static ApplicationClass FindApplication(string AppName)
    {
        foreach (var item in GetAllApplications())
        {
            if (item.ApplicationName == AppName)
                return item;
        }

        return null;
    }

    public static List<TextFile> GetTextFiles()
    {
        TextFileDatabase TF = new TextFileDatabase();
        using(StreamReader r = new StreamReader(DataPath + "/Databases/TextFileDatabase.json"))
        {
            string d = r.ReadToEnd();
            TF = JsonUtility.FromJson<TextFileDatabase>(d);
        }

        return TF.TextFileData;
    }

    public static string[] GetTextFileArray()
    {
        List<string> data = new List<string>();
        foreach (var app in GetTextFiles())
        {
            data.Add(app.FileName);
        }

        return data.ToArray();
    }
    

    public static List<string> GetSequenceDatabase()
    {
        SequenceDatabase db = new SequenceDatabase();
        using (StreamReader r = new StreamReader(DataPath + "/Databases/SequenceDatabase.json"))
        {
            string d = r.ReadToEnd();
            db = JsonUtility.FromJson<SequenceDatabase>(d);
        }

        return db.Sequences;
    }

    public static string[] GetSequenceArray()
    {
        return GetSequenceDatabase().ToArray();
    }

    public static List<string> GetMissionList(string sequence)
    {
        MissionDatabases db = new MissionDatabases();
        using (StreamReader r = new StreamReader(DataPath + "/Sequences/" + sequence + "/MissionDatabases.json"))
        {
            db = JsonUtility.FromJson<MissionDatabases>(r.ReadToEnd());
        }

        return db.Missions;
    }

    public static string[] MissionNames(string seq)
    {
        var list = GetMissionList(seq);

        var d = new List<string>();
        
        foreach (var l in list)
        {
            d.Add(l);
        }

        return d.ToArray();
        
    }

    public static List<EmailAccount> GetAceTechAccounts()
    {
        AceTechAccounts db = new AceTechAccounts();
        using (StreamReader r = new StreamReader(DataPath + "/Databases/AceTechAccounts.json"))
        {
            string d = r.ReadToEnd();
            db = JsonUtility.FromJson<AceTechAccounts>(d);
        }

        return db.Accounts;
    }

    public static string[] GetAceTechEmails()
    {
        List<string> d = new List<string>();
        d.Add("None");
        foreach (var item in GetAceTechAccounts())
        {
            d.Add(item.Username + item.Server);
        }

        return d.ToArray();
    }

    public static List<Email> GetEmailList()
    {
        EmailList eml = new EmailList();
        using (StreamReader r = new StreamReader(DataPath + "/Databases/EmailDatabase.json"))
        {
            eml = JsonUtility.FromJson<EmailList>(r.ReadToEnd());
        }

        return eml.Emails;
    }

    public static EmailAccount FindEmailAccount(string eml)
    {
        foreach (var item in GetAceTechAccounts())
        {
            if (item.Username + item.Server == eml)
            {
                return item;
            }
        }

        return null;
    }

    public static List<TerminalInfo> GetTerminalList()
    {
        TerminalDatabase db = new TerminalDatabase();
        using (StreamReader r = new StreamReader(DataPath + "/Databases/TerminalDatabase.json"))
        {
            db = JsonUtility.FromJson<TerminalDatabase>(r.ReadToEnd());
        }

        return db.Terminals;
    }

    public static string[] GetTerminalNames()
    {
        List<string> d = new List<string>();
        foreach (var item in GetTerminalList())
        {
            d.Add(item.TemrinalName);
        }

        return d.ToArray();
    }


}


