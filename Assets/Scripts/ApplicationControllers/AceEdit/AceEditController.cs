using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AceEditController : MonoBehaviour {
    public static AceEditController Instance;

    [Header("Settings")]
    private bool Unsaved;

    [Header("Display Settings")]
    [SerializeField] private GameObject _LinePrefab;
    [SerializeField] private Transform _EditorContentContainer;
    private void Awake() {
        CreateSingleInstance();
    }

    private void Start() {
        OpenTextFile();
    }

    private void OpenTextFile() {
        var file = GameController.Instance.GetOpenTextFile();
        
        foreach(var line in file.LineData) {
            var go = Instantiate(line);
            go.GetComponent<TextMeshProUGUI>().text = line;

            go.transform.SetParent(_EditorContentContainer);
            go.transform.localScale = Vector3.one;
        }
    }

    public void ChangeTextFile() {
        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }
        
        OpenTextFile();
    }

    public void UpdateTextFile() {
        GameController.Instance.GetOpenTextFile().Clear();

        foreach(var child in transform) {
            GameController.Instance.GetOpenTextFile().Add(child.GetComponent<TextMeshProUGUI>().text);
        }

        GameController.Instance.GetOpenTextFile().LastUpdateInfo = DateTimeController.Instance.GetTime() + " " + DateTimeController.Instance.GetDate();

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