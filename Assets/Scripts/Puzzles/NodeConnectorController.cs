using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeConnectorController : MonoBehaviour {
    [SerializeField] private bool _IsConnected;             // If the node is connected or not
    [SerializeField] List<NodePuzzlePiece> LinkedNodes = new List<NodePuzzlePiece>();           // List of all the nodes linked to it
    

    private void Update() {
        CheckIfConnected();
    }

    private void CheckIfConnected() {
        var TotalNodes = LinkedNodes.Count;             // Total nodes that needs to be connected
        var ConnectedNodes = 0;                         // Total node pieces that are connected
        // Loop through each node & Check if it's connected
        foreach(var NodePiece in LinkedNodes) {
            if(NodePiece.IsNodeConnected()) {
                ConnectedNodes += 1;                // ... If it's connected then increase the account
            }
        }

        if(ConnectedNodes == TotalNodes) // If all the nodes are connected Activate the node or DEACTIVATE THE NODE
            ActivateNode();
        else 
            DeactivateNode();
    }

    private void ActivateNode() {
        this.gameObject.GetComponent<Image>().color = Color.green;          // Change the color to green
        _IsConnected = true;                // Set node as connected
    }

    private void DeactivateNode() {
        this.gameObject.GetComponent<Image>().color = Color.red;                // Set color to red 
        _IsConnected = false;               // Set node as not connected
    }

    // Getter
    public bool IsConnected() => _IsConnected;
}