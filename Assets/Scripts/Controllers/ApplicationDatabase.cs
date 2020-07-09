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
        JsonSerializer json = new JsonSerializer();
        using (StreamReader file = File.OpenText(ApplicationDataPath))
        {
            AppList apps = new AppList();
            apps = (AppList)json.Deserialize(file, typeof(AppList));

            foreach(var app in apps.AppID)
            {
                ApplicationClass CurrentApp = GetApp(app);
                _SoftwareApplications.Add(CurrentApp);

                if (CurrentApp.CrackedApplication)
                    _CrackedSoftwareApplication.Add(CurrentApp);
            }

        }
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

