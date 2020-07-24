using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConnectToTerminal : MonoBehaviour
{

    [Header("Application Input Fields")]
    [SerializeField] private TMP_InputField _EnteredAddress;                       // IP Address Entered

    [SerializeField] private GameObject _ErrorText;

    [SerializeField] private GameObject _SuccessText;
    [SerializeField] private GameObject _ConnectButton;
    [SerializeField] private float _WaitTime;

    private void Awake()
    {

    }
    
    public string ValidateTerminalConnecting(string ip) {


        foreach(var terminal in GameController.Instance.GetAllTerminals()) {
            if(terminal.TerminalIP == ip) {


                if(terminal.BlockedIPs.Contains(GameController.Instance.GetPlayerData().PlayerIP)) {
                    return "IP Blocked";
                } else {
                    if(terminal.BackDoorInstalled) {
                        GameController.Instance.SetActiveTerminal(terminal);
                        return "Backdoor";
                    } else {
                        GameController.Instance.SetActiveTerminal(terminal);
                        return "Found";
                    }
                }
            }
        }

        return "Not Found";
    }

/*
    private IEnumerator DisplayText() {
        _SuccessText.SetActive(true);                   // Show the success text
        _ConnectButton.SetActive(false);                // Hide the connect button

        yield return new WaitForSeconds(3);             // Wait the tie
        SceneController.Instance.OpenUserDesktop();             // Open the user desktop
    }
*/

    // Close the application
    public void CloseApp() {
        SceneController.Instance.CloseConnectToUser();
    }
}
