using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShop : MonoBehaviour
{
    private PlayerStatus _Player;

    [Header("Text Objects")]
    [SerializeField] private TextMeshProUGUI _DownloadLevel;
    [SerializeField] private TextMeshProUGUI _ContractSpaceLevel;
    [SerializeField] private TextMeshProUGUI _ExpireTimerLevel;
    [SerializeField] private TextMeshProUGUI _CompleteTimerLevel;
    [SerializeField] private TextMeshProUGUI _BruteForceLevel;
    [SerializeField] private TextMeshProUGUI _SQLLevel;
    [SerializeField] private TextMeshProUGUI _PhishLevel;



    private void Awake() {

    }

    private void Start() {
        _Player = GameController.Instance.GetPlayerData();
    }

    public void UpgradeDownloadSpeed() {
        _Player.DownloadSpeedMutliplier += 0.3f;         // TODO: Create a method to increase the data.
    }

    public void UpgradeContractSpaces() {
        _Player.ContractSpace += 5;
    }

    public void UpgradeExpireTimer() {
        // TODO: Create a method to upgrade timer
    }

    public void UpgradeBruteForce() {
        // TODO
    }

    public void UpgradeSQL() {
        // TODO
    }

    public void UpgradePhish() {
        // TODO
    }
}
