using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class ApplicationDatabase : MonoBehaviour
{
    public static ApplicationDatabase Instance;

    [SerializeField] private List<ApplicationClass> _SoftwareApplications = new List<ApplicationClass>();
    [SerializeField] private List<ApplicationClass> _CrackedSoftwareApplication = new List<ApplicationClass>();
    [SerializeField] private string ApplicationDataPath;
    

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }

        CreateAppDatabase();
    }

    private void CreateAppDatabase()
    {
        var DirectoryInfo = new DirectoryInfo(ApplicationDataPath);
        FileInfo[] AppFiles = DirectoryInfo.GetFiles("*.app");

        foreach(var file in AppFiles)
        {
            ApplicationClass App = new ApplicationClass();
            JsonSerializer json = new JsonSerializer();
            using(StreamReader AppFile = File.OpenText(ApplicationDataPath + file.Name + ".app"))
            {
                ToolKitAppClass TempApp = (ToolKitAppClass)json.Deserialize(AppFile, typeof(ToolKitAppClass));
                
                App.ApplicationName = TempApp.ApplicatioName;
                App.ApplicationID = TempApp.ApplicationID;
                App.ApplicationDescription = TempApp.ApplicationDescription;
                App.ApplicationDownloadURL = TempApp.ApplicationURL;
                App.CrackedApplication = TempApp.CrackedApp;
                App.ApplicationIcon = GetAppIcon(App.ApplicationName);
            }

            _SoftwareApplications.Add(App);

            if (App.CrackedApplication)
                _CrackedSoftwareApplication.Add(App);
        }

        
    }

    private Sprite GetAppIcon(string name)
    {
        return Resources.Load<Sprite>("Sprites/ApplicationIcons/" + name);
    }

    public ApplicationClass GetApp(string id)
    {
        foreach(var app in _SoftwareApplications)
        {
            if (app.ApplicationID == id)
                return app;
        }

        return null;
    }

    public List<ApplicationClass> GetSoftwareApps() => _SoftwareApplications;
    public List<ApplicationClass> GetCrackedApps() => _CrackedSoftwareApplication;
}

public class AppList
{
    public List<string> AppID = new List<string>();
}

public class ToolKitAppClass
{
    public string ApplicationID;
    public string ApplicatioName;
    public string ApplicationDescription;
    public string ApplicationIcon;
    public bool CrackedApp;
    public string ApplicationURL;
    public int ApplicationHDD;
}

