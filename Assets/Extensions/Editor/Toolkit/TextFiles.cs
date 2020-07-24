using System;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class TextFiles : EditorWindow
{
    private string _TextFileName;                // Name of the text file
    private string _TextFileExt;                // Extension of the text file
    private int _SelectedTerminal;                // Index of the selected file
    private string _TextFileData;
    
    
    [MenuItem("Toolkit/Text Files")]
    private static void ShowWindow()
    {
        var window = GetWindow<TextFiles>();
        window.titleContent = new GUIContent("Text Files");
        window.Show();
        DatabaseValidation.ValidateDatabases();
    }

    private void OnGUI()
    {
        ToolkitStyles.SetupStyles();

        GUILayout.Label("Text Files", ToolkitStyles.PageHeading);
        GUILayout.Label("New Text File", ToolkitStyles.SectionHeading);

        GUILayout.Label("Text File Headers", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        _TextFileName = EditorGUILayout.TextField("File Name", _TextFileName);
        _TextFileExt = EditorGUILayout.TextField("File Extensions", _TextFileExt);
        _SelectedTerminal =
            EditorGUILayout.Popup("Terminal", _SelectedTerminal, ToolkitGlobalMethods.GetTerminalNames());
        GUILayout.EndHorizontal();
        

        GUILayout.Label("Text File Content", ToolkitStyles.SubHeading);
        _TextFileData = EditorGUILayout.TextArea(_TextFileData);

        if (GUILayout.Button("Create Text File"))
        {
            CreateTextFile();
        }
    }

    private void CreateTextFile()
    {
        var db = ToolkitGlobalMethods.GetTextFiles();                // Get the text files
        
        
        // Create the text file
        var TextFile = new TextFile();
        TextFile.FileName = _TextFileName;
        TextFile.ext = _TextFileExt;
        TextFile.FileContent = _TextFileData;

        var terminal =
            ToolkitGlobalMethods.DropdownValueToString(_SelectedTerminal, ToolkitGlobalMethods.GetTerminalNames());
        
        db.Add(TextFile);
        
        var new_db = new TextFileDatabase();

        new_db.TextFileData = db;

        // Setup the text file
        using (StreamWriter w = new StreamWriter(Application.streamingAssetsPath + "/Databases/TextFileDatabase.json"))
        {
            w.WriteLine(JsonUtility.ToJson(new_db));            
        }

        var term = ToolkitGlobalMethods.GetTerminalList();
        TerminalDatabase data = new TerminalDatabase();
        
        foreach (var item in term)
        {
            if (item.TemrinalName == terminal)
            {
                item.TextFileList.Add(TextFile);
                data.Terminals = term;
                break;
            }
        }

        // Update the terminal databases
        using (StreamWriter w = new StreamWriter(Application.streamingAssetsPath + "/Databases/TerminalDatabases.json"))
        {
            w.WriteLine(JsonUtility.ToJson(data));
        }

        
        this.ShowNotification(new GUIContent("Text File Created"));
    }

    private void OnInspectorUpdate()
    {
        this.Repaint();
    }
}
