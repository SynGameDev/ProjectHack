using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
public class Emails : EditorWindow
{

    private string _NewFirstName;
    private string _NewLastName;
    private string _NewUsername;
    private int _NewSelectServer;
    private string _NewPassword;


    private int FromUserSelect;
    private int ToUserSelect;
    private string Subject;
    private string Message;
    private bool Draft;
    
    private string[] _ServerTypes = {"@acemail.com", "@acexmail.com", "@dirtyrat.xyz"};

    [MenuItem("Toolkit/Emails")]
    public static void OnStart()
    {
        EditorWindow.GetWindow<Emails>();
        DatabaseValidation.ValidateDatabases();
    }

    private void OnGUI()
    {
        ToolkitStyles.SetupStyles();
        GUILayout.Label("Emails", ToolkitStyles.PageHeading);
        
        GUILayout.Label("Email Account", ToolkitStyles.SectionHeading);
        GUILayout.Label("User Details", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        _NewFirstName = EditorGUILayout.TextField("First Name", _NewFirstName);
        _NewLastName = EditorGUILayout.TextField("Last Name", _NewLastName);
        GUILayout.EndHorizontal();
        
        GUILayout.Label("Login Details", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        _NewUsername = EditorGUILayout.TextField("Username", _NewUsername);
        _NewSelectServer = EditorGUILayout.Popup(_NewSelectServer, _ServerTypes);
        _NewPassword = EditorGUILayout.TextField("Password", _NewPassword);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create Account"))
        {
            CreateAccount();
        }
        
        GUILayout.Space(10);
        ToolkitGlobalMethods.DrawLine(1, Color.gray);
        
        GUILayout.Label("New Email", ToolkitStyles.SectionHeading);
        GUILayout.Label("Email Headers", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        FromUserSelect = EditorGUILayout.Popup("Email From", FromUserSelect, ToolkitGlobalMethods.GetAceTechEmails());
        ToUserSelect = EditorGUILayout.Popup("Email To", ToUserSelect, ToolkitGlobalMethods.GetAceTechEmails());
        GUILayout.EndHorizontal();
        GUILayout.Label("Email Details", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        Subject = EditorGUILayout.TextField("Subject", Subject);
        Draft = EditorGUILayout.Toggle("Draft", Draft);
        GUILayout.EndHorizontal();
        GUILayout.Label("Email");
        Message = EditorGUILayout.TextArea(Message);

        if (GUILayout.Button("Add Email"))
        {
            AddEmail();
        }

        
    }

    private void CreateAccount()
    {
        AceTechAccount Acc = new AceTechAccount();
        Acc.FirstName = _NewFirstName;
        Acc.LastName = _NewFirstName;
        Acc.Username = _NewUsername;
        Acc.Server = ToolkitGlobalMethods.DropdownValueToString(_NewSelectServer, _ServerTypes);
        Acc.Password = _NewPassword;
        Acc.Email = Acc.Username + Acc.Server;


        var db = ToolkitGlobalMethods.GetAceTechAccounts();
        db.Add(Acc);
        
        AceTechAccounts ace = new AceTechAccounts();
        ace.Accounts = db;
        using (StreamWriter w = new StreamWriter(Application.streamingAssetsPath + "/Databases/AceTechAccounts.json"))
        {
            w.WriteLine(JsonUtility.ToJson(ace));
        }
        
        this.ShowNotification(new GUIContent("Account Has Been Added"));
    }

    private void AddEmail()
    {
        var NewEmail = new Email();

        NewEmail.ToUser =
            ToolkitGlobalMethods.DropdownValueToString(ToUserSelect, ToolkitGlobalMethods.GetAceTechEmails());
        NewEmail.FromUser =
            ToolkitGlobalMethods.DropdownValueToString(FromUserSelect, ToolkitGlobalMethods.GetAceTechEmails());

        NewEmail.Content = Message;
        NewEmail.Subject = Subject;
        NewEmail.Draft = Draft;

        var emls = ToolkitGlobalMethods.GetEmailList();
        
        EmailList a = new EmailList();
        
        emls.Add(NewEmail);
        a.Emails = emls;
        using (StreamWriter w = new StreamWriter(Application.streamingAssetsPath + "/Databases/EmailDatabase.json"))
        {
            w.Write(JsonUtility.ToJson(a));
        }
        
        this.ShowNotification(new GUIContent("Email Has Been Created"));
    }

    private void OnInspectorUpdate()
    {
        this.Repaint();
    }
}
