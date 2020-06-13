using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class NewGameController : MonoBehaviour
{
    [SerializeField] private GameObject NewUserPanel;

    [Header("New User Settings")]
    [SerializeField] private TMP_InputField _NewUsername;

    private bool _NewGamePanel = false;

    private void Awake() {
        NewUserPanel = MainMenuController.Instance.GetNewGamePanel();
    }

    private void Update() {
        if(_NewGamePanel) {
            if(Input.GetKeyDown(KeyCode.Return)) {
                StartNewGame();
            }
        }
    }

    public void StartNewGame() {
        var username = _NewUsername.text;
        var namevalid = false;

        var dirPaths = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dirPaths.GetFiles("*.SynSave");
        foreach(var SaveFile in info) {
            if(SaveFile.Name == username + ".synsave") {
                namevalid = true;
            }
        }

        if(namevalid) {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
            SaveGameSystem.Instance.SaveGame(username);
            SaveGameSystem.Instance.LoadGame(username + ".synsave");
            SceneController.Instance.CloseMainMenu();
            

        } else {
            Debug.Log("Username has been taken");
            // TODO: Display error message in game
        }
    }

    public void ShowNewGamePanel() {
        NewUserPanel.SetActive(true);
        _NewGamePanel = true;
    }
}
