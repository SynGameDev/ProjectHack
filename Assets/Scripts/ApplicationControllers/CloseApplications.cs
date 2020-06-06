using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseApplications : MonoBehaviour
{
    [SerializeField] private string _ApplicationToClose;

    public void CloseApp() {
        switch(_ApplicationToClose) {
            case "Software Center":
                SceneController.Instance.CloseSoftwareCenter();
                break;
        }
    }
}
