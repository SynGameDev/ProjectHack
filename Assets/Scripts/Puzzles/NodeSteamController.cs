using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSteamController : MonoBehaviour
{
    [SerializeField] private string _CurrentPosition;
    [SerializeField] private PuzzlePieceController _PuzzlePiece;
    
    public void SwitchPos() {
        switch(_CurrentPosition) {
            case "Left":
                _CurrentPosition = "Top";
                transform.localPosition = _PuzzlePiece.GetTopPos();
                transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90);
                break;
            case "Top":
                _CurrentPosition = "Right";
                transform.localPosition = _PuzzlePiece.GetRightPos();
                transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
                break;
            case "Right":
                _CurrentPosition = "Bottom";
                transform.localPosition = _PuzzlePiece.GetBottomPos();
                transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90);
                break;
            case "Bottom":
                _CurrentPosition = "Left";
                transform.localPosition = _PuzzlePiece.GetLeftPos();
                transform.localRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
                break;
        }
    }

    public string GetCurrentPos() => _CurrentPosition;
}
