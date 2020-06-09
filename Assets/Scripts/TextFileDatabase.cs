using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFileDatabase : MonoBehaviour {
    public List<TextFile> FileList = new List<TextFile>();

    public void FindFile(string filename) {
        foreach(var file in FileList) {
            if(file.FileName == filename) 
                return file;
        }

        return null;
    }
}