using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ApplicationClass
{
    [Header("Application Deatils")]
    public string ApplicationID;
    public string ApplicationName;
    [TextArea]
    public string ApplicationDescription;
    [System.NonSerialized]
    public Sprite ApplicationIcon;

    [Header("Server Information")]
    public bool CrackedApplication;
    public string ApplicationDownloadURL;

    [Header("Store Information")]
    [Tooltip("Amount to obtain the application")] public int PurchaseAmount;
    [Tooltip("Space the application will take on the HDD")] public int HDDSpace;
}
