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

        Debug.Log(GameController.Instance.ActiveContract);
        // Check if the charcter has entered the correct data.
        if(_EnteredAddress.text == GameController.Instance.GetActiveContract().TerminalIP) {
            SceneController.Instance.OpenTerminalConnector();
            CloseApp();
        } else {
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
