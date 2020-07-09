using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ApplicationDatabase : MonoBehaviour
{
    public static ApplicationDatabase Instance;

    [SerializeField] private List<ApplicationClass> _SoftwareApplications = new List<ApplicationClass>();
    [SerializeField] private List<ApplicationClass> _CrackedSoftwareApplication = new List<ApplicationClass>();

    [Header("Applications")]
    public ScriptableObject SoftwareCenter;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }

        CreateAppDatabase();
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

    public List<ScriptableObject> GetSoftwareApps() => _SoftwareApplications;
    public List<ScriptableObject> GetCrackedApps() => _CrackedSoftwareApplication;
}

