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
    private Dictionary<string, Sprite> AppIcons = new Dictionary<string, Sprite>();
    

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }

        
    }

    private void Start()
    {
        AppIcons.Add("Software Centre", Resources.Load<Sprite>("AppIcons/Software Centre"));
        AppIcons.Add("AceTerminal", Resources.Load<Sprite>("AppIcons/AceTerminal"));
        AppIcons.Add("AceMail", Resources.Load<Sprite>("AppIcons/AceMail"));
        AppIcons.Add("AceEdit", Resources.Load<Sprite>("AppIcons/AceEdit"));
        
    }

    private Sprite GetAppIcon(string name)
    {
        return null;
    }

    public ApplicationClass GetApp(string id)
    {
        foreach(var app in _SoftwareApplications)
        {
            if (app.ApplicationName == id)
                return app;
        }

        return null;
    }

    private void GetAppIcons()
    {
        
    }

    public List<ApplicationClass> GetSoftwareApps() => _SoftwareApplications;
    public List<ApplicationClass> GetCrackedApps() => _CrackedSoftwareApplication;
    public void AddAllApps(List<ApplicationClass> App) => _SoftwareApplications = App;
    public void AddCrackedApps(ApplicationClass App) => _CrackedSoftwareApplication.Add(App);
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

