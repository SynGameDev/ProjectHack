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
    public int SoftwareCenter;
    public int AceXTerminal;


    [Header("Puzzles")]
    public int TerminalConnector;

    [Header("Other Windows")]
    public int InstallProgress;

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

    #region Applications

    public void OpenSoftwareCenter() {
        SceneManager.LoadSceneAsync(SoftwareCenter, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(SoftwareCenter);
    }

    public void CloseSoftwareCenter() {
        SceneManager.UnloadSceneAsync(SoftwareCenter);
        _CurrentlyLoadedScenes.Remove(SoftwareCenter);
    }

    public void OpenAceXTerminal() {
        SceneManager.LoadSceneAsync(AceXTerminal, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(AceXTerminal);
    }

    public void CloseAceXTerminal() {
        SceneManager.UnloadSceneAsync(AceXTerminal);
        _CurrentlyLoadedScenes.Remove(AceXTerminal);
    }

    public void OpenInstallProgress() {
        SceneManager.LoadSceneAsync(InstallProgress, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(InstallProgress);
    }

    public void CloseInstallProgress() {
        SceneManager.UnloadSceneAsync(InstallProgress);
        _CurrentlyLoadedScenes.Remove(InstallProgress);
    }

    

    #endregion



    #region Puzzles

    // Puzzles
    public void OpenTerminalConnector() {
        SceneManager.LoadSceneAsync(TerminalConnector, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(TerminalConnector);
    }

    public void CloseTerminalConnector() {
        SceneManager.UnloadSceneAsync(TerminalConnector);
        _CurrentlyLoadedScenes.Remove(TerminalConnector);

    }

    #endregion

    public bool CheckIfLoaded(int SceneToCheck) {
        if(_CurrentlyLoadedScenes.Contains(SceneToCheck))
            return true;

        return false;
    }

    // Methods
    public void OpenApplication(string app) {

        switch(app) {
            case "Software Center":
                OpenSoftwareCenter();
                break;
            case "AceXTerminal":
                OpenAceXTerminal();
                break;

        }
    }
}
