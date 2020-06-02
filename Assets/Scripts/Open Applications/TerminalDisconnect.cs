using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalDisconnect : MonoBehaviour
{
    public void Disconnect() {
        SceneController.Instance.CloseUserDesktop();
    }
}
