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
    
    public void ValidateTerminalConnecting() {

        var found = false;
        foreach(var terminal in GameController.Instance.GetAllTerminals()) {
            if(terminal.TerminalIP == _EnteredAddress.text) {

                if(terminal.BackDoorInstalled) {
                    SceneController.Instance.OpenUserDesktop();
                } else {
                    SceneController.Instance.OpenTerminalConnector();
                    GameController.Instance.SetActiveTerminal(terminal);
                }

                GameController.Instance.SetActiveTerminal(terminal);
                
                CloseApp();
                break;
            }
        }

        if(!found) {
            _ErrorText.SetActive(true);
            _ErrorText.GetComponent<TextMeshProUGUI>().text = "Unable to find specified terminal";
            _ErrorText.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
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
