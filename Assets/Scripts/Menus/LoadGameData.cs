using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadGameData : MonoBehaviour
{
    [HideInInspector] public string LoadGameName;
    public TextMeshProUGUI LoadGameText;

    public void LoadGame() {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        StartCoroutine(LeaveMainMenu());
    }

    private IEnumerator LeaveMainMenu() {
        yield return new WaitForSeconds(0.5f);
        SaveGameSystem.Instance.LoadGame(LoadGameName);
        SceneController.Instance.CloseMainMenu();
    }
}
