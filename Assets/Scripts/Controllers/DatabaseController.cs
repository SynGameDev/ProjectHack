using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseController : MonoBehaviour
{
    public static DatabaseController Instance;

    [SerializeField] private List<ScriptableObject> _SoftwareApplications = new List<ScriptableObject>();

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
}
