using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [Header("Player Settings")]
    private TextMeshProUGUI _PlayerStatText;
    private PlayerStatus Player;



    [Header("Currently Active Contract")]
    public ContractInfo ActiveContract;                   // Current contract that has been accepted;
    [SerializeField] private TextMeshProUGUI _ActiveContractMessage;                    // Text Information
    [SerializeField] private GameObject CompleteContractButton;
    private GameObject ActiveContractButton;


    [Header("Available Contracts")]
    [SerializeField] private GameObject _ContractButtonPrefab;
    [SerializeField] private Transform _ContractContainer;
    private List<ContractInfo> _AvailableContracts = new List<ContractInfo>();

    [Header("Viewing Contract")]
    [SerializeField] private ContractInfo _ViewingContract;
    private GameObject _ViewingContractButton;
    private TextFile _OpenFile;

    [Header("Terminals")]
    private List<TerminalInfo> _TerminalList = new List<TerminalInfo>();
    private TerminalInfo _CurrentlyConnectedToTerminal;

    [Header("Dropdown Menu Settings")]
    private GameObject _DropdownMenuItem;

    [Header("Objective Display")]
    [SerializeField] private GameObject _ObjectivePrefab;
    [SerializeField] private Transform _ObjectiveContainer_1;
    [SerializeField] private Transform _ObjectiveContainer_2;
    private int _RowOneCount;
    private int _RowTwoCount;
    private List<GameObject> ObjectiveObject = new List<GameObject>();

    
    

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
        //ActiveContractDisplay();
    }

    private void DisplayAvailableContracts() {
        foreach(var contract in _AvailableContracts) {
            var go = Instantiate(_ContractButtonPrefab);                        // Create the button
            go.name = "Contract: " + contract.ContractID.ToString();            // Name the button

            go.GetComponentInChildren<TextMeshProUGUI>().text = "Contract: " + contract.ContractID.ToString();
            go.GetComponent<ContractButton>().SetContract(contract);
            go.GetComponent<ContractTimerController>().SetContract(contract);

            go.transform.SetParent(_ContractContainer);
            go.transform.localScale = Vector3.one;

            contract.ContractButton = go;

        }
    }

    private void ActiveContractDisplay() {
        if(ActiveContract != null) {
            foreach(var obj in ActiveContract.Objective) {
                var go = Instantiate(_ObjectivePrefab);
                go.GetComponent<TextMeshProUGUI>().text = obj;

                // Set the transform
                if(_RowOneCount > 5) {
                    go.transform.SetParent(_ObjectiveContainer_2);
                    _RowTwoCount += 1;
                } else {
                    go.transform.SetParent(_ObjectiveContainer_1);
                    _RowOneCount += 1;
                }

                go.transform.localScale = Vector3.one;
                ObjectiveObject.Add(go);
                CompleteContractButton.SetActive(true);
            }
        } else {
            foreach(var go in ObjectiveObject) {
                Destroy(go);
            }
            CompleteContractButton.SetActive(false);
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

    public void ExpiredContract(ContractInfo contract) {
        _AvailableContracts.Remove(contract);
        Destroy(contract.ContractButton);
        contract.ContractStatus = "Expired";
        Player.ContractsCompletedToday.Add(contract);
        ActiveContractDisplay();

        // TODO: Update Stats
    }

    public void AcceptContract() {
        ActiveContract = _ViewingContract;
        _ViewingContractButton.GetComponent<ContractTimerController>().SetStatus(true);
        ActiveContract.ContractButton.name = ActiveContract.ContractButton.name + " (Accepted)";
        ActiveContract.ContractButton.GetComponentInChildren<TextMeshProUGUI>().text = ActiveContract.ContractButton.name;
        ActiveContract.ContractButton.GetComponentInChildren<Image>().fillAmount = 1;
        _ViewingContractButton = null;
        _ViewingContract = null;
        ActiveContract.ContractStatus = "Accepted";

        ActiveContractDisplay();
    }

    public void CompleteContract() {
        Destroy(ActiveContract.ContractButton);
        var ObjectiveToComplete = ActiveContract.Objective.Count;
        var ObjectivesCompleted = 0;
        foreach(var action in ActiveContract.ActionLog) {
            if(ActiveContract.Objective.Contains(action)) {
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

        Player.ContractsCompletedToday.Add(ActiveContract);

        ActiveContract = null;
        ActiveContractDisplay();
    }

    // Getters 
    public ContractInfo GetActiveContract() => ActiveContract;
    public ContractInfo GetViewingContract() => _ViewingContract;
    public PlayerStatus GetPlayerData() => Player;
    public List<ContractInfo> GetAvailableContracts() => _AvailableContracts;
    public TextFile GetOpenTextFile() => _OpenFile;
    public List<TerminalInfo> GetAllTerminals() => _TerminalList;
    public TerminalInfo GetActiveTerminal() => _CurrentlyConnectedToTerminal;
    public GameObject GetActiveDropdownItem() => _DropdownMenuItem;

    // Setters
    public void SetViewingButton(GameObject contract) {
        _ViewingContractButton = contract;
    }

    public void AddContract(ContractInfo contract) => _AvailableContracts.Add(contract);
    public void SetActiveContract(ContractInfo contract) => ActiveContract = contract;
    public void SetOpenTextFile(TextFile file) => _OpenFile = file;
    public void SetActiveTerminal(TerminalInfo terminal) => _CurrentlyConnectedToTerminal = terminal;
    public void SetDropdownItem(GameObject DropdownItem) => _DropdownMenuItem = DropdownItem;

    // Save System
    public void LoadPlayer(PlayerStatus NewPlayer) {
        Player = NewPlayer;
    }




    // TEST
    public ContractInfo SetupTestContract() {
        var term = CreateTerminal();

        var info = new ContractInfo();
        info.ContractID = 1;
        info.ContractName = "Test Contract";
        info.ContractOwner = "Alex A";
        info.ContractStatus = "Pending";
        info.ContractMessage = "Hey Man,\n\nI Heard you're the man I need to speak to.\n\nMy business partner is trying to screw me out of the business and I need to access his PC, can you install a key logger!\n\nCheers David";
        info.ContractSubject = "Install Software";  

        info.Terminal = term;
        _TerminalList.Add(term);
        

        info.Objective.Add("Install AceXTerminal IP: 192.111.111");
        info.Objective.Add("Install DirtyRat KeyLogger IP: 192.111.111");
        info.Objective.Add("Hide DirtyRat KeyLogger IP: 192.111.111");
        info.Objective.Add("Uninstall AcexTerminal IP: 192.111.111");
        

        return info;
    }

    private TerminalInfo CreateTerminal() {
        var terminal = new TerminalInfo();

        terminal.TerminalType = "Desktop";
        terminal.TerminalIP = "192.111.111";
        terminal.InstalledApplication.Add("App_1");
        terminal.TextFileList.Add(TestTextFile());

        terminal.BackDoorInstalled = false;

        return terminal;
    }

    private PlayerStatus CreatePlayer() {
        var player = new PlayerStatus();
        
        player.PlayerName = "DirtyRat";
        player.PlayerIP = "48.111.321";
        return player;
    }

    private TextFile TestTextFile() {
        TextFile text = new TextFile();

        text.FileName = "Test Text File";
        text.FileContent ="This is a test of the content";

        return text;
    }

    

}
