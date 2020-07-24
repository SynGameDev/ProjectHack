using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractGenerator
{
    private static List<string> _Actions = new List<string>();
    public static List<ApplicationClass> AppsToInstall = new List<ApplicationClass>();
    public static List<string> Objective = new List<string>();
    public static List<TextFile> TextFiles = new List<TextFile>();
    
    private static ContractInfo Contract = new ContractInfo();            // Create Contract

    public static void CreateContract()
    {
        GenerateActions();
        
    }

    private static void NewContract()
    {
        var Terminal = GenerateTerminal();
        var totalObjectives = UnityEngine.Random.Range(1, _Actions.Count);            // Number
        
        
        
        for (int i = 0;i > totalObjectives;i++)
        {
            CreateObjective();
        }

    }

    private static TerminalInfo GenerateTerminal()
    {
        var Terminal = new TerminalInfo();

        return Terminal;
    }

    private static List<string> CreateObjective()
    {
        List<string> NewObjectiveList = new List<string>();
        var Object = _Actions[Random.Range(0, _Actions.Count - 1)];            // Get a action
        
        switch (Object)
        {
            case "Install":
                Objective.Add(CreateInstallObj());
                break;
            case "Uninstall":
                Objective.Add(CreateUninstallObj());
                break;
            case "Hide":
                Objective.Add(CreateHideObj());
                break;
            case "Show":
                Objective.Add(CreateShowObj());
                break;
        }

        return NewObjectiveList;
    }

    private static string CreateInstallObj()
    {
        ApplicationClass App = SelectApp();
        string Term = Contract.Terminal[0].TerminalIP;

        return "Install " + App.ApplicationName + " IP: " + Term;
    }

    private static string CreateUninstallObj()
    {
        ApplicationClass App = SelectApp();
        string Term = Contract.Terminal[0].TerminalIP;

        if (!Objective.Contains("Install " + App.ApplicationName + " IP: " + Term))
        {
            AppsToInstall.Add(App);
        }

        return "Uninstall " + App.ApplicationName + " IP: " + Term;
    }

    private static string CreateHideObj()
    {
        ApplicationClass App = SelectApp();
        string Term = Contract.Terminal[0].TerminalIP;

        if (!Objective.Contains("Install " + App.ApplicationName + " IP: " + Term))
        {
            Objective.Add("Install " + App.ApplicationName + " IP: " + Term);
        }

        return "Hide " + App.ApplicationName + " IP: " + Term;
    }

    private static string CreateShowObj()
    {
        ApplicationClass App = SelectApp();
        string Term = Contract.Terminal[0].TerminalIP;
        
        AppsToInstall.Add(App);
        return "Show " + App.ApplicationName + " IP: " + Term;
    }

    private static string CreateDeleteObj()
    {
        ApplicationClass App = SelectApp();
        string Term = Contract.Terminal[0].TerminalIP;
        var txt = new TextFile();
        TextFiles.Add(txt);

        return "Delete " + txt.FileName + "." + txt.ext + " IP: " + Term;

    }

    private static ApplicationClass SelectApp()
    {
        var AppDB = ApplicationDatabase.Instance.GetSoftwareApps();
        return AppDB[Random.Range(0, AppDB.Count)];
    }
    
    

    /// <summary>
    /// This Method will init the list of actions that can be used for the contract
    /// </summary>
    private static void GenerateActions()
    {
        _Actions.Add("Install");
        _Actions.Add("Uninstall");
        _Actions.Add("Hide");
        _Actions.Add("Show");
        _Actions.Add("Delete");;
    }
}