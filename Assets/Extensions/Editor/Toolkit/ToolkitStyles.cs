using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEditor;
using UnityEditor.VersionControl;

public static class ToolkitStyles
{
    // --- TEXT --- //
    public static GUIStyle PageHeading = new GUIStyle();
    public static GUIStyle SectionHeading = new GUIStyle();
    public static GUIStyle SubHeading = new GUIStyle();
    public static GUIStyle StandardText =  new GUIStyle();
    public static GUIStyle MessageText = new GUIStyle();
    
    // --- BUTTONS --- //
    public static GUIStyle Button = new GUIStyle();

    public static void SetupStyles()
    {
        PageHeadingStyle();
        SectionHeadingStyle();
        SubHeadingSetup();
        MessageTextSetup();
        ButtonSetup();
    }

    private static void PageHeadingStyle()
    {
        PageHeading.alignment = TextAnchor.MiddleCenter;
        PageHeading.margin = new RectOffset(0, 0, 10, 10);
        PageHeading.fontSize = 24;
        PageHeading.fontStyle = FontStyle.Bold;
    }

    private static void SectionHeadingStyle()
    {
        SectionHeading.alignment = TextAnchor.MiddleCenter;
        SectionHeading.margin = new RectOffset(0, 0, 10, 10);
        SectionHeading.fontSize = 16;
        SectionHeading.fontStyle = FontStyle.Bold;
    }

    private static void SubHeadingSetup()
    {
        SubHeading.margin = new RectOffset(5, 0, 10, 0);
        SubHeading.fontStyle = FontStyle.Bold;
    }

    private static void MessageTextSetup()
    {
        MessageText.fontSize = 8;
        MessageText.fontStyle = FontStyle.Bold;
    }

    private static void ButtonSetup()
    {
        Button.alignment = TextAnchor.MiddleCenter;
    }
}
