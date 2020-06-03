using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    public ToolbarController Instance;                      // Singleton Instacne

    private string CurrentToolbar;                          // String stating which toolbar is active

    [Header("Tool Prefabs")]
    [SerializeField] private GameObject _ConnectToHostButton;               // Connected to host prefab
    [SerializeField] private GameObject _DisconnectFromHost;                // Disconnect to host prefab

    [Header("Toolbar Presets")]                 // List of all the toolbar tool presets
    private List<GameObject> _MainDashToolbar = new List<GameObject>();          
    private List<GameObject> _UserTerminalToolbar = new List<GameObject>();

    private List<GameObject> _LoadToolbarTools = new List<GameObject>();            // Current toolbars that are being displayed


    private void Awake() {
        // Create a singleton
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        InitToolbar();
    }

    /// <summary>
    /// Setup all the toolbars
    /// </summary>
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

    public void SwitchToolbar(string Toolbar) {
        CurrentToolbar = Toolbar;                   // Switch the toolbar

        // Foreach tool that is currently active, Remove it from the scene
        foreach(var tool in _LoadToolbarTools) {
            Destroy(tool);
        }

        _LoadToolbarTools.Clear();                  // Empty the list

        // Check which tool bar needs to be displayed now & Start the process
        switch(CurrentToolbar) {
            case "Main":
                LoadToolbar(_MainDashToolbar);
                break;
            case "User":
                LoadToolbar(_UserTerminalToolbar);
                break;
            default:
                break;
        }
    }

    private void LoadToolbar(List<GameObject> Tools) {
        // Foreach tool that needs to be displayed
        foreach(var tool in Tools) {
            var go = Instantiate(tool);                 // Create the tool
            go.transform.SetParent(this.gameObject.transform);            // Assign the tool to the toolbar

            go.transform.localScale = Vector3.one;          // Set the scale to 1

            _LoadedToolbarTools.Add(go);                // Add tool to the toolbar  
        }
    }


    // Setters
    private void SetCurrentToolbar(string set) => CurrentToolbar = set;

    // Getters
    private string GetCurrentToolbar() => CurrentToolbar;
}
