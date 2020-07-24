using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceData
{
    public List<string> SequenceNames = new List<string>();
}

public class TerminalList
{
    public List<string> TerminalIP = new List<string>();
}

public class MissionData
{
    public string MissionID;
    public string MissionName;
    public string MissionSequence;
    public string MissionOwner;
    public string MissionEmail;
    public List<TerminalInfo> Terminals = new List<TerminalInfo>();
}

public class AppDatabase
{
    public List<string> Apps = new List<string>();
    public List<ApplicationClass> AppClassData = new List<ApplicationClass>();
}

public class EmailClass
{
    public List<string> EmailNames = new List<string>();
}