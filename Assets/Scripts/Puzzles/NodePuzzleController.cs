using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodePuzzleController : MonoBehaviour {
    
    [Header("Puzzle Stats")]
    [SerializeField] private int _TotalConnectors;
    private int _CurrentlyConnected;

    [Header("Puzzle Object")]   
    [SerializeField] private List<NodeConnectorController> AllNodes = new List<NodeConnectorController>();
    [SerializeField] private GameObject PuzzleStatusText;

    [Header("Timer Settings")]
    [SerializeField] private TextMeshProUGUI _TimerText;
    [SerializeField] private float _SecondsToCompleted;
    private float _CurrentTimer;



    private void Awake() {
        PuzzleStatusText.SetActive(false);
    }



    private void Update() {
        CheckIfCompleted();
        TimerControl();
    }

    private void TimerControl() {
        _SecondsToCompleted -= 1;
        if(_SecondsToCompleted <= 0) {
            PuzzleCompleted("Failed");
        }

        _TimerText.text = _SecondsToCompleted.ToString("F0");
    }

    private void CheckIfCompleted() {
        var TotalNodes = AllNodes.Count;
        var Connected = 0;
        foreach(var node in AllNodes) {
            if(node.IsConnected()) {
                Connected += 1;
            }
        }

        if(Connected == TotalNodes) {
            PuzzleCompleted("Success");
        }
    }

    private IEnumerator PuzzleCompleted(string status) {
        _TimerText.gameObject.SetActive(false);
        PuzzleStatusText.SetActive(true);
        PuzzleStatusText.GetComponent<TextMeshProUGUI>().text = status;

        yield return new WaitForSeconds(3);
        SceneController.Instance.CloseTerminalConnector();

        if(status == "Success")
            SceneController.Instance.OpenUserDesktop();
    }

    public void ConnectNode() {
        _CurrentlyConnected += 1;
    }

    public void DisconnectNode() {
        _CurrentlyConnected -= 1;
    }
}