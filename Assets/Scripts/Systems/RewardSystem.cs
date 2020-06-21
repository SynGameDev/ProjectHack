using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem : MonoBehaviour
{
    public static RewardSystem Instance;

    [Header("Ranked Points Reward")]
    [SerializeField] private int _EasyRankedPoints;
    [SerializeField] private int _MedRankedPoints;
    [SerializeField] private int _HardRankedPoints;

    [Header("Cash Earned Reward")]
    [SerializeField] private int _EasyCashEarned;
    [SerializeField] private int _MedCashEarned;
    [SerializeField] private int _HardCashEarned;

    [Header("Ranked Penalties")]
    [SerializeField] private int _UnhidenFileRankPen;
    [SerializeField] private int _ExpiredContractPen;
    [SerializeField] private int _FailedContractPen;

    [Header("Cash Penalties")]
    [SerializeField] private int _UnhiddenFileCashPen;

    private int CashEarnedToday;
    private int RankedPointsEarnedToday;

    private void Awake() {
        CashEarnedToday = 0;
        RankedPointsEarnedToday = 0;

        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    public int[] GetResults() {
        var player = GameController.Instance.GetPlayerData();

        foreach(var contract in player.ContractsCompletedToday) {
            if(contract.ContractStatus == "Success") {
                switch(contract.ContractDifficulty) {
                    case 0:
                        CashEarnedToday += _EasyCashEarned;
                        RankedPointsEarnedToday += _EasyRankedPoints;
                        break;
                    case 1:
                        CashEarnedToday += _MedCashEarned;
                        RankedPointsEarnedToday += _MedRankedPoints;
                        break;
                    case 2:
                        CashEarnedToday += _HardCashEarned;
                        RankedPointsEarnedToday += _HardRankedPoints;
                        break;
                }
            } else if(contract.ContractStatus == "Expired") {
                RankedPointsEarnedToday -= _ExpiredContractPen;
            } else if(contract.ContractStatus == "Failed") {
                RankedPointsEarnedToday -= _FailedContractPen;
            }

            foreach(var obj in contract.Objective) {
                if(!contract.MainObjectives.Contains(obj)) {
                    if(!contract.ActionLog.Contains(obj)) {
                        DealPen(obj);
                    }
                }
            }
        }

        int[] data = {CashEarnedToday, RankedPointsEarnedToday};    
        return data;    
    }

    private void DealPen(string obj) {
        var objective = obj.Split(' ');
        switch(objective[0]) {
            case "Hide":
                CashEarnedToday -= _UnhiddenFileCashPen;
                RankedPointsEarnedToday -= _UnhidenFileRankPen;
                break;
        }
    }
}
