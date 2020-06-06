using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SoftwareApplicationInstaller : MonoBehaviour
{
    [Header("Installer Application")]
    [SerializeField] private GameObject InstallApp;
    [SerializeField] private Image _SliderBar;
    [SerializeField] private TextMeshProUGUI _ActionText;
    float SliderPercentage = 0f;
    private string InstallerType;

    [Header("Software Center")]
    [SerializeField] private SoftwareCenterMainController _SoftwareCenter;
    

    private void Awake() {
        InstallerType = GameObject.FindGameObjectWithTag("SoftwareCenter").GetComponent<SoftwareCenterMainController>().GetApplicationAction();
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
        this.gameObject.SetActive(false);
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

    private void InstallProgram(ScriptableObject AppToInstall) {
        GameController.Instance.GetActiveContract().InstalledApplication.Add(AppToInstall);
        GameObject.FindGameObjectWithTag("UserDesktop").GetComponent<DisplayUserDesktop>().UpdateDesktop();
    }

    private void UninstallProgram(ScriptableObject AppToRemove) {
        GameController.Instance.GetActiveContract().InstalledApplication.Remove(AppToRemove);
        GameObject.FindGameObjectWithTag("UserDesktop").GetComponent<DisplayUserDesktop>().UpdateDesktop();
    }

}
