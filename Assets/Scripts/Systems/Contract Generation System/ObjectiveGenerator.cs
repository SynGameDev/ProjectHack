using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ObjectiveGenerator 
{

    public List<string> Actions = new List<string>();
    public List<ScriptableObject> Applications = new List<ScriptableObject>();
    public TerminalGenerator TerminalGen;
    public TerminalInfo Terminal;
    public ContractInfo Contract;

    // Contract Basics
    public int diff;
    public string ContractorName;
    public string ContractorEmail;



    public void CreateContract(List<ScriptableObject> Apps)
    {
        Applications = Apps;
        SetupContractBasics();

        TerminalGen = new TerminalGenerator();
        TerminalGen.CreateTerminal(diff);
        Terminal = TerminalGen.NewTerminal;

        Contract = new ContractInfo();



    }
    /// <summary>
    /// This Method will Create a list of actions for the user to implement
    /// </summary>

    public void EasyDesktopActions()
    {
        Actions.Add("Install");
        Actions.Add("Uninstall");
        Actions.Add("Hide");

    }

    private void SetupContractBasics()
    {
        diff = UnityEngine.Random.Range(1, 4);
    }

    private void CreateActions()
    {
        int NumOfObjectives;
        List<ScriptableObject> ActionedApps = new List<ScriptableObject>();
        if(diff == 1)
        {
            NumOfObjectives = UnityEngine.Random.Range(0, 6);
            for (int i = 0; i < NumOfObjectives; i++)
            {
                if(Terminal.TerminalType == "Desktop")
                {
                    string action;
                    EasyDesktopActions();
                    action = Actions[UnityEngine.Random.Range(0, Actions.Count + 1)];
                    if(action == "Install")
                    {
                        var app = Applications[UnityEngine.Random.Range(0, Applications.Count)];
                        var AppObject = app as ApplicationScriptableObject;
                        string NewObjective = CreateDesktopObjective(action, AppObject.AppData.ApplicationName, Terminal.TerminalIP);
                        Contract.MainObjectives.Add(NewObjective);

                        if (AppObject.AppData.CrackedApplication)
                        {
                            string SecondObjective = "Hide " + AppObject.AppData.ApplicationName + " On " + Terminal.TerminalIP;
                            Contract.Objective.Add(SecondObjective);
                        }

                        ActionedApps.Add(app);
                        

                    } else if(action == "Uninstall")
                    {
                        var app = Applications[UnityEngine.Random.Range(0, Applications.Count)];
                        if(!ActionedApps.Contains(app))
                        {
                            var AppObject = app as ApplicationScriptableObject;
                            string NewObjective = CreateDesktopObjective(action, AppObject.AppData.ApplicationName, Terminal.TerminalIP);
                            Contract.MainObjectives.Add(NewObjective);

                            AddApplication(AppObject.AppData.ApplicationID, AppObject.AppData.CrackedApplication);
                        }
                    } else if(action == "Hide")
                    {
                        var app = (ApplicationScriptableObject)Applications[UnityEngine.Random.Range(0, Applications.Count)];
                        if(!ActionedApps.Contains(app))
                        {
                            string NewObjective = CreateDesktopObjective(action, app.AppData.ApplicationName, Terminal.TerminalIP);
                            Contract.MainObjectives.Add(NewObjective);

                            AddApplication(app.AppData.ApplicationID, app.AppData.CrackedApplication);
                        }
                    }
                }
            }
        }
    }

    private void AddApplication(string id, bool cracked)
    {
        if(cracked)
        {
            Terminal.HiddenApplications.Add(id);
        } else
        {
            Terminal.InstalledApplication.Add(id);
        }
    }

    private string CreateDesktopObjective(string action, string name, string ip)
    {
        return action + name + " IP: " + ip;
    }
}
