using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="Application", menuName="Scriptable Objects/Applications/New Application", order = 1)]
public class ApplicationScriptableObject : ScriptableObject
{
    public string ApplicationName;
    [TextArea]
    public string ApplicationDescription;
    public List<Sprite> ApplicationScreenshots = new List<Sprite>();

    public Sprite ApplicationIcon;

    [Tooltip("Amount to obtain the application")] public int PurchaseAmount;
    [Tooltip("Space the application will take on the HDD")] public int HDDSpace;
}
