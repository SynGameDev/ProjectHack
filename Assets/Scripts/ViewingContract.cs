using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewingContract : MonoBehaviour
{
    [Header("Text Details")]
    [SerializeField] private TextMeshProUGUI _ToHeader;
    [SerializeField] private TextMeshProUGUI _Subject;
    [SerializeField] private TextMeshProUGUI _MessageContent;
    [SerializeField] private GameObject _ContractActionPanel;

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
            _ContractActionPanel.SetActive(false);
        }
    }

    private void DisplayContractInfo() {
        var contract = GameController.Instance.GetViewingContract();

        _ToHeader.text = contract.ContractOwner + " | " + "hacker@acexmail.com";
        _Subject.text = contract.ContractStatus;
        _MessageContent.text = contract.ContractMessage;
        _ContractActionPanel.SetActive(true);
    }

    public void AcceptContract() {
        GameController.Instance.AcceptContract();
    }

    public void DeclineContract() => GameController.Instance.DeclineContract();
}
