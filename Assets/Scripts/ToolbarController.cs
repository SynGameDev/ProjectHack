using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    public ToolbarController Instance;

    private string CurrentToolbar;

    [Header("Tool Prefabs")]
    [SerializeField] private GameObject _ConnectToHostButton;
    [SerializeField] private GameObject _DisconnectFromHost;

    [Header("Toolbar Presets")]
    private List<GameObject> _MainDashToolbar = new List<GameObject>();
    private List<GameObject> _UserTerminalToolbar = new List<GameObject>();


    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
    private void InitToolbar() {
        _MainDashToolbarSetup();
        _UserTerminalToolbarSetup();
    }
    

    // Toolbar 
    private void _MainDashToolbarSetup() {
        _MainDashToolbar.Add(_ConnectToHostButton);
    }

    private void _UserTerminalToolbarSetup() {
        _MainDashToolbar.Add(_DisconnectFromHost);
    }

    private void Update() {

    }


    // Setters
    private void SetCurrentToolbar(string set) => CurrentToolbar = set;

    // Getters
    private string GetCurrentToolbar() => CurrentToolbar;
}
