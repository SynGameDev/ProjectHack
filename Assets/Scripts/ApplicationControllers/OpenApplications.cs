using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OpenApplications : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string _ApplicationToOpen;
    [SerializeField] private TextMeshProUGUI _Text;

    public void OnPointerClick(PointerEventData pointerEventData) {
        OpenApplication();
    }

    private void Start() {
       // _ApplicationToOpen = _Text.text;
    }

    public void OpenApplication() {
        var app = GetComponentInChildren<TextMeshProUGUI>().text;
        Debug.Log(app);
        switch(app) {
            case "Software Center":
                OpenSoftwareCenter();
                break;
            case "AceXTerminal":
                SceneController.Instance.OpenAceXTerminal();
                break;

        }
    }

    private void OpenSoftwareCenter() => SceneController.Instance.OpenSoftwareCenter();


    // Getters


    // Setters
    public void SetAppToOpen(string app) {
        _ApplicationToOpen = app;
    }
}
