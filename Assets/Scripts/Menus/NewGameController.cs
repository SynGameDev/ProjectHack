using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameController : MonoBehaviour
{
    [SerializeField] private GameObject NewUserPanel;

    [Header("New User Settings")]
    [SerializeField] private TMP_InputField _NewUsername;

    private bool _NewGamePanel = false;

    private void Awake()
    {
        NewUserPanel = MainMenuController.Instance.GetNewGamePanel();
    }

    private void Update()
    {
        if (_NewGamePanel)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartNewGame();
            }
        }
    }

    public void StartNewGame()
    {
        var username = MainMenuController.Instance.NewUsername();
        var namevalid = false;

        var dirPaths = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dirPaths.GetFiles("*.SynSave");
        foreach (var SaveFile in info)
        {
            Debug.Log(SaveFile.Name);
            if (SaveFile.Name == username + ".synsave")
            {
                namevalid = true;
            }
        }

        if (!namevalid)
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            StartCoroutine(StartTheGame(username));
        }
        else
        {
            Debug.Log("Username has been taken");
            // TODO: Display error message in game
        }
    }

    private IEnumerator StartTheGame(string username)
    {
        yield return new WaitForSeconds(0.5f);
        SaveGameSystem.Instance.SaveGame(username);
        SaveGameSystem.Instance.LoadGame(username + ".synsave");
        SceneController.Instance.CloseMainMenu();
    }

    public void ShowNewGamePanel()
    {
        NewUserPanel.SetActive(true);
        GameObject.FindGameObjectWithTag("ItemContainer").GetComponent<Animator>().SetBool("Hide", true);
        this.gameObject.GetComponent<Animator>().SetBool("Show", true);
        _NewGamePanel = true;
    }
}