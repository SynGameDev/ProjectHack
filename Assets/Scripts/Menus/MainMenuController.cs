using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainMenuController : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private Sprite _NewGameIcon;
    [SerializeField] private Sprite _LoadGameIcon;

    private void Awake() {
        GetLoadGames();
    }

    private void GetLoadGames() {
        var dirPaths = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] info = dirPaths.GetFiles("*.SynSave");
        foreach(var SaveFile in info) {
            Debug.Log(SaveFile.Name);
        }
    }

}
