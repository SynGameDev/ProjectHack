using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class Terminals : EditorWindow
{
    [MenuItem("Toolkit/Terminals")]
    public static void OnStart()
    {
        EditorWindow.GetWindow<Terminals>();
        
    }
    
    private List<ApplicationClass> InstalledApplications = new List<ApplicationClass>();
    private List<ApplicationClass> HiddenApplications = new List<ApplicationClass>();
    private List<TextFile> TextFileList = new List<TextFile>();
    public EmailDatabaseClass EmailAccount;

    private string _New_TerminalName;
    private string _New_TerminalType;
    private string _New_TerminalIP;
    private int _New_SelectedApp;
    private int _New_SelectedTextFile;
    private int _New_EmailAccount;
    private int _New_SelectedTerminalType;
    private int _New_SelectedEmailAcc;
    private int _New_AntiVirus;
    private int _New_HDSpace;
    
    // Dropdown Details
    private string[] _TerminalTypes = {"Desktop", "Web Server", "Database"};
    
    
    private string[] _ApplicationDropdown;
    
    
    
    public void OnGUI()
    {
        
        ToolkitStyles.SetupStyles();
        DatabaseValidation.ValidateDatabases();
        
        // --- NEW TERMINAL --- //
        GUILayout.Label("TERMINALS", ToolkitStyles.PageHeading);
        
        GUILayout.Label("New Terminal", ToolkitStyles.SectionHeading);
        GUILayout.Label("Terminal Details", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        _New_TerminalName = EditorGUILayout.TextField("Terminal Name", _New_TerminalName);
        _New_TerminalIP = EditorGUILayout.TextField("Terminal IP", _New_TerminalIP);
        _New_SelectedTerminalType = EditorGUILayout.Popup("Terminal Type", _New_SelectedTerminalType, _TerminalTypes);
        GUILayout.EndHorizontal();
        
        GUILayout.Label("Applications", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        _New_SelectedApp = EditorGUILayout.Popup("Applications", _New_SelectedApp, ToolkitGlobalMethods.GetApplicationArray());
        if (GUILayout.Button("Installed Applications"))
        {
            InstalledApplications.Add(ToolkitGlobalMethods.FindApplication(ToolkitGlobalMethods.DropdownValueToString(_New_SelectedApp, ToolkitGlobalMethods.GetApplicationArray())));
            this.ShowNotification(new GUIContent("Application Added"));
        }

        if (GUILayout.Button("Hidden Application"))
        {
            HiddenApplications.Add(ToolkitGlobalMethods.FindApplication(ToolkitGlobalMethods.DropdownValueToString(_New_SelectedApp, ToolkitGlobalMethods.GetApplicationArray())));
            this.ShowNotification(new GUIContent("Hidden Application"));
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Label("Terminal Files & Accounts", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        _New_SelectedTextFile =
            EditorGUILayout.Popup("Text Files", _New_SelectedTextFile, ToolkitGlobalMethods.GetTextFileArray());
        if (GUILayout.Button("Add File"))
        {
            
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Label("Other System Details");
        GUILayout.BeginHorizontal();
        _New_SelectedEmailAcc = EditorGUILayout.Popup("Email Account", _New_SelectedEmailAcc,
            ToolkitGlobalMethods.GetAceTechEmails());
        _New_AntiVirus = EditorGUILayout.IntField("Anti-Virus Level", _New_AntiVirus);
        _New_HDSpace = EditorGUILayout.IntField("HD Space", _New_HDSpace);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create Terminal", GUILayout.Width(400)))
        {
            CreateTerminal();
        }
        GUILayout.Space(10);
        ToolkitGlobalMethods.DrawLine(1, Color.gray);
    }

    private void CreateTerminal()
    {
        TerminalInfo NewTerminal = new TerminalInfo();
        NewTerminal.TemrinalName = _New_TerminalName;
        NewTerminal.TerminalIP = _New_TerminalIP;
        
        NewTerminal.InstalledApplication = InstalledApplications;
        NewTerminal.HiddenApplications = HiddenApplications;
        NewTerminal.AntiVirusLevel = _New_AntiVirus;
        NewTerminal.HHD = _New_HDSpace;
        NewTerminal.TextFileList = TextFileList;

        if (_New_SelectedTerminalType == null)
        {
            NewTerminal.TerminalType = "Desktop";
        }
        else
        {
            NewTerminal.TerminalType =
                ToolkitGlobalMethods.DropdownValueToString(_New_SelectedTerminalType, _TerminalTypes);
        }
       

        if (_New_SelectedEmailAcc > 0)
        {
            NewTerminal.EmailAccount =
                ToolkitGlobalMethods.FindEmailAccount(ToolkitGlobalMethods.DropdownValueToString(_New_SelectedEmailAcc,
                    ToolkitGlobalMethods.GetAceTechEmails()));
        }
        else
        {
            NewTerminal.EmailAccount = null;
        }
        
        TerminalDatabase db = new TerminalDatabase();
        List<TerminalInfo> Terms = new List<TerminalInfo>();
        Terms.Add(NewTerminal);
        db.Terminals = Terms;
        using (StreamWriter w = new StreamWriter(Application.streamingAssetsPath + "/Databases/TerminalDatabase.json"))
        {
            string d = JsonUtility.ToJson(db);
            w.WriteLine(d);
        }

        this.ShowNotification(new GUIContent("Terminal Created"));
        InstalledApplications.Clear();
        HiddenApplications.Clear();
    }
}