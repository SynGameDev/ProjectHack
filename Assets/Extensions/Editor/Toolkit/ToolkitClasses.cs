using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ApplicationDatabase
{
    public List<ApplicationClass> Applications = new List<ApplicationClass>();
}

public class TextFileDatabase
{
    public List<TextFile> TextFileData = new List<TextFile>();
}

public class TerminalDatabase
{
    public List<TerminalInfo> Terminals = new List<TerminalInfo>();
}

public class EmailDatabase
{
    public List<EmailDatabaseClass> EmailAccounts = new List<EmailDatabaseClass>();
}

public class SequenceDatabase
{
    public List<string> Sequences = new List<string>();
}

public class MissionDatabases
{
    public List<string> Missions = new List<string>();
}

public class AceTechAccounts
{
    public List<AceTechAccount> Accounts = new List<AceTechAccount>();
}

public class EmailList
{
    public List<Email> Emails = new List<Email>();
}

public class NamesData
{
    public List<string> FirstNames = new List<string>();
    public List<string> LastNames = new List<string>();
    public List<string> Usernames = new List<string>();
}

public class TemplateDatabases
{
    public List<Email> EmailTemp = new List<Email>();
    public List<TextFile> TextFileContent = new List<TextFile>();
    public List<string> MissionMessage = new List<string>();
    public List<string> FoodTemp = new List<string>();
}