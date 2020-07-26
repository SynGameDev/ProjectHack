using System;
using UnityEditor;
using UnityEngine;
using System.IO;
using TMPro;


public class Templates : EditorWindow
{
    public string EmailSubject;
    public string EmailContent;

    public string TextFileName;
    public string TextFileExt;
    public string TextFileContent;

    public string MissionMessage;
    
    // List
    public string FoodItem;
    
    [MenuItem("Toolkit/Templates")]
    private static void ShowWindow()
    {
        var window = GetWindow<Templates>();
        window.titleContent = new GUIContent("Templates");
        window.Show();
        
        DatabaseValidation.ValidateDatabases();
    }

    private void OnGUI()
    {
        ToolkitStyles.SetupStyles();
        GUILayout.Label("Generation Templates", ToolkitStyles.PageHeading);
        GUILayout.Label("Emails", ToolkitStyles.SectionHeading);

        EmailSubject = EditorGUILayout.TextField("Email Subject", EmailSubject);
        GUILayout.Label("Email Content");
        EmailContent = EditorGUILayout.TextArea(EmailContent);

        if (GUILayout.Button("Add Email Template"))
        {
            CreateEmailTemplate();
        }
        GUILayout.Space(10);
        ToolkitGlobalMethods.DrawLine(1, Color.gray);
        

        GUILayout.Label("Text File", ToolkitStyles.SectionHeading);
        GUILayout.BeginHorizontal();
        TextFileName = EditorGUILayout.TextField("File Name", TextFileName);
        TextFileExt = EditorGUILayout.TextField("File Extensions", TextFileExt);
        GUILayout.EndHorizontal();

        GUILayout.Label("File Content");
        TextFileContent = EditorGUILayout.TextArea(TextFileContent);

        if (GUILayout.Button("Add Add Text File Template"))
        {
            CreateTextFileTemplate();
        }
        GUILayout.Space(10);
        ToolkitGlobalMethods.DrawLine(2, Color.gray);

        GUILayout.Label("Add Mission Message", ToolkitStyles.SectionHeading);
        GUILayout.Label("Message");
        MissionMessage = EditorGUILayout.TextArea(MissionMessage);
        if (GUILayout.Button("Add Mission Message"))
        {
            AddMissioNMessage();
        }
        
        GUILayout.Space(10);
        ToolkitGlobalMethods.DrawLine(1, Color.gray);
        GUILayout.Label("List", ToolkitStyles.PageHeading);
        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        FoodItem = EditorGUILayout.TextField("Food Item", FoodItem);
        if (GUILayout.Button("Add Food Item"))
        {
            AddFoodItem();
            
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    private void CreateEmailTemplate()
    {
        TemplateDatabases Temp = ReadFromFile();
        
        Email eml = new Email();
        eml.Content = EmailContent;
        eml.Subject = EmailSubject;
        eml.FromUser = "temp";
        eml.ToUser = "temp";
        
        Temp.EmailTemp.Add(eml);
        
        WriteToFile(Temp);
        
        this.ShowNotification(new GUIContent("Added Email Template"));

    }

    private void CreateTextFileTemplate()
    {
        TemplateDatabases temp = ReadFromFile();
        TextFile Text = new TextFile();
        
        // Create the text file
        Text.FileName = TextFileName;
        Text.FileContent = TextFileContent;
        Text.ext = TextFileExt;
        
        temp.TextFileContent.Add(Text);
        WriteToFile(temp);
        
        this.ShowNotification(new GUIContent("Added Text File"));
    }

    private void AddMissioNMessage()
    {
        TemplateDatabases temp = ReadFromFile();
        temp.MissionMessage.Add(MissionMessage);
        
        WriteToFile(temp);
        this.ShowNotification(new GUIContent("Added Mission Message"));
    }

    private void AddFoodItem()
    {
        var temp = ReadFromFile();
        temp.FoodTemp.Add(FoodItem);
        WriteToFile(temp);
        
        this.ShowNotification(new GUIContent("Added Food Item"));
    }

    private TemplateDatabases ReadFromFile()
    {
        TemplateDatabases temp = new TemplateDatabases();
        using (StreamReader r = new StreamReader(Application.streamingAssetsPath + "/Databases/TemplateDatabase.json"))
        {
            temp = JsonUtility.FromJson<TemplateDatabases>(r.ReadToEnd());
        }

        return temp;
    }

    private void WriteToFile(TemplateDatabases temp)
    {
        using (StreamWriter w = new StreamWriter(Application.streamingAssetsPath + "/Databases/TemplateDatabase.json"))
        {
            w.Write(JsonUtility.ToJson(temp));
        }
    }

    private void OnInspectorUpdate()
    {
        this.Repaint();
    }
}
