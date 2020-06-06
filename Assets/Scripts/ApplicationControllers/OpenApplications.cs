using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenApplications : MonoBehaviour
{
    [SerializeField] private string _ApplicationToOpen;

    public void OpenApplication() {
        switch(_ApplicationToOpen) {
            case "Software Center":
                OpenSoftwareCenter();
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
