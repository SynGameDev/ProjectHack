using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItemButton : MonoBehaviour {
    public string ItemName;

    public void Upgrade() {
        switch(ItemName) {
            case "Download":
                PlayerShop.Instance.UpgradeDownloadLevel();
                break;
            case "Spaces":
                PlayerShop.Instance.UpgradeContractSpace();
                break;
            case "Expire":
                PlayerShop.Instance.UpgradeExpireLevel();
                break;
            case "Complete":
                PlayerShop.Instance.UpgradeCompleteLevel();
                break;
            case "Brute":
                PlayerShop.Instance.UpgradeBruteForce();
                break;
            case "SQL":
                PlayerShop.Instance.UpgradeSQL();
                break;
            case "Phish":
                PlayerShop.Instance.UpgradePhish();
                break;
        }
    }
}