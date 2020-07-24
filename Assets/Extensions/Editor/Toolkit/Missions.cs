using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Missions : EditorWindow
{

    private string _New_SequenceName;

    private string _NewMissionName;
    private string _NewMissionOwner;
    private int _NewSelectedSequence;
    private int _NewSelectedTerminal;
    private string _NewMissionEmail;
    
    [MenuItem("Toolkit/Missions")]
    public static void OnStart()
    {
        EditorWindow.GetWindow<Missions>();
        DatabaseValidation.ValidateDatabases();
        
    }

    private void OnGUI()
    {
        ToolkitStyles.SetupStyles();
        GUILayout.Label("New Mission Sequence", ToolkitStyles.SectionHeading);
        GUILayout.BeginHorizontal();
        _New_SequenceName = EditorGUILayout.TextField("Sequence Name", _New_SequenceName);
        if (GUILayout.Button("Create Sequence"))
        {
            if (ValidateSequence())
            {
                CreateSequence();
            }
            else
            {
                this.ShowNotification(new GUIContent("Name aleady used"));
            }
        }
        GUILayout.EndHorizontal();
        
        GUILayout.Space(20);
        ToolkitGlobalMethods.DrawLine(1, Color.gray);
        
        GUILayout.Label("New Mission", ToolkitStyles.SectionHeading);
        GUILayout.Label("Mission Details", ToolkitStyles.SubHeading);
        GUILayout.BeginHorizontal();
        _NewMissionName = EditorGUILayout.TextField("New Mission Name", _NewMissionName);
        _NewMissionOwner = EditorGUILayout.TextField("Mission Owner", _NewMissionOwner);
        _NewSelectedSequence =
            EditorGUILayout.Popup("Sequence", _NewSelectedSequence, ToolkitGlobalMethods.GetSequenceArray());
        GUILayout.EndHorizontal();
        
        GUILayout.Label("Email Details", ToolkitStyles.SubHeading);
        _NewMissionEmail = EditorGUILayout.TextArea(_NewMissionEmail);

        if (GUILayout.Button("Create Mission"))
        {
            CreateMission();
        }
    }

    private bool ValidateSequence()
    {
        var name = _New_SequenceName;
        var list = ToolkitGlobalMethods.GetSequenceDatabase();

        foreach (var item in list)
        {
            if (item == name)
                return false;
        }

        return true;
    }

    private void CreateSequence()
    {
        var data = ToolkitGlobalMethods.GetSequenceDatabase();
        data.Add(_New_SequenceName);
        using (StreamWriter w = new StreamWriter(Application.streamingAssetsPath + "/Databases/SequenceDatabase.json"))
        {
            SequenceDatabase sdb = new SequenceDatabase();
            sdb.Sequences = data;
            w.WriteLine(JsonUtility.ToJson(sdb));
        }
        
        Directory.CreateDirectory(Application.streamingAssetsPath + "/Sequences/" + _New_SequenceName);
        using (StreamWriter w = new StreamWriter(Application.streamingAssetsPath + "/Sequences/" + _New_SequenceName +
                                                 "/MissionDatabases.json"))
        {
            MissionDatabases db = new MissionDatabases();
            string d = JsonUtility.ToJson(db);
            Debug.Log(d);
            w.WriteLine(d);                    // Write the line
        }
        
        this.ShowNotification(new GUIContent("Sequence Has Been Created"));
    }

    private void CreateMission()
    {
        MissionInfo mission = new MissionInfo();
        mission.MissionName = _NewMissionName;
        mission.MissionOwner = _NewMissionOwner;
        mission.MissionMessage = _NewMissionEmail;
        mission.MissionSequence =
            ToolkitGlobalMethods.DropdownValueToString(_NewSelectedSequence, ToolkitGlobalMethods.GetSequenceArray());

        var dir = Application.streamingAssetsPath + "/Sequences/" + mission.MissionSequence + "/";

        using (StreamWriter w = new StreamWriter(dir + mission.MissionName + ".json"))
        {
            w.WriteLine(JsonUtility.ToJson(mission));
        }

        var missionsdb = ToolkitGlobalMethods.GetMissionList(mission.MissionSequence);
        missionsdb.Add(mission.MissionName);
        using (StreamWriter w = new StreamWriter(Application.streamingAssetsPath + "/Sequences/" +
                                                 mission.MissionSequence + "/MissionDatabases.json"))
        {
            MissionDatabases mis = new MissionDatabases();
            mis.Missions = missionsdb;
            w.WriteLine(JsonUtility.ToJson(mis));
        }
        
        this.ShowNotification(new GUIContent("Mission Created"));
        
    }

    private void OnInspectorUpdate()
    {
        this.Repaint();
    }
}
