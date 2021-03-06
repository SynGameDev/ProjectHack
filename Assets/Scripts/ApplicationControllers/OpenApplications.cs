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
        SceneController.Instance.OpenApplication(GetComponentInChildren<TextMeshProUGUI>().text);

        /*
        if(_Text.text == "AceEdit") {
            GameController.Instance.SetOpenTextFile(TextFileDatabase.Instance.FindFile(_Text.text));
        }
        */
    }

    private void Start() {
       // _ApplicationToOpen = _Text.text;
    }


    

    private void OpenSoftwareCenter() => SceneController.Instance.OpenSoftwareCenter();


    // Getters


    // Setters
}