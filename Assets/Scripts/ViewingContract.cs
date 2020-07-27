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
    [SerializeField] private bool _ViewingContract;
    

    private void Update() {
        CheckIfViewing();

        if (GameController.Instance.GetViewingContract().ContractName == "Null")
        {
            _ToHeader.text = "";
            _Subject.text = "";
            _MessageContent.text = "";
            _ContractActionPanel.SetActive(false);
        }
        else
        {
            DisplayContractInfo();
        }
    }

    private void CheckIfViewing() {
        // If the Viewing Contract is null that don't display anything otherwise so the contract info
        if(GameController.Instance.GetViewingContract() == null)
        {
            _ToHeader.text = "";
            _Subject.text = "";
            _MessageContent.text = "";
            _ContractActionPanel.SetActive(false);
        } else {
           // DisplayContractInfo();
           
           
        }
    }

    private void DisplayContractInfo() {
        var contract = GameController.Instance.GetViewingContract();

        _ToHeader.text = contract.ContractOwner.Username;
        _Subject.text = contract.ContractSubject;
        _MessageContent.text = contract.ContractMessage;

        _ContractActionPanel.SetActive(contract.ContractStatus == "Pending");
    }

    public void AcceptContract() {
        GameController.Instance.AcceptContract();
    }

    public void DeclineContract() => GameController.Instance.DeclineContract();

    public void SetViewingContract(bool viewing) => _ViewingContract = viewing;
}
