using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("Currently Active Contract")]
    public ContractInfo ActiveContract;                   // Current contract that has been accepted;
    [SerializeField] private TextMeshProUGUI _ActiveContractMessage;                    // Text Information

    [Header("Available Contracts")]
    [SerializeField] private GameObject _ContractButtonPrefab;
    [SerializeField] private Transform _ContractContainer;
    private List<ContractInfo> _AvailableContracts = new List<ContractInfo>();

    [Header("Viewing Contract")]
    [SerializeField] private ContractInfo _ViewingContract;
    private GameObject _ViewingContractButton;
    
    

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }

        
    }

    private void Start() {
        DisplayAvailableContracts();
        _AvailableContracts.Add(SetupTestContract());             // TEST
        DisplayAvailableContracts();
    }

    private void Update() {
        ActiveContractDisplay();
    }

    private void DisplayAvailableContracts() {
        foreach(var contract in _AvailableContracts) {
            var go = Instantiate(_ContractButtonPrefab);                        // Create the button
            go.name = "Contract: " + contract.ContractID.ToString();            // Name the button

            go.GetComponentInChildren<TextMeshProUGUI>().text = "Contract: " + contract.ContractID.ToString();
            go.GetComponent<ContractButton>().SetContract(contract);

            go.transform.SetParent(_ContractContainer);
            go.transform.localScale = Vector3.one;

        }
    }

    private void ActiveContractDisplay() {
        if(ActiveContract != null) {
            _ActiveContractMessage.text = ActiveContract.ContractMessage;
        } else {
            _ActiveContractMessage.text = "";
        }
    }

    public void DisplayContract(ContractInfo Contract) {
        _ViewingContract = Contract;
    }



    public void DeclineContract() {
        _AvailableContracts.Remove(_ViewingContract);
        Destroy(_ViewingContractButton);
        _ViewingContract = null;
        
        // TODO:Update Stats
    }

    public void AcceptContract() {
        ActiveContract = _ViewingContract;
        _ViewingContract = null;
    }

    // Getters 
    public ContractInfo GetActiveContract() => ActiveContract;
    public ContractInfo GetViewingContract() => _ViewingContract;

    // Setters
    public void SetViewingButton(GameObject contract) {
        _ViewingContractButton = contract;
    }


    // TEST
    public ContractInfo SetupTestContract() {
        var info = new ContractInfo();
        info.ContractID = 1;
        info.ContractName = "Test Contract";
        info.ContractOwner = "Alex A";
        info.ContractStatus = "Accepted";
        info.ContractMessage = "Installed Data on my friends computer to spy on them";
        info.ContractSubject = "Install Software";  

        info.TerminalType = "Desktop";
        info.TerminalIP = "192.111.111";

        info.InstalledApplication.Add(DatabaseController.Instance.SoftwareCenter);
        

        return info;
    }

    

}
