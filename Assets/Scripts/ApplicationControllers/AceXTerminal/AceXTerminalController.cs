using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AceXTerminalController : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField _CommandInput;

    [Header("Prefabs")]
    [SerializeField] private GameObject CommandPrefab;

    [Header("Validation")]
    [SerializeField] private List<string> ValidCommand = new List<string>();

    private void Update() {

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
                    }
                }
            }
        }
    }

    private bool FilterInput(string command) {
        if(ValidCommand.Contains(command))
            return true;

        return false;
    }

    private void DownloadFile(string url) {
        foreach(var AppUrl in ApplicationDatabase.Instance.GetCrackedApps()) {
            var appurl = AppUrl as CrackedAppScriptableObject;
            if(appurl.ApplicationURL == url) {
                DownloadApplication(appurl);
                break;
            }
        }
    } 

    private void DownloadApplication(ScriptableObject AppToDownload) {
        
    }

}
