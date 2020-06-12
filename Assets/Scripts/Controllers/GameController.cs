using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("Player Settings")]
    private PlayerStatus Player;

    [Header("Currently Active Contract")]
    public ContractInfo ActiveContract;                   // Current contract that has been accepted;
    [SerializeField] private TextMeshProUGUI _ActiveContractMessage;                    // Text Information
    private GameObject ActiveContractButton;

    [Header("Available Contracts")]
    [SerializeField] private GameObject _ContractButtonPrefab;
    [SerializeField] private Transform _ContractContainer;
    private List<ContractInfo> _AvailableContracts = new List<ContractInfo>();

    [Header("Viewing Contract")]
    [SerializeField] private ContractInfo _ViewingContract;
    private GameObject _ViewingContractButton;
    private TextFile _OpenFile;
    
    

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
        _AvailableContracts.Add(SetupTestContract());             // TEST METHOD
        Player = CreatePlayer();                                // TEST METHOD
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

            contract.ContractButton = go;

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
        ActiveContract.ContractStatus = "Accepted";
    }

    public void CompleteContract() {
        Destroy(ActiveContract.ContractButton);
        var ObjectiveToComplete = ActiveContract.Terminal.Objective.Count;
        var ObjectivesCompleted = 0;
        foreach(var action in ActiveContract.Terminal.ActionLog) {
            if(ActiveContract.Terminal.Objective.Contains(action)) {
                ObjectivesCompleted += 1;
            }
        }


        if(ObjectivesCompleted >= ObjectiveToComplete) {
            ActiveContract.ContractStatus = "Success";
            SceneController.Instance.OpenContractCompletedSuccessPopup();
        } else {
            ActiveContract.ContractStatus = "Failed";
            SceneController.Instance.OpenContractCompletedFailedPopup();
        }

        ActiveContract = null;
    }

    // Getters 
    public ContractInfo GetActiveContract() => ActiveContract;
    public ContractInfo GetViewingContract() => _ViewingContract;
    public PlayerStatus GetPlayerData() => Player;
    public List<ContractInfo> GetAvailableContracts() => _AvailableContracts;
    public TextFile GetOpenTextFile() => _OpenFile;

    // Setters
    public void SetViewingButton(GameObject contract) {
        _ViewingContractButton = contract;
    }

    public void AddContract(ContractInfo contract) => _AvailableContracts.Add(contract);
    public void SetActiveContract(ContractInfo contract) => ActiveContract = contract;
    public void SetOpenTextFile(TextFile file) => _OpenFile = file;

    // Save System
    public void LoadPlayer(PlayerStatus NewPlayer) {
        Player = NewPlayer;
    }




    // TEST
    public ContractInfo SetupTestContract() {
        var info = new ContractInfo();
        info.ContractID = 1;
        info.ContractName = "Test Contract";
        info.ContractOwner = "Alex A";
        info.ContractStatus = "Pending";
        info.ContractMessage = "Installed Data on my friends computer to spy on them";
        info.ContractSubject = "Install Software";  

        info.Terminal.TerminalType = "Desktop";
        info.Terminal.TerminalIP = "192.111.111";

        info.Terminal.InstalledApplication.Add("App_1");

        info.Terminal.Objective.Add("Install AceXTerminal");
        info.Terminal.Objective.Add("Install DirtyRat KeyLogger");
        info.Terminal.Objective.Add("Hide Application");
        info.Terminal.Objective.Add("Uninstall AcexTerminal");
        

        return info;
    }

    private PlayerStatus CreatePlayer() {
        var player = new PlayerStatus();
        
        player.PlayerName = "DirtyRat";
        return player;
    }

    

}
