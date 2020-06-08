using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="Application", menuName="Scriptable Objects/Applications/New Application", order = 1)]
[PreferBinarySerialization]
public class ApplicationScriptableObject : ScriptableObject
{
    public ApplicationClass AppData = new ApplicationClass();
}
