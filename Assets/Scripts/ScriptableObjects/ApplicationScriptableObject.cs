using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="Application", menuName="Scriptable Objects/Applications/New Application", order = 1)]
public class ApplicationScriptableObject : ScriptableObject
{

    [Header("Application Deatils")]
    public string ApplicationName;
    [TextArea]
    public string ApplicationDescription;
    public List<Sprite> ApplicationScreenshots = new List<Sprite>();
    public Sprite ApplicationIcon;

    [Header("Server Information")]
    public bool CrackedApplication;
    public string ApplicationDownloadURL;

    [Header("Store Information")]
    [Tooltip("Amount to obtain the application")] public int PurchaseAmount;
    [Tooltip("Space the application will take on the HDD")] public int HDDSpace;
}
