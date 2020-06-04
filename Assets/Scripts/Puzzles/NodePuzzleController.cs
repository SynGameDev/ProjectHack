using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodePuzzlePiece : MonoBehaviour {
    
    [Header("Puzzle Stats")]
    [SerializeField] private int _TotalConnectors;
    private int _CurrentlyConnected;

    [Header("Puzzle Object")]   
    [SerializeField] private List<NodeConnectorController> AllNodes = new List<NodeConnectorController>();
    [SerializeField] private GameObject PuzzleStatusText;


    private void Awake() {
        PuzzleStatusText.SetActive(false);
    }



    private void Update() {
        CheckIfCompleted();
    }
    private void CheckIfCompleted() {
        var TotalNodes = AllNodes.Count;
        var Connected = 0;
        foreach(var node in AllNodes) {
            if(node.IsConnected) {
                Connected += 1;
            }
        }

        if(Connected == TotalNodes) {
            PuzzleCompleted();
        }
    }

    private void PuzzleCompleted() {
        PuzzleStatusText.SetActive(true);
        StartCoroutine(ClosePuzzle());
    }

    private IEnumerator ClosePuzzle() {
        yield return new WaitForSeconds(3);
        SceneController.Instance.CloseTerminalConnector();
        SceneController.Instance.OpenUserDesktop();
    }

    public void ConnectNode() {
        _CurrentlyConnected += 1;
    }

    public void DisconnectNode() {
        _CurrentlyConnected -= 1;
    }
}