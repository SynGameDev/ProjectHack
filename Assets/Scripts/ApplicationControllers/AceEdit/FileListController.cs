using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileListController : MonoBehaviour
{
    [SerializeField] private GameObject _FileButtonPrefab;
    [SerializeField] private Transform _SpawnPoint;

    private void Awake() {
        GetAllFiles();
    }

    private void GetAllFiles() {
        List<TextFile> files = GameController.Instance.GetActiveContract().Terminal.TextFileList;

        foreach(var file in files) {
            CreateButton(file);
        }
    }


    public void CreateButton(TextFile file) {
            var go = Instantiate(_FileButtonPrefab);
            go.GetComponent<FileButtonData>().SetFile(file);
            go.transform.SetParent(_SpawnPoint);
            go.transform.localScale = Vector3.one;
    }

}
