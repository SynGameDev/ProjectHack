using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFileDatabase : MonoBehaviour {

    public static TextFileDatabase Instance;
    public List<TextFile> FileList = new List<TextFile>();

    public TextFile FindFile(string filename) {
        foreach(var file in FileList) {
            if(file.FileName == filename) 
                return file;
        }

        return null;
    }
}