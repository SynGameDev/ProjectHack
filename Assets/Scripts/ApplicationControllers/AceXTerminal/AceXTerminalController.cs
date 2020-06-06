using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AceXTerminalController : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField _CommandInput;

    [Header("Transforms")]
    [SerializeField] private Transform _CommandSpawnLocation;

    [Header("Prefabs")]
    [SerializeField] private GameObject CommandPrefab;

    [Header("Validation")]
    [SerializeField] private List<string> ValidCommand = new List<string>();

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) {
            DisplayInput(_CommandInput.text);
            ValidateInput();
        }
    }

    private void DisplayInput(string text) {
        var go = Instantiate(CommandPrefab);
        go.GetComponent<TextMeshProUGUI>().text = text;
        go.transform.SetParent(_CommandSpawnLocation);
        go.transform.localScale = Vector3.one;

    }

    public void ValidateInput() {
        if(_CommandInput.text != "" || _CommandInput.text != null) {
            var FullInput = _CommandInput.text;
            string[] InputSplit = FullInput.Split(' ');

            if(InputSplit[0].Length == 3) {
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
                    }
                } else {
                    DisplayInput("Invalid Command");
                }
            } else {
                DisplayInput("Invalid Command");
            }
        }

        _CommandInput.text = "";
        _CommandInput.ActivateInputField();
    }

    private bool FilterInput(string command) {
        if(ValidCommand.Contains(command))
            return true;

        return false;
    }

    private void DownloadFile(string url) {
        bool Found = false;
        foreach(var AppUrl in ApplicationDatabase.Instance.GetSoftwareApps()) {
            var appurl = AppUrl as ApplicationScriptableObject;
            if(appurl.ApplicationDownloadURL == url) {
                Found = true;
                StartCoroutine(DownloadApplication(appurl));
                break;
            }
        }

        if(Found == false)
            DisplayInput("Unable To Locate Download File");
    } 

    private void RemoveApplication(string ApplicationName) {
        var AppName = ApplicationName.Replace("_", " ");

        foreach(var app in GameController.Instance.GetActiveContract().InstalledApplication) {
            var App = app as ApplicationScriptableObject;

            if(App.ApplicationName == AppName) {
                StartCoroutine(RemoveApp(App));
                break;
            }
        }
    }

    private void HideApplication(string ApplicationName) {
        ScriptableObject AppTohide = null;

        ApplicationName = ApplicationName.Replace("_", " ");
        foreach(var app in GameController.Instance.GetActiveContract().InstalledApplication) {
            var App = app as ApplicationScriptableObject;
            if(App.ApplicationName == ApplicationName) {
                AppTohide = App;
                break;
            }
        }

        if(AppTohide != null) {
            GameController.Instance.GetActiveContract().HiddenApplications.Add(AppTohide);
            GameController.Instance.GetActiveContract().InstalledApplication.Remove(AppTohide);
            GameObject.FindGameObjectWithTag("UserDesktop").GetComponent<DisplayUserDesktop>().UpdateDesktop();
        } else {
            DisplayInput("Application Not Found");
        }
    }

    private void UnhideApplication(string ApplicationName) {
        ScriptableObject AppTohide = null;

        ApplicationName = ApplicationName.Replace("_", " ");
        foreach(var app in GameController.Instance.GetActiveContract().HiddenApplications) {
            var App = app as ApplicationScriptableObject;
            if(App.ApplicationName == ApplicationName) {
                AppTohide = App;
                break;
            }
        }

        if(AppTohide != null) {
            GameController.Instance.GetActiveContract().InstalledApplication.Add(AppTohide);
            GameController.Instance.GetActiveContract().HiddenApplications.Remove(AppTohide);
            GameObject.FindGameObjectWithTag("UserDesktop").GetComponent<DisplayUserDesktop>().UpdateDesktop();
        } else {
            DisplayInput("Application Not Found");
        }
    }

    private void OpenApplication(string ApplicatioName) {
        var AppName = ApplicatioName.Replace("_", " ");
        bool AppFound = false;

        foreach(var app in GameController.Instance.GetActiveContract().InstalledApplication) {
            var App = app as ApplicationScriptableObject;

            if(App.ApplicationName == AppName) {
                AppFound = true;
                SceneController.Instance.OpenApplication(AppName);
                break;
            }
        }

        if(!AppFound) {
            foreach(var app in GameController.Instance.GetActiveContract().HiddenApplications) {
                AppFound = true;
                SceneController.Instance.OpenApplication(AppName);
                break;
            }
        }

        if(!AppFound) {
            DisplayInput("Unable To Locate Application");
        }

    }

    private void ListHiddenApps() {
        string HiddenApps = "";
        foreach(var app in GameController.Instance.GetActiveContract().HiddenApplications) {
            var App = app as ApplicationScriptableObject;
            HiddenApps += " | " + App.ApplicationName;
        }

        DisplayInput(HiddenApps);
    }

    private IEnumerator DownloadApplication(ScriptableObject AppToDownload) {
        DisplayInput("Downloading & Installing");
        yield return new WaitForSeconds(3);
        SoftwareApplicationInstaller.Instance.InstallProgram(AppToDownload);
        DisplayInput("Application Installed");
    }

    private IEnumerator RemoveApp(ScriptableObject AppToRemove) {
        DisplayInput("Uninstalling Application");
        yield return new WaitForSeconds(3);
        SoftwareApplicationInstaller.Instance.UninstallProgram(AppToRemove);
        DisplayInput("Application Uninstalled");
    }

}
