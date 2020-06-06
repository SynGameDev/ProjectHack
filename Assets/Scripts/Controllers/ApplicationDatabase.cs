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

    public List<ScriptableObject> GetSoftwareApps() => _SoftwareApplications;
    public List<ScriptableObject> GetCrackedApps() => _CrackedSoftwareApplication;
}
