using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class ToolkitApplications : EditorWindow
{ 
    #region New Application Variables

    private string DataPath = Application.streamingAssetsPath + "/" +
                              "Databases/";

    private string _NewApp_ID;
    private string _NewApp_AppName;
    private string _NewApp_AppDescription = "Application Description";
    private string _NewApp_DownloadURL;
    private bool _NewApp_Cracked;

    private int _NewApp_HD_Space;
    private int _NewApp_PurchaseAmount;
    
    #endregion
    
    #region Find Application

    private string _Find_AppName;
    private string _Find_AppID;
    private List<ApplicationClass> _AppsFound = new List<ApplicationClass>();
    #endregion
    
    #region Update application varianles

    private string _Update_ID;
    private string _Update_AppName;
    private string _Update_AppDescription;
    private string _Update_DownloadURL;
    private bool _Update_Cracked;
    private int _Update_HD_Space;
    private int _Update_PurchaseAmount;
    private bool _AppFound;
    private int _ViewingIndex;
    private ApplicationClass _ViewingAppClass;
        
    #endregion

    [MenuItem("Toolkit/Applications")]
    public static void Init()
    {
        EditorWindow.GetWindow<ToolkitApplications>();
        DatabaseValidation.ValidateDatabases();
    }
    
    
    private void OnGUI()
    {
        ToolkitStyles.SetupStyles();

        GUILayout.Label("Applications", ToolkitStyles.PageHeading);
        
        // --- NEW APPLICATION --- //
        GUILayout.Label("New Application", ToolkitStyles.SectionHeading);
        
        GUILayout.Label("Base Details", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        _NewApp_ID = EditorGUILayout.TextField("App ID", _NewApp_ID, GUILayout.Width(300));
        _NewApp_AppName = EditorGUILayout.TextField("App Name", _NewApp_AppName);
        _NewApp_Cracked = EditorGUILayout.Toggle("Cracked Application", _NewApp_Cracked);
        GUILayout.EndHorizontal();
        
        GUILayout.Label("Download Details", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        _NewApp_AppDescription = EditorGUILayout.TextArea(_NewApp_AppDescription, GUILayout.Height(150));
        _NewApp_DownloadURL = EditorGUILayout.TextField("App Download URL", _NewApp_DownloadURL);
        GUILayout.EndHorizontal();
        
        GUILayout.Label("Terminal Details", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        _NewApp_HD_Space = EditorGUILayout.IntField("Hard Drive Space", _NewApp_HD_Space,GUILayout.Width(500));
        _NewApp_PurchaseAmount = EditorGUILayout.IntField("Purchase Amount", _NewApp_PurchaseAmount, GUILayout.Width(500));
        GUILayout.EndHorizontal();
        
        

        // Create a button to complete the form.
        if (GUILayout.Button("Create Application", GUILayout.Width(300)))
        {
            // Check if the application is valid
            if (ValidateApplication())
            {
               CreateApplication();            // If the application is valid, then create the application.
            }
            else
            {
                this.ShowNotification(new GUIContent("Form is Invalid"), 2);            // If the applciation isn't valid then display a message
            }
        }
        
        ToolkitGlobalMethods.DrawLine(1, new Color(0, 0, 0.63f, 1));            // Draw a line
        
        
        // --- FIND APPLICATION ---//
        GUILayout.Label("Find Application", ToolkitStyles.SectionHeading);            // Heading
        
        GUILayout.Label("Application Search Terms", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        _Find_AppName = EditorGUILayout.TextField("Application Name", _Find_AppName);
        _Find_AppID = EditorGUILayout.TextField("Application ID", _Find_AppID);
        if (GUILayout.Button("Search"))
        {
            if (String.IsNullOrEmpty(_Find_AppName))
            {
                SearchApp(_Find_AppID, true);
            }
            else
            {
                SearchApp(_Find_AppName, false);
            }
        }
        
        GUILayout.EndHorizontal();
        
        ToolkitGlobalMethods.DrawLine(1, Color.grey);


        if (_AppFound)
        {
            // --- UPDATE APPLICATION --- //
            GUILayout.Label("Update Application", ToolkitStyles.SectionHeading);
            GUILayout.Label("Base Details", ToolkitStyles.SubHeading);
            GUILayout.BeginHorizontal();
            _Update_ID = EditorGUILayout.TextField("App ID", _Update_ID, GUILayout.Width(300));
            _Update_AppName = EditorGUILayout.TextField("App Name", _Update_AppName);
            _Update_Cracked = EditorGUILayout.Toggle("Cracked Application", _Update_Cracked);
            GUILayout.EndHorizontal();
        
            GUILayout.Label("Download Details", ToolkitStyles.SubHeading);
            GUILayout.BeginHorizontal();
            _NewApp_AppDescription = EditorGUILayout.TextArea(_NewApp_AppDescription, GUILayout.Height(150));
            _NewApp_DownloadURL = EditorGUILayout.TextField("App Download URL", _NewApp_DownloadURL);
            GUILayout.EndHorizontal();
        
            GUILayout.Label("Terminal Details", ToolkitStyles.SubHeading);
            GUILayout.BeginHorizontal();
            _NewApp_HD_Space = EditorGUILayout.IntField("Hard Drive Space", _NewApp_HD_Space,GUILayout.Width(500));
            _NewApp_PurchaseAmount = EditorGUILayout.IntField("Purchase Amount", _NewApp_PurchaseAmount, GUILayout.Width(500));
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Update Application", GUILayout.Width(300)))
            {
                UpdateApplication();
            }
        
        
            ToolkitGlobalMethods.DrawLine(1, Color.gray);
            
        }
        
        GUILayout.Label("All Applications", ToolkitStyles.SubHeading);
        GUILayout.BeginVertical();
        foreach (var item in ToolkitGlobalMethods.GetAllApplications())
        { 
            GUILayout.Label("NAME: " + item.ApplicationName + " | ID: " + item.ApplicationID);
        }
        GUILayout.EndVertical();
    }

    private bool ValidateApplication()
    {
        List<string> StringValues = new List<string>();                // Initalize the list
        
        // Add all variables to a list
        StringValues.Add(_NewApp_AppName);
        StringValues.Add(_NewApp_ID);
        StringValues.Add(_NewApp_AppDescription);
        StringValues.Add(_NewApp_DownloadURL);

        // Loop through each string variables
        foreach (var i in StringValues)
        {
            // if the value is to nothing then return false
            if (string.IsNullOrEmpty(i))
                return false;
        }
        

        return true;                // Return true if all values are okay.
    }

    private void CreateApplication()
    {
        // Create the new application
        ApplicationClass App = new ApplicationClass();
        App.ApplicationID = _NewApp_ID;
        App.ApplicationName = _NewApp_AppName;
        App.ApplicationDescription = _NewApp_AppDescription;
        App.CrackedApplication = _NewApp_Cracked;
        App.ApplicationDownloadURL = _NewApp_DownloadURL;
        App.PurchaseAmount = _NewApp_PurchaseAmount;
        App.HDDSpace = _NewApp_HD_Space;
        
        ApplicationDatabase AppDB = new ApplicationDatabase();                // Create a DB Class for Deseralization & Seralization
        if (!File.Exists(DataPath + "ApplicationDatabase.json"))
        {
            
            using (StreamWriter w = new StreamWriter(DataPath + "ApplicationDatabase.json"))
            {
                string d = JsonUtility.ToJson(AppDB);
                w.WriteLine(d);
            }
        }
        
        // Get the current Data
        using (StreamReader r = new StreamReader(DataPath + "ApplicationDatabase.json"))
        {
            string d = r.ReadToEnd();
            AppDB = JsonUtility.FromJson<ApplicationDatabase>(d);
        }
        
        
        AppDB.Applications.Add(App);            // Add the application to the DB
        
        // Create the JSON file with the new data
        using (StreamWriter w = new StreamWriter(DataPath + "ApplicationDatabase.json"))
        {
            string d = JsonUtility.ToJson(AppDB);
            w.WriteLine(d);
        }
        
        this.ShowNotification(new GUIContent("Application Has Been Created"), 2);            // Complete the application
    }

    private void SearchApp(string term, bool ID)
    {
        var found = false;
        var index = 0;
        ApplicationClass AppToDisplay = new ApplicationClass();
        List<ApplicationClass> DB = ToolkitGlobalMethods.GetAllApplications();
        foreach (var app in DB)
        {
            var search = "";
            if (ID)
            {
                search = app.ApplicationID;
            }
            else
            {
                search = app.ApplicationName;
            }

            if (term == search)
            {
                AppToDisplay = app;
                found = true;
                _AppFound = true;
                _ViewingIndex = index;
                break;
            }

            index++;
        }

        if (found)
        {
            OpenApplication(AppToDisplay);
            _ViewingAppClass = AppToDisplay;
        }
        else
        {
            _AppFound = false;
            this.ShowNotification(new GUIContent("No Application Found"));
        }
    }

    private void OpenApplication(ApplicationClass App)
    {
        _Update_AppName = App.ApplicationName;
        _Update_ID = App.ApplicationID;
        _Update_AppDescription = App.ApplicationDescription;
        _Update_PurchaseAmount = App.PurchaseAmount;
        _Update_HD_Space = App.HDDSpace;
        _Update_DownloadURL = App.ApplicationDownloadURL;
        _Update_Cracked = App.CrackedApplication;
    }

    
    // TODO: Update Application
    private void UpdateApplication()
    {
        List<ApplicationClass> DB = ToolkitGlobalMethods.GetAllApplications();            // Get the database
        
        // Setup the variables
        _ViewingAppClass.ApplicationName = _Update_AppName;
        _ViewingAppClass.ApplicationID = _Update_ID;
        _ViewingAppClass.ApplicationDescription = _Update_AppDescription;
        _ViewingAppClass.ApplicationDownloadURL = _Update_DownloadURL;
        _ViewingAppClass.HDDSpace = _Update_HD_Space;
        _ViewingAppClass.CrackedApplication = _Update_Cracked;
        
        DB[_ViewingIndex] = _ViewingAppClass;            // Update the Databases
        ApplicationDatabase DB_Class = new ApplicationDatabase();            // New Class
        DB_Class.Applications = DB;                // Add data to the class

        
        // Write the JSON data
        using (StreamWriter w = new StreamWriter(DataPath + "/ApplicationDatabase.json"))
        {
            string d = JsonUtility.ToJson(DB_Class);
            w.WriteLine(d);
        }
        
        this.ShowNotification(new GUIContent("Application Updated"));                // Display a notification
    }


    private void OnInspectorUpdate()
    {
        this.Repaint();
    }
}
