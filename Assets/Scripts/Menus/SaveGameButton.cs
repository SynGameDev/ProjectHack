using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameButton : MonoBehaviour
{
    public void SaveGame()
    {
        SaveGameSystem.Instance.SaveGame(GameController.Instance.GetPlayerData().PlayerName);
    }
}
