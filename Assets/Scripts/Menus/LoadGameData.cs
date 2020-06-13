using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameData : MonoBehaviour
{
    [HideInInspector] public string LoadGameName;

    public void LoadGame() {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        SaveGameSystem.Instance.LoadGame(LoadGameName);
        SceneController.Instance.CloseMainMenu();
    }
}
