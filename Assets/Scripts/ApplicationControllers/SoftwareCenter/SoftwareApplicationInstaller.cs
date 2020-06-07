using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SoftwareApplicationInstaller : MonoBehaviour
{
    public static SoftwareApplicationInstaller Instance;

    [Header("Installer Application")]
    [SerializeField] private GameObject InstallApp;
    [SerializeField] private Image _SliderBar;
    [SerializeField] private TextMeshProUGUI _ActionText;
    float SliderPercentage = 0f;
    private string InstallerType;

    [Header("Software Center")]
    [SerializeField] private SoftwareCenterMainController _SoftwareCenter;
    

    private void Awake() {

        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }

        InstallerType = GameObject.FindGameObjectWithTag("SoftwareCenter").GetComponent<SoftwareCenterMainController>().GetApplicationAction();

        if(SceneController.Instance.CheckIfLoaded(SceneController.Instance.SoftwareCenter))
            _SoftwareCenter = GameObject.FindGameObjectWithTag("SoftwareCenter").GetComponent<SoftwareCenterMainController>();
    }

    private void Update() {
        IncreaseSlider();
        CheckForComplete();
    }

    private void IncreaseSlider() {
        

        SliderPercentage += 0.3f * Time.deltaTime;

        _SliderBar.fillAmount = SliderPercentage;

    }

    private void CheckForComplete() {
        if(SliderPercentage >= 1) 
            StartCoroutine(FinishInstall());
    }

    private IEnumerator FinishInstall() {
        UpdateText();
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
        GameController.Instance.GetActiveContract().InstalledApplication.Add(AppToInstall);
        GameObject.FindGameObjectWithTag("UserDesktop").GetComponent<DisplayUserDesktop>().UpdateDesktop();
        var i = AppToInstall as ApplicationScriptableObject;
        LogAction("Install " + i.ApplicationName);
    }

    public void UninstallProgram(ScriptableObject AppToRemove) {
        GameController.Instance.GetActiveContract().InstalledApplication.Remove(AppToRemove);
        GameObject.FindGameObjectWithTag("UserDesktop").GetComponent<DisplayUserDesktop>().UpdateDesktop();

        var i = AppToRemove as ApplicationScriptableObject;
        LogAction("Install " + i.ApplicationName);
    }

    private void LogAction(string action) {
        GameController.Instance.GetActiveContract().ActionLog.Add(action);
    }

}
