using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    [Header("Terminal Loop Input")]
    private List<GameObject> TerminalInputList = new List<GameObject>();

    private List<GameObject> TerminalResponseList = new List<GameObject>();
    private int _CurrentIndex;
    private bool _FilterInputs;

    private void Awake()
    {
        TerminalInputList.Clear();
        _CurrentIndex = -1;
    }

    private void Update()
    {
        // Submit Input
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _CurrentIndex = -1;
            DisplayInput(_CommandInput.text, true);               // Display the text entered
            ValidateInput();                // Validate the einput
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeInput("Down");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeInput("Up");
        }
    }

    private void ChangeInput(string increament)
    {
        if (increament == "Down")
        {
            if (_CurrentIndex < -1)
            {
                _CurrentIndex = TerminalInputList.Count;
            }
            else
            {
                _CurrentIndex -= 1;
            }
        }
        else
        {
            if (_CurrentIndex >= TerminalInputList.Count)
            {
                _CurrentIndex = -1;
            }
            else
            {
                _CurrentIndex += 1;
            }
        }

        if (_CurrentIndex == -1)
        {
            _CommandInput.text = "";
        }
        else
        {
            _CommandInput.text = TerminalInputList[_CurrentIndex].GetComponent<TextMeshProUGUI>().text;
        }
    }

    public void DisplayInput(string text, bool input)
    {
        var go = Instantiate(CommandPrefab);                    // Create the prefab
        go.GetComponent<TextMeshProUGUI>().text = text;         // Assign th message
        go.transform.SetParent(_CommandSpawnLocation);              // Set the location to display message
        go.transform.localScale = Vector3.one;                  // Scale the object to correct size

        // Add the input to a list
        if (input)
            TerminalInputList.Add(go);
        else
            TerminalResponseList.Add(go);
    }

    public void ValidateInput()
    {
        if (_CommandInput.text != "" || _CommandInput.text != null)
        {            // Make sure input isn't blank
            var FullInput = _CommandInput.text;                 // Get the total innput
            string[] InputSplit = FullInput.Split(' ');             // Split the inputs up to tell the different methods

            if (InputSplit[0].Length == 3)
            {                     // Make sure the action command is only 3 letters
                // Loop through each command and run the valid action
                if (FilterInput(InputSplit[0]))
                {
                    switch (InputSplit[0])
                    {
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
                            RunApplication(InputSplit);
                            break;
                    }
                }
                else
                {
                    DisplayInput("Invalid Command", false);
                }
            }
            else
            {
                if (InputSplit[0] == "clear")
                {
                    ClearTerminal();
                }
                else
                {
                    DisplayInput("Invalid Command", false);
                }
            }
        }

        _CommandInput.text = "";                // Remove the input text
        _CommandInput.ActivateInputField();     // Activate the field again
    }

    // Check if the command ins valid
    private bool FilterInput(string command)
    {
        if (ValidCommand.Contains(command))
            return true;

        return false;
    }

    private void DownloadFile(string url)
    {
        bool Found = false;                 // File Found

        // Loop through all the apps in the Software List
        foreach (var AppUrl in ApplicationDatabase.Instance.GetSoftwareApps())
        {
            var appurl = AppUrl;
            if (appurl.ApplicationDownloadURL == url)
            {          // Check if the URL is found on the software
                Found = true;                   // ... Set to found
                StartCoroutine(DownloadApplication(appurl));                // Start downloading the app
                break;          // Break the loop
            }
        }

        if (Found == false)              // If the app isn't found display error message
            DisplayInput("Unable To Locate Download File", false);
    }

    private void RemoveApplication(string ApplicationName)
    {
        var AppName = ApplicationName.Replace("_", " ");                // Remove the under scrote and replace it with a space

        // Loop through each application to find the application to remove
        foreach (var app in GameController.Instance.GetActiveContract().Terminal.InstalledApplication)
        {
            var App = ApplicationDatabase.Instance.GetApp(app);

            if (App.ApplicationName == AppName)
            {                // If the app is found start removing the app
                StartCoroutine(RemoveApp(App));
                break;
            }
        }
    }

    private void HideApplication(string ApplicationName)
    {
        ApplicationClass AppTohide = null;              // Initial Object

        ApplicationName = ApplicationName.Replace("_", " ");                // Remove the under score & replace with space

        // Loop through each installed app & hide the app if found
        foreach (var app in GameController.Instance.GetActiveContract().Terminal.InstalledApplication)
        {
            var App = ApplicationDatabase.Instance.GetApp(app);

            if (App.ApplicationName == ApplicationName)
            {
                AppTohide = App;
                break;
            }
        }

        if (AppTohide != null)
        {
            var app = AppTohide;
            GameController.Instance.GetActiveContract().Terminal.HiddenApplications.Add(app.ApplicationID);
            GameController.Instance.GetActiveContract().Terminal.InstalledApplication.Remove(app.ApplicationID);
            GameObject.FindGameObjectWithTag("UserDesktop").GetComponent<DisplayUserDesktop>().UpdateDesktop();
        }
        else
        {
            DisplayInput("Application Not Found", false);
        }

        LogAction("Hide " + ApplicationName + " IP: " + GameController.Instance.GetActiveTerminal().TerminalIP);           // Add to the action log
    }

    private void UnhideApplication(string ApplicationName)
    {
        ApplicationClass AppTohide = null;

        ApplicationName = ApplicationName.Replace("_", " ");
        foreach (var app in GameController.Instance.GetActiveContract().Terminal.HiddenApplications)
        {
            var App = ApplicationDatabase.Instance.GetApp(app);
            if (App.ApplicationName == ApplicationName)
            {
                AppTohide = App;
                break;
            }
        }

        if (AppTohide != null)
        {
            var app = AppTohide;
            GameController.Instance.GetActiveContract().Terminal.InstalledApplication.Add(app.ApplicationID);
            GameController.Instance.GetActiveContract().Terminal.HiddenApplications.Remove(app.ApplicationID);
            GameObject.FindGameObjectWithTag("UserDesktop").GetComponent<DisplayUserDesktop>().UpdateDesktop();
        }
        else
        {
            DisplayInput("Application Not Found", false);
        }

        LogAction("Unhide " + ApplicationName + " IP: " + GameController.Instance.GetActiveTerminal().TerminalIP);
    }

    private void OpenApplication(string ApplicatioName)
    {
        var AppName = ApplicatioName.Replace("_", " ");         // Replace the underscore witha space
        bool AppFound = false;              // init found to false

        // loop through each application. if the application is found then open the application
        foreach (var app in GameController.Instance.GetActiveContract().Terminal.InstalledApplication)
        {
            var App = ApplicationDatabase.Instance.GetApp(app);

            if (App.ApplicationName == AppName)
            {
                AppFound = true;
                SceneController.Instance.OpenApplication(AppName);
                break;
            }
        }

        // loop through each application. if the application is found then open the application (Only Loop though this if the app wasn't found)
        if (!AppFound)
        {
            foreach (var app in GameController.Instance.GetActiveContract().Terminal.HiddenApplications)
            {
                AppFound = true;
                SceneController.Instance.OpenApplication(AppName);
                break;
            }
        }

        if (!AppFound)
        {             // If app cannoot be found display an error messsage
            DisplayInput("Unable To Locate Application", false);
        }
    }

    private void ClearTerminal()
    {
        // Destroy all the input Objects
        foreach (var cmd in TerminalInputList)
        {
            Destroy(cmd);
        }

        foreach (var response in TerminalResponseList)
        {
            Destroy(response);
        }

        // Clear the list
        TerminalInputList.Clear();
        TerminalResponseList.Clear();
    }

    // TODO: Store all the terminal data in a list

    private void ListHiddenApps()
    {
        string HiddenApps = "";
        foreach (var app in GameController.Instance.GetActiveContract().Terminal.HiddenApplications)
        {
            var App = ApplicationDatabase.Instance.GetApp(app);
            HiddenApps += " | " + App.ApplicationName;
        }

        DisplayInput(HiddenApps, false);
    }

    private IEnumerator DownloadApplication(ApplicationClass AppToDownload)
    {
        var app = AppToDownload;
        LogAction("Install " + app.ApplicationName + " IP: " + GameController.Instance.GetActiveTerminal().TerminalIP);                // Add to log file
        gameObject.AddComponent<SoftwareApplicationInstaller>();
        this.gameObject.GetComponent<SoftwareApplicationInstaller>().SetInstallerType("Install");

        DisplayInput("Downloading & Installing", false);               // Display Downloading message
        yield return new WaitForSeconds(3);                     // Wait Timer

        SoftwareApplicationInstaller.Instance.InstallProgram(AppToDownload);            // Add Application to downloaded

        DisplayInput("Application Installed", false);                  // Display output message
        Destroy(this.gameObject.GetComponent<SoftwareApplicationInstaller>());
    }

    private IEnumerator RemoveApp(ApplicationClass AppToRemove)
    {
        var app = AppToRemove;
        LogAction("Uninstall " + app.ApplicationName + " IP: " + GameController.Instance.GetActiveTerminal().TerminalIP);

        DisplayInput("Uninstalling Application", false);
        yield return new WaitForSeconds(3);
        SoftwareApplicationInstaller.Instance.UninstallProgram(AppToRemove);
        DisplayInput("Application Uninstalled", false);
    }

    private void LogAction(string action)
    {
        GameController.Instance.GetActiveContract().ActionLog.Add(action);
    }

    private void RunApplication(string[] ApplicationData)
    {
        if (OpenTerminalApp != null)
            Destroy(OpenTerminalApp);

        var go = new GameObject();
        go.transform.SetParent(this.gameObject.transform);

        switch (ApplicationData[1])
        {
            case "HITW":
                go.AddComponent<HoleInTheWallApp>();
                break;

            case "ConnectTerminal":
                ConnectTerminal(ApplicationData);
                break;
        }
    }

    private void ConnectTerminal(string[] TerminalData)
    {
        if (TerminalData[2] == "conn")
        {
            SceneController.Instance.OpenConnectToUser();

            if (TerminalData.Length > 4)
            {
                // TODO: Get the connection IP & start the connection process
            }
        }

        if (TerminalData[2] == "disc")
        {
            SceneController.Instance.CloseUserDesktop();
        }
    }
}