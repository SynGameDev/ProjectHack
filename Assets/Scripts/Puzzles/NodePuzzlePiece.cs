using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePuzzlePiece : MonoBehaviour {
    
    [SerializeField] private bool _IsConnected = false;                 // If the node has been connected or not

    public void RotatePiece() {
        var CurrentRotation = transform.rotation;                   // Get the current rotation
        float y = CurrentRotation.y += 90;

        Vector3 NewRotPos = new Vector3(CurrentRotation.x, y, CurrentRotation.z);                // Increaase the Y rotation

        transform.rotation = Quaternion.Euler(NewRotPos);                // Set the new rotation
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("NodeCollider")) {              // If the node collides then it is connected
            _IsConnected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {            // if the node is not colliding anymore than it's not connected
        if(other.CompareTag("NodeCollider")) {
            _IsConnected = false;
        }
    }

    // Getters
    public bool IsNodeConnected() => _IsConnected;


}