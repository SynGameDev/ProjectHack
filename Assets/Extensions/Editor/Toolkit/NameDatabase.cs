using System;
using System.IO;
using UnityEditor;
using UnityEngine;


public class NameDatabase : EditorWindow
{
    private string Input;
    
    [MenuItem("Toolkit/Names")]
    private static void ShowWindow()
    {
        var window = GetWindow<NameDatabase>();
        window.titleContent = new GUIContent("Names");
        window.Show();

        DatabaseValidation.ValidateDatabases();
    }

    private void OnGUI()
    {
        ToolkitStyles.SetupStyles();
        GUILayout.Label("Name Databases", ToolkitStyles.PageHeading);

        GUILayout.Label("Person Names", ToolkitStyles.SectionHeading);
        
        Input = EditorGUILayout.TextField("New Name", Input);
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add First Name"))
        {
            AddData("FirstName");
        }

        if (GUILayout.Button("Add Last Name"))
        {
            AddData("LastName");
        }

        if (GUILayout.Button("Add Username"))
        {
            AddData("Username");
        }
        GUILayout.EndHorizontal();
        
        
    }

    
    /// <summary>
    /// Add the data to the list & write to the file
    /// </summary>
    /// <param name="type"></param>
    private void AddData(string type)
    {
        var Names = ReadFromFile();
        switch (type)
        {
            case "FirstName":
                Names.FirstNames.Add(Input);
                break;
            case "LastName":
                Names.LastNames.Add(Input);
                break;
            case "Username":
                Names.Usernames.Add(Input);
                break;
                
        }
        
        WriteToFile(Names);
    }
    private void WriteToFile(NamesData names)
    {
        using (StreamWriter w = new StreamWriter(Application.streamingAssetsPath + "/Databases/NamesDatabase.json"))
        {
            w.WriteLine(JsonUtility.ToJson(names));
        }
    }

    private NamesData ReadFromFile()
    {
        using (StreamReader r = new StreamReader(Application.streamingAssetsPath + "/Databases/NamesDatabase.json"))
        {
            return JsonUtility.FromJson<NamesData>(r.ReadToEnd());
        }
    }

    private void OnInspectorUpdate()
    {
        this.Repaint();
    }
}
