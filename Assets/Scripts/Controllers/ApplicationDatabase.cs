using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationDatabase : MonoBehaviour
{
    public static ApplicationDatabase Instance;

    [SerializeField] private List<ScriptableObject> _SoftwareApplications = new List<ScriptableObject>();
    [SerializeField] private List<ScriptableObject> _CrackedSoftwareApplication = new List<ScriptableObject>();

    [Header("Applications")]
    public ScriptableObject SoftwareCenter;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    public ScriptableObject GetApplication(string id) {
        foreach(var app in _SoftwareApplications) {
            var App = app as ApplicationScriptableObject;
            if(App.AppData.ApplicationID == id) {
                return App;
            }
        }

        return null;
    }

    public List<ScriptableObject> GetSoftwareApps() => _SoftwareApplications;
    public List<ScriptableObject> GetCrackedApps() => _CrackedSoftwareApplication;
}
