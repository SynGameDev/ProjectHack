using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AccountantDetails : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private Transform _ContentSpawnPoint;
    [SerializeField] private GameObject _ContentData;

    private void Awake() {

    }

    private void Start() {
        GetDetails();
    }

    private void GetDetails() {
        var player = GameController.Instance.GetPlayerData();

        foreach(var day in player.EndOfDayStats) {
            var data = day.Date + " | " + day.CashEarned + " | " + day.RankedPoints + " | " + day.SuccessfulCompleted;
            var go = Instantiate(_ContentData);
            go.GetComponent<TextMeshProUGUI>().text = data;

            go.transform.localScale = Vector3.one;
            go.transform.SetParent(_ContentSpawnPoint);
        }
    }

    public void CloseApp() {
        SceneController.Instance.CloseAccountant();
    }

}
