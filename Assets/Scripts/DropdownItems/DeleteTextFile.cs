using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTextFile : MonoBehaviour
{
    public void DeleteFile() {
        var FileObject = GameController.Instance.GetActiveDropdownItem();

        GameController.Instance.GetActiveTerminal().TextFileList.Remove(FileObject.GetComponent<FileButtonData>().GetFile());
        Destroy(FileObject);
        //RightClickObject.Instance.HideDropdownMenu();
    }
}
