using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="Cracked App", menuName="Scriptable Objects/Applications/New Cracked Application", order=2)]
public class CrackedAppScriptableObject : ScriptableObject
{
    public string ApplicationName;
    public Sprite ApplicationIcon;
    public string ApplicationURL;
    public int HHD;
}
