using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MissionObjectives : EditorWindow
{
    private int _SelectedSequence;
    private int _SelectedMission;

    private int _SelectedAction;
    private int _SelectedApp;
    private int _SelectedTerminal;
    
    
    // Dropdowns
    private string[] _Actions = {"Install", "Uninstall", "Hide", "Locate"};



    [MenuItem("Toolkit/Mission Objects")]
    public static void OnStart()
    {
        EditorWindow.GetWindow<MissionObjectives>();
        DatabaseValidation.ValidateDatabases();
    }

    private void OnGUI()
    {
        ToolkitStyles.SetupStyles();
        
        GUILayout.Label("Mission Objectives", ToolkitStyles.PageHeading);
        GUILayout.Label("New Mission Objectives", ToolkitStyles.SectionHeading);
        
        GUILayout.Label("Mission Details", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        _SelectedSequence =
            EditorGUILayout.Popup("Sequence", _SelectedSequence, ToolkitGlobalMethods.GetSequenceArray());

        _SelectedMission = EditorGUILayout.Popup("Mission", _SelectedMission,
            ToolkitGlobalMethods.MissionNames(
                ToolkitGlobalMethods.DropdownValueToString(_SelectedSequence,
                    ToolkitGlobalMethods.GetSequenceArray())));
        
        
        GUILayout.EndHorizontal();
        
        GUILayout.Label("Objectives");
        GUILayout.BeginHorizontal();
        _SelectedAction = EditorGUILayout.Popup("Action", _SelectedAction, _Actions);
        _SelectedApp = EditorGUILayout.Popup("Application", _SelectedApp, ToolkitGlobalMethods.GetApplicationArray());
        _SelectedTerminal = EditorGUILayout.Popup("Terminal", _SelectedTerminal, ToolkitGlobalMethods.GetTerminalNames());
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Add Objective"))
        {
            CreateObjective();                     // TODO: Validate the objective before creating it (e.g Make sure an app is installed before uninstalling objective)                       
        }
        
        
    }

    private void CreateObjective()
    {
        MissionDatabases m_db = new MissionDatabases();
        
        // Set the sequence and the mission
        string seq =
            ToolkitGlobalMethods.DropdownValueToString(_SelectedSequence, ToolkitGlobalMethods.GetSequenceArray());
        var mis = ToolkitGlobalMethods.DropdownValueToString(_SelectedMission, ToolkitGlobalMethods.MissionNames(seq));

        // Set the object
        var act = ToolkitGlobalMethods.DropdownValueToString(_SelectedAction, _Actions);                // Set the action
        var app = ToolkitGlobalMethods.DropdownValueToString(_SelectedApp, ToolkitGlobalMethods.GetApplicationArray());                // Set the application
        var term = ToolkitGlobalMethods.DropdownValueToString(_SelectedTerminal,                
            ToolkitGlobalMethods.GetTerminalNames());                // Set the terminal

        foreach (var item in ToolkitGlobalMethods.GetTerminalList())
        {
            if (item.TemrinalName == term)
            {
                term = item.TerminalIP;
                break;
            }
        }
        
        // TODO: Find Terminal IP


        var obj = act + " " + app + " IP: " + term;
        
        var Mission = new MissionInfo();
        using (StreamReader r =
            new StreamReader(Application.streamingAssetsPath + "/Sequences/" + seq + "/" + mis + ".json"))
        {
            Mission = JsonUtility.FromJson<MissionInfo>(r.ReadToEnd());
        }
        
        Mission.MissionObjectives.Add(obj);                // Add the mission to the objectives

        
        // Update the mission file
        using (StreamWriter w =
            new StreamWriter(Application.streamingAssetsPath + "/Sequences/" + seq + "/" + mis + ".json"))
        {
            w.WriteLine(JsonUtility.ToJson(Mission));                // Write line to file
        }
        
        this.ShowNotification(new GUIContent("Objective Create"));                    // Set the notification
    }


    private void OnInspectorUpdate()
    {
        this.Repaint();
    }
}
