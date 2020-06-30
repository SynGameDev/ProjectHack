using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatInfo : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var data = this.GetComponent<TextMeshProUGUI>();
        var coin = GameController.Instance.GetPlayerData().coin;
        var rep = GameController.Instance.GetPlayerData().RankedPoints;

        data.text = "$" + coin.ToString() + " | " + "REP " + rep;
    }
}
