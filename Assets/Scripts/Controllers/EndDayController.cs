using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndDayController : MonoBehaviour
{
    [Header("Field Text Objects")]
    [SerializeField] private TextMeshProUGUI _DayText;
    [SerializeField] private TextMeshProUGUI _ContractsOfferedText;
    [SerializeField] private TextMeshProUGUI _ContractsCompletedSuccessful;
    [SerializeField] private TextMeshProUGUI _ContractsFailed;
    [SerializeField] private TextMeshProUGUI _RankedPointsEarned;
    [SerializeField] private TextMeshProUGUI _EarnedCash;

    private void Awake() {

    }

    private void Start() {
        // TODO: Implement Auto Save If triggered
        Time.timeScale = 0;
        var player = GameController.Instance.GetPlayerData();               // Get the player
        player.PlayerDay += 1;              // Increarse the day

        _DayText.text = "DAY: " + player.PlayerDay;              // Set the text
        
        // Integer Value of the contracts
        var offered_count = 0;
        var success_count = 0;
        var failed_count = 0;

        // Loop through each contract and increase the value
        foreach(var contract in player.ContractsCompletedToday) {
            offered_count += 1;
            if(contract.ContractStatus == "Completed") {
                success_count += 1;
            } else {
                failed_count += 1;
            }
        }

        // Set the stat text
        _ContractsOfferedText.text = "CONTRACTS OFFERED: " + offered_count;
        _ContractsCompletedSuccessful.text = "CONTRACTS SUCCESSFUL: " + success_count;
        _ContractsFailed.text = "CONTRACTS FAILED: " + failed_count;

        // Show Reward Data
        _RankedPointsEarned.text = "EARNED POINTS: " + RewardSystem.Instance.GetResults()[1];
        _EarnedCash.text = "EARNED CASH: " + RewardSystem.Instance.GetResults()[0];
    }


    public void ClosePanel() {
        Time.timeScale = 1;
        SceneController.Instance.CloseEndOfDayPopup();
    }
}
