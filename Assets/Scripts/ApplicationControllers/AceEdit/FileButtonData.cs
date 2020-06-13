using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FileButtonData : MonoBehaviour
{
    private TextFile _File;
    [SerializeField] private TextMeshProUGUI _ButtonText;

    public void SelectFile() {
        GameController.Instance.SetOpenTextFile(_File);
        AceEditController.Instance.OpenTextFile();
        
    }

    public void SetFile(TextFile file) {
         _File = file;
         _ButtonText.text = file.FileName;
    }
    public TextFile GetFile() => _File;
}
