using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AceXTerminalController : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField _CommandInput;              // Where commands will be entered

    [Header("Transforms")]  
    [SerializeField] private Transform _CommandSpawnLocation;           // Location to display input & output

    [Header("Prefabs")]
    [SerializeField] private GameObject CommandPrefab;                          // Prefab to display input & output

    [Header("Validation")]
    [SerializeField] private List<string> ValidCommand = new List<string>();                // List of valid commands

    [Header("Open Terminal Application")]
    private GameObject OpenTerminalApp;

    private void Update() {
        // Submit Input
        if(Input.GetKeyDown(KeyCode.Return)) {
            DisplayInput(_CommandInput.text);               // Display the text entered
            ValidateInput();                // Validate the einput
        }
    }

    public void DisplayInput(string text) {
        var go = Instantiate(CommandPrefab);                    // Create the prefab
        go.GetComponent<TextMeshProUGUI>().text = text;         // Assign th message
        go.transform.SetParent(_CommandSpawnLocation);              // Set the location to display message
        go.transform.localScale = Vector3.one;                  // Scale the object to correct size

    }

    public void ValidateInput() {
        if(_CommandInput.text != "" || _CommandInput.text != null) {            // Make sure input isn't blank
            var FullInput = _CommandInput.text;                 // Get the total innput
            string[] InputSplit = FullInput.Split(' ');             // Split the inputs up to tell the different methods

            if(InputSplit[0].Length == 3) {                     // Make sure the action command is only 3 letters
                // Loop through each command and run the valid action
                if(FilterInput(InputSplit[0])) {                    
                    switch(InputSplit[0]) {
                        case "dwn":
                            DownloadFile(InputSplit[1]);
                            break;
                        case "hde":
                            HideApplication(InputSplit[1]);
                            break;
                        case "see":
                            ListHiddenApps();
                            break;
                        case "shw":
                            UnhideApplication(InputSplit[1]);
                            break;
                        case "opn":
                            OpenApplication(InputSplit[1]);
                            break;
                        case "rma":
                            RemoveApplication(InputSplit[1]);
                            break;
                        case "run":
                            RunApplication(InputSplit[1]);
                            break;
                    }
                } else {
                    DisplayInput("Invalid Command");
                }
            } else {
                DisplayInput("Invalid Command");
            }
        }

        _CommandInput.text = "";                // Remove the input text
        _CommandInput.ActivateInputField();     // Activate the field again
    }


    // Check if the command ins valid
    private bool FilterInput(string command) {     
        if(ValidCommand.Contains(command))
            return true;

        return false;
    }

    private void DownloadFile(string url) {
        bool Found = false;                 // File Found

        // Loop through all the apps in the Software List
        foreach(var AppUrl in ApplicationDatabase.Instance.GetSoftwareApps()) {
            var appurl = AppUrl as ApplicationScriptableObject;         // Setup the scriptable object
            if(appurl.AppData.ApplicationDownloadURL == url) {          // Check if the URL is found on the software
                Found = true;                   // ... Set to found
                StartCoroutine(DownloadApplication(appurl));                // Start downloading the app
                break;          // Break the loop
            }
        }

        if(Found == false)              // If the app isn't found display error message
            DisplayInput("Unable To Locate Download File");
    } 


    private void RemoveApplication(string ApplicationName) {
        var AppName = ApplicationName.Replace("_", " ");                // Remove the under scrote and replace it with a space

        // Loop through each application to find the application to remove
        foreach(var app in GameController.Instance.GetActiveContract().Terminal.InstalledApplication) {          
            var App = ApplicationDatabase.Instance.GetApplication(app) as ApplicationScriptableObject;

            if(App.AppData.ApplicationName == AppName) {                // If the app is found start removing the app
                StartCoroutine(RemoveApp(App));
                break;
            }
        }
    }

    private void HideApplication(string ApplicationName) {
        ScriptableObject AppTohide = null;              // Initial Object

        ApplicationName = ApplicationName.Replace("_", " ");                // Remove the under score & replace with space

        // Loop through each installed app & hide the app if found
        foreach(var app in GameController.Instance.GetActiveContract().Terminal.InstalledApplication) {
            var App = ApplicationDatabase.Instance.GetApplication(app) as ApplicationScriptableObject;

            if(App.AppData.ApplicationName == ApplicationName) {
                AppTohide = App as ApplicationScriptableObject;
                break;
            }
        }

        if(AppTohide != null) {
            var app = AppTohide as ApplicationScriptableObject;
            GameController.Instance.GetActiveContract().Terminal.HiddenApplications.Add(app.AppData.ApplicationID);
            GameController.Instance.GetActiveContract().Terminal.InstalledApplication.Remove(app.AppData.ApplicationID);
            GameObject.FindGameObjectWithTag("UserDesktop").GetComponent<DisplayUserDesktop>().UpdateDesktop();
        } else {
            DisplayInput("Application Not Found");
        }

        LogAction("Hide " + ApplicationName);           // Add to the action log
    }

    private void UnhideApplication(string ApplicationName) {
        ScriptableObject AppTohide = null;

        ApplicationName = ApplicationName.Replace("_", " ");
        foreach(var app in GameController.Instance.GetActiveContract().Terminal.HiddenApplications) {
            var App = ApplicationDatabase.Instance.GetApplication(app) as ApplicationScriptableObject;
            if(App.AppData.ApplicationName == ApplicationName) {
                AppTohide = App;
                break;
            }
        }

        if(AppTohide != null) {
            var app = AppTohide as ApplicationScriptableObject;
            GameController.Instance.GetActiveContract().Terminal.InstalledApplication.Add(app.AppData.ApplicationID);
            GameController.Instance.GetActiveContract().Terminal.HiddenApplications.Remove(app.AppData.ApplicationID);
            GameObject.FindGameObjectWithTag("UserDesktop").GetComponent<DisplayUserDesktop>().UpdateDesktop();
        } else {
            DisplayInput("Application Not Found");
        }

        LogAction("Unhide " + ApplicationName);
    }

    private void OpenApplication(string ApplicatioName) {
        var AppName = ApplicatioName.Replace("_", " ");         // Replace the underscore witha space
        bool AppFound = false;              // init found to false


        // loop through each application. if the application is found then open the application
        foreach(var app in GameController.Instance.GetActiveContract().Terminal.InstalledApplication) {
            var App = ApplicationDatabase.Instance.GetApplication(app) as ApplicationScriptableObject;

            if(App.AppData.ApplicationName == AppName) {
                AppFound = true;
                SceneController.Instance.OpenApplication(AppName);
                break;
            }
        }


        // loop through each application. if the application is found then open the application (Only Loop though this if the app wasn't found)
        if(!AppFound) {
            foreach(var app in GameController.Instance.GetActiveContract().Terminal.HiddenApplications) {
                AppFound = true;
                SceneController.Instance.OpenApplication(AppName);
                break;
            }
        }

        if(!AppFound) {             // If app cannoot be found display an error messsage
            DisplayInput("Unable To Locate Application");
        }

    }

    private void ListHiddenApps() {
        string HiddenApps = "";
        foreach(var app in GameController.Instance.GetActiveContract().Terminal.HiddenApplications) {
            var App = ApplicationDatabase.Instance.GetApplication(app) as ApplicationScriptableObject;
            HiddenApps += " | " + App.AppData.ApplicationName;
        }

        DisplayInput(HiddenApps);
    }

    private IEnumerator DownloadApplication(ScriptableObject AppToDownload) {
        var app = AppToDownload as ApplicationScriptableObject;
        LogAction("Install " + app.AppData.ApplicationName);                // Add to log file

        DisplayInput("Downloading & Installing");               // Display Downloading message
        yield return new WaitForSeconds(3);                     // Wait Timer
        SoftwareApplicationInstaller.Instance.InstallProgram(AppToDownload);            // Add Application to downloaded
        DisplayInput("Application Installed");                  // Display output message
    }

    private IEnumerator RemoveApp(ScriptableObject AppToRemove) {
        var app = AppToRemove as ApplicationScriptableObject;
        LogAction("Uninstall " + app.AppData.ApplicationName);

        DisplayInput("Uninstalling Application");
        yield return new WaitForSeconds(3);
        SoftwareApplicationInstaller.Instance.UninstallProgram(AppToRemove);
        DisplayInput("Application Uninstalled");
    }


    private void LogAction(string action) {
        GameController.Instance.GetActiveContract().Terminal.ActionLog.Add(action);
    }

    private void RunApplication(string ApplicationData) {
        var AppInfo = ApplicationData.Split(' ');

        if(OpenTerminalApp != null)
            Destroy(OpenTerminalApp);

        var go = new GameObject();
        go.transform.SetParent(this.gameObject.transform);

        switch(AppInfo[0]) {
            case "HITW":
                go.AddComponent<HoleInTheWallApp>();
                break;
        }
    }
}
