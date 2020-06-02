using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenConnectToTerminal : MonoBehaviour
{
    public void Open() => SceneController.Instance.OpenConnectToUser();
}
