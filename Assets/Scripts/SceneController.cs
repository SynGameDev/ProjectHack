using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    private List<int> _CurrentlyLoadedScenes = new List<int>();
    

    [Header("Scene Load Number")]
    public int MainDash;
    public int PlayerDash;
    public int UserDesktop;

    [Header("Application Load Number")]
    public int ConnectToUser;


    [Header("Puzzles")]
    public int TerminalConnector;

    public void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        SceneManager.LoadSceneAsync(PlayerDash, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(PlayerDash);
        
    }


    // Connect to user
    public void OpenConnectToUser() {
        _CurrentlyLoadedScenes.Add(ConnectToUser);
        SceneManager.LoadSceneAsync(ConnectToUser, LoadSceneMode.Additive);
    }
    public void CloseConnectToUser() {
        _CurrentlyLoadedScenes.Remove(ConnectToUser);
        SceneManager.UnloadSceneAsync(ConnectToUser);
    }


    // Open User Desktop
    public void OpenUserDesktop() {
        // Unload all scenes
        foreach(var scene in _CurrentlyLoadedScenes) {
            SceneManager.UnloadSceneAsync(scene);
        }

        _CurrentlyLoadedScenes.Clear();

        SceneManager.LoadSceneAsync(UserDesktop, LoadSceneMode.Additive);           // Load the user Desktop
        _CurrentlyLoadedScenes.Add(UserDesktop);                    // add this to loaded scenes
        ToolbarController.Instance.SwitchToolbar("User");
    }

    // Disconnect From Desktop
    public void CloseUserDesktop() {
        
        foreach(var scene in _CurrentlyLoadedScenes) {
            SceneManager.UnloadSceneAsync(scene);
        }


        _CurrentlyLoadedScenes.Clear();

        SceneManager.LoadSceneAsync(PlayerDash, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(PlayerDash);
        ToolbarController.Instance.SwitchToolbar("Main");
    }


    // Puzzles
    public void OpenTerminalConnector() {
        SceneManager.LoadSceneAsync(TerminalConnector, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(TerminalConnector);
    }

    public void CloseTerminalConnector() {
        SceneManager.UnloadSceneAsync(TerminalConnector);
        _CurrentlyLoadedScenes.Remove(TerminalConnector);

    }

    
}
