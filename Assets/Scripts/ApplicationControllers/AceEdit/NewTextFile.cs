using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTextFile : MonoBehaviour
{
    public void CreateFile() {
        TextFile NewFile = new TextFile();
        NewFile.FileName = "New File Name";
        NewFile.FileContent = "";

        GameController.Instance.GetActiveTerminal().TextFileList.Add(NewFile);
        GameController.Instance.SetOpenTextFile(NewFile);

        AceEditController.Instance.OpenTextFile();

    }
}
