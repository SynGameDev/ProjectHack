using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AceEditController : MonoBehaviour {
    public static AceEditController Instance;

    [Header("Settings")]
    private bool Unsaved;

    [Header("Display Settings")]
    [SerializeField] private TextMeshProUGUI _Content;
    private void Awake() {
        CreateSingleInstance();
    }

    private void Start() {
        OpenTextFile();
    }

    private void OpenTextFile() {
        var file = GameController.Instance.GetOpenTextFile();
        
        _Content.text = file.FileContent;
    }

    public void ChangeTextFile() {
        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }
        
        OpenTextFile();
    }

    public void UpdateTextFile() {
        GameController.Instance.GetOpenTextFile().FileContent = _Content.text;

        GameController.Instance.GetOpenTextFile().LastUpdateInfo = DateTimeController.Instance.GetDateTime();

        // TODO: Change Save Icon For a time
    }

    private void CreateSingleInstance() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
}