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
    public int AceEdit;
    public int SystemDirectory;
    public int AceMail;


    [Header("Puzzles")]
    public int TerminalConnector;
    public int Maze;

    [Header("Popups")]
    public int InstallProgress;
    public int ContractCompletedSuccess;
    public int ContractCompletedFailed;
    public int EndOfDay;
    public int Accountant;
    public int PlayerShop;

    [Header("Other")]
    public int MainMenu;

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
//        ToolbarController.Instance.SwitchToolbar("User");
    }

    // Disconnect From Desktop
    public void CloseUserDesktop() {
        
        foreach(var scene in _CurrentlyLoadedScenes) {
            SceneManager.UnloadSceneAsync(scene);
        }


        _CurrentlyLoadedScenes.Clear();
        SceneManager.UnloadSceneAsync(UserDesktop);
        SceneManager.LoadSceneAsync(PlayerDash, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(PlayerDash);
//        ToolbarController.Instance.SwitchToolbar("Main");
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

    public void OpenAceEdit() {
        SceneManager.LoadSceneAsync(AceEdit, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(AceEdit);
        
    }

    public void CloseAceEdit() {
        SceneManager.UnloadSceneAsync(AceEdit);
        _CurrentlyLoadedScenes.Remove(AceEdit);
    }

    public void OpenFolderSystem() {
        SceneManager.LoadSceneAsync(SystemDirectory, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(SystemDirectory);
    }

    public void CloseFolderSystem() {
        SceneManager.UnloadSceneAsync(SystemDirectory);
        _CurrentlyLoadedScenes.Remove(SystemDirectory);
    }

    public void CloseMainMenu() {
        SceneManager.UnloadSceneAsync(MainMenu);
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

    public void OpenContractCompletedSuccessPopup() {
        SceneManager.LoadSceneAsync(ContractCompletedSuccess, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(ContractCompletedSuccess);
        StartCoroutine(CloseContractSuccessCompleted());
    }

    public IEnumerator CloseContractSuccessCompleted() {
        yield return new WaitForSeconds(2);
        SceneManager.UnloadScene(ContractCompletedSuccess);
        _CurrentlyLoadedScenes.Remove(ContractCompletedSuccess);
    }

    public void OpenContractCompletedFailedPopup() {
        SceneManager.LoadSceneAsync(ContractCompletedFailed, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(ContractCompletedFailed);
        StartCoroutine(CloseContractFailedCompleted());
    }

    public IEnumerator CloseContractFailedCompleted() {
        yield return new WaitForSeconds(2);
        SceneManager.UnloadSceneAsync(ContractCompletedFailed);
        _CurrentlyLoadedScenes.Remove(ContractCompletedFailed);
    }

    public void OpenEndOfDayPopup() {
        SceneManager.LoadSceneAsync(EndOfDay, LoadSceneMode.Additive);
        
    }

    public void CloseEndOfDayPopup() {
        SceneManager.UnloadSceneAsync(EndOfDay);
    }

    public void OpenAccountant() {
        SceneManager.LoadSceneAsync(Accountant, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(Accountant);
    }

    public void CloseAccountant() {
        SceneManager.UnloadSceneAsync(Accountant);
        _CurrentlyLoadedScenes.Remove(Accountant);
    }

    public void OpenPlayerShop() {
        SceneManager.LoadSceneAsync(PlayerShop, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(PlayerShop);
    }

    public void ClosePlayerShop() {
        SceneManager.UnloadSceneAsync(PlayerShop);
        _CurrentlyLoadedScenes.Remove(PlayerShop);
    }

    public void OpenMaze() {
        SceneManager.LoadSceneAsync(Maze, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(Maze);
    }

    public void CloseMaze() {
        SceneManager.UnloadSceneAsync(Maze);
        _CurrentlyLoadedScenes.Remove(Maze);
    }

    public bool CheckIfLoaded(int SceneToCheck) {
        if(_CurrentlyLoadedScenes.Contains(SceneToCheck))
            return true;

        return false;
    }

    public void OpenAceMail()
    {
        SceneManager.LoadSceneAsync(AceMail, LoadSceneMode.Additive);
        _CurrentlyLoadedScenes.Add(AceMail);
    }

    public void CloseAceMail()
    {
        SceneManager.UnloadSceneAsync(AceMail);
        _CurrentlyLoadedScenes.Remove(AceMail);
    }

    // Methods
    public void OpenApplication(string app) {

        switch(app) {
            case "Software Centre":
                OpenSoftwareCenter();
                break;
            case "AceXTerminal":
                OpenAceXTerminal();
                break;
            case "AceEdit":
                OpenAceEdit();
                break;
            case "System Directory":
                OpenFolderSystem();
                break;
            case "Accountant":
                OpenAccountant();
                break;
            case "Shop":
                OpenPlayerShop();
                break;
            case "AceMail":
                OpenAceMail();
                break;

        }
    }
}
