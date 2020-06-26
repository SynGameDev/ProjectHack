using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShop : MonoBehaviour
{

    public static PlayerShop Instance;
    private PlayerStatus _Player;

    [Header("Level Text Objects")]
    [SerializeField] private TextMeshProUGUI _DownloadLevel;
    [SerializeField] private TextMeshProUGUI _ContractSpaceLevel;
    [SerializeField] private TextMeshProUGUI _ExpireTimerLevel;
    [SerializeField] private TextMeshProUGUI _CompleteTimerLevel;
    [SerializeField] private TextMeshProUGUI _BruteForceLevel;
    [SerializeField] private TextMeshProUGUI _SQLLevel;
    [SerializeField] private TextMeshProUGUI _PhishLevel;

    [Header("Purchase Amount Text Object")]
    [SerializeField] private TextMeshProUGUI _DownloadCost;
    [SerializeField] private TextMeshProUGUI _ContractSpaceCost;
    [SerializeField] private TextMeshProUGUI _ExpireTimerCost;
    [SerializeField] private TextMeshProUGUI _CompleteTimerCost;
    [SerializeField] private TextMeshProUGUI _BruteForceCost;
    [SerializeField] private TextMeshProUGUI _SQLCost;
    [SerializeField] private TextMeshProUGUI _PhishCost;

    [Header("Item Cost")]           // TODO: #1wwy2r
    [SerializeField] private int _DownloadUpgradeCost;
    [SerializeField] private int _ContractSpaceUpgradeCost;
    [SerializeField] private int _ExpireTimerUpgradeCost;
    [SerializeField] private int _CompleteTimerUpgradeCost;
    [SerializeField] private int _BruteForceUpgradeCost;
    [SerializeField] private int _SQLUpgradeCost;
    [SerializeField] private int _PhishUpgradeCost;

    [Header("Modifier Cost")]
    [SerializeField, Range(1.5f, 3)] private float _UpgradeModifer;

    [Header("Error Panel")]
    [SerializeField] private GameObject _ErrorPanel;
    [SerializeField] private GameObject _ErrorWaitTime;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        _Player = GameController.Instance.GetPlayerData();
    }

    private void Update() {
        CreateTextData();
        SetItemCost();
    }

    private void CreateTextData() {
        _DownloadLevel.text = GetDownloadLevel().ToString();
        _ContractSpaceLevel.text = GetContractSpactes().ToString();
        _ExpireTimerLevel.text = GetExpireLevel().ToString();
        _CompleteTimerLevel.text = GetCompleteLevel().ToString();
        _BruteForceLevel.text = GetBruteForceLevel().ToString();
        _SQLLevel.text = GetSQLLevel().ToString();
        _PhishLevel.text = GetPhish().ToString();
    }

    private void SetItemCost() {
        _DownloadCost.text = _DownloadUpgradeCost.ToString();
        _ContractSpaceCost.text = _ContractSpaceUpgradeCost.ToString();
        _ExpireTimerCost.text = _ExpireTimerUpgradeCost.ToString();
        _CompleteTimerCost.text = _CompleteTimerUpgradeCost.ToString();
        _BruteForceCost.text = _BruteForceUpgradeCost.ToString();
        _SQLCost.text = _SQLUpgradeCost.ToString();
        _PhishCost.text = _PhishCost.ToString();
    }

    // Item Cost Mod

    public void UpgradeDownloadLevel() {
        if(CanBuyItem(_DownloadUpgradeCost)) {
            _Player.DownloadLevel+= 1;
            _DownloadUpgradeCost = ApplyIncrease(_DownloadUpgradeCost);
        }
    }

    public void UpgradeContractSpace() {
        if(CanBuyItem(_ContractSpaceUpgradeCost)) {
            _Player.ContractSpaces += 5;
            _ContractSpaceUpgradeCost = ApplyIncrease(_ContractSpaceUpgradeCost);
        }
    }

    public void UpgradeExpireLevel() {
        if(CanBuyItem(_ExpireTimerUpgradeCost)) {
            _Player.ExpireTimerLevel += 1;
            _ExpireTimerUpgradeCost = ApplyIncrease(_ExpireTimerUpgradeCost);
        }
    }

    public void UpgradeCompleteLevel() {
        if(CanBuyItem(_CompleteTimerUpgradeCost)) {
            _Player.CompleteTimerLevel += 1;
            _CompleteTimerUpgradeCost = ApplyIncrease(_CompleteTimerUpgradeCost);
        }
    }

    public void UpgradeBruteForce() {
        if(CanBuyItem(_BruteForceUpgradeCost)) {
            _Player.BruteForceLevel += 1;
            _BruteForceUpgradeCost = ApplyIncrease(_BruteForceUpgradeCost);
        }
    }

    public void UpgradeSQL() {
        if(CanBuyItem(_SQLUpgradeCost)) {
            _Player.SQLLevel += 1;
            _SQLUpgradeCost = ApplyIncrease(_SQLUpgradeCost);
        }
    }

    public void UpgradePhish() {
        if(CanBuyItem(_PhishUpgradeCost)) {
            _Player.PhishLevel += 1;
            _PhishUpgradeCost = ApplyIncrease(_PhishUpgradeCost);
        }
    }

    private bool CanBuyItem(int amount) {
        if(amount < _Player.coin) {
            _Player.coin -= amount;
            return true;
        }

        return false;
    }

    private void DisplayPurchaseError() {
        _ErrorPanel.SetActive(true);
    }

    private IEnumarator HidePurchaseError() {
        yield return new WaitForSeconds(_ErrorWaitTime);
        _ErrorPanel.SetActive(false);
    }
    public int ApplyIncrease(int ToUpgrade) {
        return Mathf.RoundToInt(ToUpgrade * _UpgradeModifer);
    }

    public void ClosePlayerShop() {
        SceneController.Instance.ClosePlayerShop();
    }


    // Get the item cost
    public int GetDownloadLevel() => _Player.DownloadLevel;
    public int GetContractSpactes() => _Player.ContractSpaces;
    public int GetExpireLevel() => _Player.ExpireTimerLevel;
    public int GetCompleteLevel() => _Player.CompleteTimerLevel;
    public int GetBruteForceLevel() => _Player.BruteForceLevel;
    public int GetSQLLevel() => _Player.SQLLevel;
    public int GetPhish() => _Player.PhishLevel;
}
