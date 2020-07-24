using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public static class DatabaseValidation
{
    
    public static string DataPath = Application.streamingAssetsPath + "/Databases/";
    public static void ValidateDatabases()
    {
        ValidateAppDatabase();
        ValidateEmailDatabase();    
        ValidateTerminalDatabase();
        ValidateTextFileDatabase();
        ValidateSequenceDatabase();
        ValidateAceTechDatabase();
    }

    public static void ValidateEmailDatabase()
    {
        if (!File.Exists(DataPath + "EmailDatabase.json"))
        {
            using (StreamWriter w = new StreamWriter(DataPath + "EmailDatabase.json"))
            {
                EmailList eml = new EmailList();
                string d = JsonUtility.ToJson(eml);
                w.WriteLine(d);
            }
        }
    }

    public static void ValidateAppDatabase()
    {
        if (!File.Exists(DataPath + "ApplicationDatabase.json"))
        {
            using (StreamWriter w = new StreamWriter(DataPath + "ApplicationDatabase.json"))
            {
                ApplicationDatabase db = new ApplicationDatabase();
                string d = JsonUtility.ToJson(db);
                w.WriteLine(d);
            }
        }
    }

    public static void ValidateTerminalDatabase()
    {
        if (!File.Exists(DataPath + "TerminalDatabase.json"))
        {
            using (StreamWriter w = new StreamWriter(DataPath + "TerminalDatabase.json"))
            {
                TerminalDatabase db = new TerminalDatabase();
                string d = JsonUtility.ToJson(db);
                w.WriteLine(d);
            }
        }
    }

    public static void ValidateTextFileDatabase()
    {
        if (!File.Exists(DataPath + "TextFileDatabase.json"))
        {
            using (StreamWriter w = new StreamWriter(DataPath + "TextFileDatabase.json"))
            {
                TextFileDatabase db = new TextFileDatabase();
                string d = JsonUtility.ToJson(db);
                w.WriteLine(d);
            }
        }
    }

    public static void ValidateSequenceDatabase()
    {
        if (!File.Exists(DataPath + "SequenceDatabase.json"))
        {
            using (StreamWriter w = new StreamWriter(DataPath + "SequenceDatabase.json"))
            {
                SequenceData db = new SequenceData();
                string d = JsonUtility.ToJson(db);
                w.WriteLine(d);
            }
        }
    }

    public static void ValidateAceTechDatabase()
    {
        if (!File.Exists(DataPath + "AceTechAccounts.json"))
        {
            using (StreamWriter w = new StreamWriter(DataPath + "AceTechAccounts.json"))
            {
                AceTechAccounts db = new AceTechAccounts();
                string d = JsonUtility.ToJson(db);
                w.WriteLine(d);
            }

        }
    }
}