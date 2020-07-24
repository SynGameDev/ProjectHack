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
            NewInputValidation();
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


        // Set the size of the terminal
        var totalChar = text.Length;
        if(totalChar > 56 && totalChar < 112)
        {
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y * 2);
        } else if(totalChar > 112 && totalChar< 168)
        {
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y * 3);
        } else if(totalChar > 168 && totalChar > 224)
        {
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y * 4);
        } else if(totalChar > 224 && totalChar < 280)
        {
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.y * 5);
        }

        Debug.Log(text + " | " + totalChar);

        // Add the input to a list
        if (input)
            TerminalInputList.Add(go);
        else
            TerminalResponseList.Add(go);
    }

    public void NewInputValidation()
    {
        if(_CommandInput.text != "" || _CommandInput.text != null)
        {
            var Input = _CommandInput.text;
            string[] Split = Input.Split(' ');

            switch(Split[0])
            {
                case "dwn":
                    ValidateDwn(Split);
                    break;
                case "hde":
                    ValidateHde(Split);
                    break;
                case "see":
                    ValidateSee(Split);
                    break;
                case "shw":
                    ValidateShw(Split);
                    break;
                case "opn":
                    ValidateOpn(Split);
                    break;
                case "rma":
                    ValidateRma(Split);
                    break;
                case "run":
                    ValidateRun(Split);
                    break;
                case "clear":
                    ValidateClear(Split);
                    break;
            }
        }

        _CommandInput.text = "";                // Remove the input text
        _CommandInput.ActivateInputField();     // Activate the field again
    }

    #region validate input
    private void ValidateDwn(string[] input)
    {
        if(input.Length == 1 || input.Length > 2)
        {
            string data_1 = "--- Invalid Command Input ---\r\n";
            string data_2 = "Command can only take 2 arguments\r\n";
            string data_3 = "dwn [Download Server]";
            string help = data_1 + data_2 + data_3;
            DisplayInput(help, false);
        } else if(input[1] == "Help")
        {
            string data_1 = "--- Syndicate's Downloaer ---\r\n";
            string data_2 = "--- DESCRIPTION: Syndicate's Downloader can access any file located on the ace web. add the application URL, and we will get it downloaded straight to your terminal\r\n";
            string data_3 = "--- HOW TO USE ---\r\n";
            string data_4 = "EXAMPLE: dwn [Application URL]\r\n";
            string data_5 = "(dwn) Run our downloader\r\n";
            string data_6 = "([Application URL]) Application that you're wanting to install";
            string[] HelpData = { data_1, data_2, data_3, data_4, data_5, data_6 };
            foreach(var data in HelpData)
            {
                DisplayInput(data, false);
            }
        }
        else
        {
            DownloadFile(input[1]);
        }
    }

    private void ValidateHde(string[] input)
    {
        if(input.Length == 1 || input.Length > 2)
        {
            string data_1 = "--- Invalid Command ---\r\n";
            string data_2 = "Command can only take 2 arguments\r\n";
            string data_3 = "hde [Application]";
            DisplayInput(data_1 + data_2 + data_3, false);
        } else
        {
            HideApplication(input[1]);
        }
    }

    private void ValidateSee(string[] input)
    {
        if (input.Length == 1 || input.Length > 2)
        {
            string data_1 = "--- Invalid Command ---\r\n";
            string data_2 = "Command can only take to arguments";
            string data_3 = "see [Application (Replace Spaces with _)]";
            DisplayInput(data_1 + data_2 + data_3, false);
        }
        else
        {
            ListHiddenApps();
        }
    }
    
    private void ValidateShw(string[] input)
    {
        if(input.Length == 1 || input.Length > 2)
        {
            string data_1 = "--- Invalid Commands ---\r\n";
            string data_2 = "Comand can only take 2 arguments";
            string data_3 = "Shw [Application Name (Replace Spaces with _)]";
            DisplayInput(data_1 + data_2 + data_3, false);
        } else
        {
            UnhideApplication(input[1]);
        }
    }
    
    private void ValidateOpn(string[] input)
    {
        if(input.Length == 1 || input.Length > 2)
        {
            string data_1 = "--- Invalid Command ---\r\n";
            string data_2 = "Command can only take 2 arguements\r\n";
            string data_3 = "opn [Application_Name (Replace Spaces with _)]\r\n";
            DisplayInput(data_1 + data_2 + data_3, false);
        } else
        {
            OpenApplication(input[1]); ;
        }
    }

    private void ValidateRma(string[] input)
    {
        if(input.Length == 1 || input.Length > 2)
        {
            string data_1 = "--- Invalid Command ---\r\n";
            string data_2 = "Command can only take 2 arguements";
            string data_3 = "rma [Application_Name (Replace Spaces with _)]";
            DisplayInput(data_1 + data_2 + data_3, false);
        } else
        {
            RemoveApp(input[1]);
        }
    }

    private void ValidateRun(string[] input)
    {
        if(input.Length == 1)
        {
            string data_1 = "--- Invalid Command ---\r\n";
            string data_2 = "Command requires an application to run\r\n";
            string data_3 = "run [Application Name] [Application Specific Details]";
            DisplayInput(data_1 + data_2 + data_3, false);
        } else
        {
            RunApplication(input);
        }
    }

    private void ValidateClear(string[] input)
    {
        if(input.Length == 1)
        {
            ClearTerminal();
        } else
        {
            // TODO: Display Help
        }
    }

    #endregion

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
        foreach (var app in GameController.Instance.GetActiveTerminal().InstalledApplication)
        {
            var App = app;

            if (App.ApplicationName == AppName)
            {                // If the app is found start removing the app
                StartCoroutine(RemoveApp(AppName));
                break;
            }
        }
    }

    private void HideApplication(string ApplicationName)
    {
        ApplicationClass AppTohide = null;              // Initial Object

        ApplicationName = ApplicationName.Replace("_", " ");                // Remove the under score & replace with space

        // Loop through each installed app & hide the app if found
        foreach (var app in GameController.Instance.GetActiveTerminal().InstalledApplication)
        {
            var App = app;

            if (App.ApplicationName == ApplicationName)
            {
                AppTohide = App;
                break;
            }
        }

        if (AppTohide != null)
        {
            var app = AppTohide;
            GameController.Instance.GetActiveTerminal().HiddenApplications.Add(app);
            GameController.Instance.GetActiveTerminal().InstalledApplication.Remove(app);
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
        foreach (var app in GameController.Instance.GetActiveTerminal().HiddenApplications)
        {
            var App = app;
            if (App.ApplicationName == ApplicationName)
            {
                AppTohide = App;
                break;
            }
        }

        if (AppTohide != null)
        {
            var app = AppTohide;
            GameController.Instance.GetActiveTerminal().InstalledApplication.Add(app);
            GameController.Instance.GetActiveTerminal().HiddenApplications.Remove(app);
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
        foreach (var app in GameController.Instance.GetActiveTerminal().InstalledApplication)
        {
            var App = app;

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
            foreach (var app in GameController.Instance.GetActiveTerminal().HiddenApplications)
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
    

    private void ListHiddenApps()
    {
        string HiddenApps = "";
        foreach (var app in GameController.Instance.GetActiveTerminal().HiddenApplications)
        {
            var App = app;
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

    private IEnumerator RemoveApp(string AppToRemove)
    {
        var app = AppToRemove;
        LogAction("Uninstall " + ApplicationDatabase.Instance.GetApp(AppToRemove) + " IP: " + GameController.Instance.GetActiveTerminal().TerminalIP);

        DisplayInput("Uninstalling Application", false);
        yield return new WaitForSeconds(3);
        SoftwareApplicationInstaller.Instance.UninstallProgram(ApplicationDatabase.Instance.GetApp(AppToRemove));
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

        

        switch (ApplicationData[1])
        {
            case "HITW":
                HITW(ApplicationData);
                break;

            case "ConnectTerminal":
                ConnectTerminal(ApplicationData);
                break;
        }
    }

    #region Run Applications
    private void HITW(string[] input)
    {

        var go = new GameObject();
        go.transform.SetParent(this.gameObject.transform);

        if (input.Length < 3)
        {
            go.AddComponent<HoleInTheWallApp>();
        } else
        {
            // TODO: Display Help Message
        }
    }


    private void ConnectTerminal(string[] TerminalData)
    {
        if(TerminalData.Length <= 3)
        {
            if (TerminalData[2] == "disc")
            {
                SceneController.Instance.CloseUserDesktop();
            }
        } else if(TerminalData.Length == 4)
        {
            if(TerminalData[2] == "conn")
            {
                var IP = TerminalData[3];           // Connect TO IP Address
                var go = new GameObject();
                go.AddComponent<ConnectToTerminal>();
                var CTT = go.GetComponent<ConnectToTerminal>();
                string ConnectStatus = CTT.ValidateTerminalConnecting(IP);
                switch(ConnectStatus)
                {
                    case "Blocked":
                        DisplayInput("Unable to connect... Your IP has been blacklisted", false);
                        break;
                    case "Found":
                        SceneController.Instance.OpenTerminalConnector();
                        break;
                    case "Not Found":
                        DisplayInput("Unable To Locate Terminal", false);
                        break;
                }
                
            }
        }

        
    }

    #endregion
}