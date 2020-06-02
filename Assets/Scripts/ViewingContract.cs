﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewingContract : MonoBehaviour
{
    [Header("Text Details")]
    [SerializeField] private TextMeshProUGUI _ToHeader;
    [SerializeField] private TextMeshProUGUI _Subject;
    [SerializeField] private TextMeshProUGUI _MessageContent;

    private void Update() {
        CheckIfViewing();
    }

    private void CheckIfViewing() {
        if(GameController.Instance.GetViewingContract() != null) {
            DisplayContractInfo();
        } else {
            _ToHeader.text = "";
            _Subject.text = "";
            _MessageContent.text = "";
        }
    }

    private void DisplayContractInfo() {
        var contract = GameController.Instance.GetViewingContract();

        _ToHeader.text = contract.ContractOwner + " | " + "hacker@acexmail.com";
        _Subject.text = contract.ContractStatus;
        _MessageContent.text = contract.ContractMessage;
    }
}
