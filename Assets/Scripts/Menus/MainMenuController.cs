using System.IO;
using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance;

    [Header("Prefabs")]
    [SerializeField] private GameObject _NewGameIcon;

    [SerializeField] private GameObject _LoadGameIcon;

    [Header("Spawn Points & Containers")]
    [SerializeField] private Transform _ButtonTransform;

    [Header("Other Settings")]
    [SerializeField] private GameObject NewUserPanel;

    [SerializeField] private TMP_InputField _UsernameInput;
    private int CurrentObjects = 0;

    private void Awake()
    {
        Instance = this;

        GetLoadGames();
        SetupNewGames();
    }

    private void GetLoadGames()
    {
        var dirPaths = new DirectoryInfo("C:/Dirty Rats/");
        FileInfo[] info = dirPaths.GetFiles("*.SynSave");
        foreach (var SaveFile in info)
        {
            var go = Instantiate(_LoadGameIcon);
            string[] name = SaveFile.Name.Split('.');

            go.transform.SetParent(_ButtonTransform);
            go.transform.localScale = Vector3.one;

            go.GetComponent<LoadGameData>().LoadGameName = SaveFile.Name;
            go.GetComponent<LoadGameData>().LoadGameText.text = name[0];
            CurrentObjects += 1;
        }
    }

    private void SetupNewGames()
    {
        if (CurrentObjects <= 5)
        {
            var total = 5 - CurrentObjects;
            for (int i = 0; i < total; i++)
            {
                var go = Instantiate(_NewGameIcon);
                go.transform.SetParent(_ButtonTransform);
                go.transform.localScale = Vector3.one;
            }
        }
    }

    public GameObject GetNewGamePanel()
    {
        return NewUserPanel;
    }

    public string NewUsername() => _UsernameInput.text;

    // TODO: Quit Game
    // TODO: Open Settings Menu
}