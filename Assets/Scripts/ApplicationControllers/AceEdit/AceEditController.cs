using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AceEditController : MonoBehaviour {
    public static AceEditController Instance;

    [Header("Settings")]
    private bool Unsaved;

    [Header("Display Settings")]
    [SerializeField] private TMP_InputField _Content;
    [SerializeField] private GameObject _FileListObject;

    [Header("Header Details")]
    [SerializeField] private TMP_InputField _FileTitle;

    private void Awake() {
        CreateSingleInstance();
    }

    private void Start() {
        OpenTextFile();
    }

    public void OpenTextFile() {
        if(GameController.Instance.GetOpenTextFile() == null) {
            OpenFileList();
        } else {
            _FileListObject.SetActive(false);
            _Content.text = GameController.Instance.GetOpenTextFile().FileContent;
            _FileTitle.text = GameController.Instance.GetOpenTextFile().FileName;
        }
    }

    public void ChangeTextFile() {
        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }
        
        OpenTextFile();
    }

    public void SaveTextFile() {
        var content = _Content.text;
        var title = _FileTitle.text;

        GameController.Instance.GetOpenTextFile().FileContent = content;
        GameController.Instance.GetOpenTextFile().FileName = title;
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


    public void OpenFileList() {
        _FileListObject.SetActive(true);
    }

    public void CloseApp() {
        SceneController.Instance.CloseAceEdit();
    }
}