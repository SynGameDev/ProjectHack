using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewingSoftwareApp : MonoBehaviour
{
    private ScriptableObject _App;

    [Header("Content Objects")]
    [SerializeField] private Image _AppIcon;
    [SerializeField] private TextMeshProUGUI _AppTitle;
    [SerializeField] private TextMeshProUGUI _AppDescription;
    [SerializeField] private TextMeshProUGUI _InstallButton;
    [SerializeField] private TextMeshProUGUI _Cost;

    [Header("Software Center Objects")]
    [SerializeField] private SoftwareCenterMainController _SoftwareCenter;

    private void Awake() {
        _App = _SoftwareCenter.GetViewingApp();
        SetupDisplay();
    }

    private void SetupDisplay() {
        var App = _App as ApplicationScriptableObject;

        _AppIcon.sprite = App.ApplicationIcon;
        _AppTitle.text = App.ApplicationName;
        _AppDescription.text = App.ApplicationDescription;

        if(_SoftwareCenter.CheckIfInstalled(_App)) {
            _Cost.gameObject.SetActive(false);
            _InstallButton.text = "Uninstall";
        } else {
            _Cost.gameObject.SetActive(true);
            if(App.PurchaseAmount > 0) {
                _Cost.text = App.PurchaseAmount.ToString();
                _InstallButton.text = "Buy";
            } else {
                _Cost.text = "Free";
                _InstallButton.text = "Install";
            }
        }

    }

    public void CloseWindow() {
        this.gameObject.SetActive(false);
    }

}
