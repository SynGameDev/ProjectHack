using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeItemButton : MonoBehaviour, IPointerClickHandler {
    public string ItemName;

    public void OnPointerClick(PointerEventData pointerEventData) {
        Upgrade();
    }

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