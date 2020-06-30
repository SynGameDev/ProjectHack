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


    [Header("Modifier Cost")]
    [SerializeField, Range(1.5f, 3)] private float _UpgradeModifer;

    [Header("Error Panel")]
    [SerializeField] private GameObject _ErrorPanel;
    [SerializeField] private float _ErrorWaitTime;

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
        _DownloadCost.text = _Player.DownloadCost.ToString();
        _ContractSpaceCost.text = _Player.SpaceCost.ToString();
        _ExpireTimerCost.text = _Player.ExpireCost.ToString();
        _CompleteTimerCost.text = _Player.CompleteCost.ToString();
        _BruteForceCost.text = _Player.BruteForceCost.ToString();
        _SQLCost.text = _Player.SQLCost.ToString();
        _PhishCost.text = _Player.PhishCost.ToString();
    }

    // Item Cost Mod

    public void UpgradeDownloadLevel() {
        if(CanBuyItem(_Player.DownloadCost)) {
            _Player.DownloadLevel+= 1;
            _Player.DownloadCost = ApplyIncrease(_Player.DownloadCost);
        }
        SetItemCost();
        CreateTextData();
    }

    public void UpgradeContractSpace() {
        if(CanBuyItem(_Player.SpaceCost)) {
            _Player.ContractSpaces += 5;
            _Player.SpaceCost = ApplyIncrease(_Player.SpaceCost);
        }
        SetItemCost();
        CreateTextData();

    }

    public void UpgradeExpireLevel() {
        if(CanBuyItem(_Player.ExpireCost)) {
            _Player.ExpireTimerLevel += 1;
            _Player.ExpireCost = ApplyIncrease(_Player.ExpireCost);
        }
        SetItemCost();
        CreateTextData();
    }

    public void UpgradeCompleteLevel() {
        if(CanBuyItem(_Player.CompleteCost)) {
            _Player.CompleteTimerLevel += 1;
            _Player.CompleteCost = ApplyIncrease(_Player.CompleteCost);
        }
        SetItemCost();
        CreateTextData();
    }

    public void UpgradeBruteForce() {
        if(CanBuyItem(_Player.BruteForceCost)) {
            _Player.BruteForceLevel += 1;
            _Player.BruteForceCost = ApplyIncrease(_Player.BruteForceCost);
        }
        SetItemCost();
        CreateTextData();
    }

    public void UpgradeSQL() {
        if(CanBuyItem(_Player.SQLCost)) {
            _Player.SQLLevel += 1;
            _Player.SQLCost = ApplyIncrease(_Player.SQLCost);
        }
        SetItemCost();
        CreateTextData();
    }

    public void UpgradePhish() {
        if(CanBuyItem(_Player.PhishCost)) {
            _Player.PhishLevel += 1;
            _Player.PhishCost = ApplyIncrease(_Player.PhishCost);
        }
        SetItemCost();
        CreateTextData();
    }

    private bool CanBuyItem(int amount) {
        if(amount < _Player.coin) {
            _Player.coin -= amount;
            return true;
        }

        StartCoroutine(PurchaseError());

        return false;
    }

    private IEnumerator PurchaseError() {
        _ErrorPanel.SetActive(true);
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
