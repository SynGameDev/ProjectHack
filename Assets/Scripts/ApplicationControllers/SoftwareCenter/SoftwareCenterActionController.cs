using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoftwareCenterActionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ActionText;
    [SerializeField] private GameObject Installer;
    private string ActionType;

    public void Actioned() {
        GameObject.FindGameObjectWithTag("SoftwareCenter").GetComponent<SoftwareCenterMainController>().SetActionText(ActionText.text);
        SceneController.Instance.OpenInstallProgress();

    }
}
