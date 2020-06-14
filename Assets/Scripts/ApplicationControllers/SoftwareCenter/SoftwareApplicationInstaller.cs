using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SoftwareApplicationInstaller : MonoBehaviour
{
    public static SoftwareApplicationInstaller Instance;

    [Header("Installer Application")]
    [SerializeField] private GameObject InstallApp;                     // This gameobject
    [SerializeField] private Image _SliderBar;                          // Progress Slider
    [SerializeField] private TextMeshProUGUI _ActionText;                   // Action Text
    float SliderPercentage = 0f;                // Current slider percentage
    private string InstallerType;               // Type of installer

    [Header("Software Center")]
    [SerializeField] private SoftwareCenterMainController _SoftwareCenter;          // Software Center
    

    private void Awake() {

            // Create instance
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }

        InstallerType = GameObject.FindGameObjectWithTag("SoftwareCenter").GetComponent<SoftwareCenterMainController>().GetApplicationAction();     // Set the installer

        // If downloading form software center than assign the software center
        if(SceneController.Instance.CheckIfLoaded(SceneController.Instance.SoftwareCenter))
            _SoftwareCenter = GameObject.FindGameObjectWithTag("SoftwareCenter").GetComponent<SoftwareCenterMainController>();
    }

    private void Update() {
        IncreaseSlider();
        CheckForComplete();
    }


    // Increase teh download slider
    private void IncreaseSlider() {
        

        SliderPercentage += 0.3f * Time.deltaTime;

        _SliderBar.fillAmount = SliderPercentage;

    }


    // Check if the download has completed
    private void CheckForComplete() {
        if(SliderPercentage >= 1) 
            StartCoroutine(FinishInstall());
    }

    
    private IEnumerator FinishInstall() {
        UpdateText();               // Display Finised Test
        yield return new WaitForSeconds(3);
        UpdateApplication();            
        SceneController.Instance.CloseInstallProgress();
    }

    private void UpdateText() {
        switch(InstallerType) {
            case "Install":
                _ActionText.text = "Installed";
                break;
            case "Uninstall":
                _ActionText.text = "Uninstalled";
                break;
            // TODO: Implement Purchasing
        }
    }

    private void UpdateApplication() {
        switch(InstallerType) {
            case "Install":
                _ActionText.text = "Installed";
                InstallProgram(_SoftwareCenter.GetViewingApp());
                break;
            case "Uninstall":
                _ActionText.text = "Uninstalled";
                UninstallProgram(_SoftwareCenter.GetViewingApp());
                break;
            // TODO: Implement Purchasing
        }
    }

    public void InstallProgram(ScriptableObject AppToInstall) {
        var app = AppToInstall as ApplicationScriptableObject;
        GameController.Instance.GetActiveContract().Terminal.InstalledApplication.Add(app.AppData.ApplicationID);
        GameObject.FindGameObjectWithTag("UserDesktop").GetComponent<DisplayUserDesktop>().UpdateDesktop();
        var i = AppToInstall as ApplicationScriptableObject;
        LogAction("Install " + i.AppData.ApplicationName + " IP: " + GameController.Instance.GetActiveTerminal().TerminalIP);
    }

    public void UninstallProgram(ScriptableObject AppToRemove) {
        var app = AppToRemove as ApplicationScriptableObject;
        GameController.Instance.GetActiveContract().Terminal.InstalledApplication.Remove(app.AppData.ApplicationID);
        GameObject.FindGameObjectWithTag("UserDesktop").GetComponent<DisplayUserDesktop>().UpdateDesktop();

        switch(app.AppData.ApplicationName) {
            case "Hole In The Wall":
                StartCoroutine(HoleInTheWallApp.Instance.RemoveApp());
                break;
        }

        var i = AppToRemove as ApplicationScriptableObject;
        LogAction("Uninstall " + i.AppData.ApplicationName + " IP: " + GameController.Instance.GetActiveTerminal().TerminalIP);
    }

    private void LogAction(string action) {
        GameController.Instance.GetActiveContract().ActionLog.Add(action);
    }

}
