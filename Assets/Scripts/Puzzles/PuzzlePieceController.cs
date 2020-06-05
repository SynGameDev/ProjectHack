using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePieceController : MonoBehaviour
{
    [Header("Piece Settings")]
    [SerializeField] private bool _StraightPiece;
    [SerializeField] private bool _AnglePiece;
    [SerializeField] private bool _IsConnected;
    [SerializeField] private bool _IsRootPiece;
    [SerializeField] private bool _IsEndPiece;


    [Header("Straight Piece Settings")]
    [SerializeField] private float _ConnectedRotation;
    [SerializeField] private float _CurrentZRotation;


    [Header("Angle Piece Settings")]
    [SerializeField] private Vector3 _LeftAnglePoint;
    [SerializeField] private Vector3 _RightAnglePoint;
    [SerializeField] private Vector3 _TopAnglePoint;
    [SerializeField] private Vector3 _BottomAnglePoint;
    [Space(10)]
    [SerializeField] private int _MultiPieceTotal;
    [SerializeField] private int _PiecesConnected;


    [Header("Angle Piece Objects")]
    [SerializeField] private NodeSteamController _Piece_1;
    [SerializeField] private NodeSteamController _Piece_2;
    [SerializeField] private NodeSteamController _Piece_3;
    [SerializeField] private NodeSteamController _Piece_4;
    private List<string> _CurrentPosition = new List<string>();

    [Header("Angle Images")]
    [SerializeField] private Image _ImagePiece_1;
    [SerializeField] private Image _ImagePiece_2;
    [SerializeField] private Image _ImagePiece_3;
    [SerializeField] private Image _ImagePiece_4;
    [SerializeField] private Image _ImageCentralNode;



    [Header("Piece To Be Connected Settings")]
    [SerializeField] private bool _LeftAnchorPoint;
    [SerializeField] private bool _RightAnchorPoint;
    [SerializeField] private bool _TopAnchorPoint;
    [SerializeField] private bool _BottomAnchorPoint;


    [Header("Connecting Piece")]
    [SerializeField] private PuzzlePieceController _ConnectingPiece;


    private void Update() {
        CheckPosition();

        SetPieceColor();
    }

    private bool CheckIfConnected() {
        return false;
    }

    public List<string> PiecesToConnect() {
        List<string> data = new List<string>();

        if(_LeftAnchorPoint)
            data.Add("Left");

        if(_RightAnchorPoint)
            data.Add("Right");

        if(_TopAnchorPoint)
            data.Add("Top");

        if(_BottomAnchorPoint)
            data.Add("Bottom");   

        return data;     
            
    }

    public void RotateSinglePiece() {
        if(_StraightPiece) {
            if(_CurrentZRotation == 0) {
                _CurrentZRotation = 90;
            } else {
                _CurrentZRotation = 0;
            }

            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _CurrentZRotation);
        }
    }

    public void RotateMultiPiece() {
        switch(_MultiPieceTotal) {
            case 1:
                _Piece_1.SwitchPos();
                break;
            case 2:
                _Piece_1.SwitchPos();
                _Piece_2.SwitchPos();
                break;
            case 3:
                _Piece_1.SwitchPos();
                _Piece_2.SwitchPos();
                _Piece_3.SwitchPos();
                break;
            case 4:
                _Piece_1.SwitchPos();
                _Piece_2.SwitchPos();
                _Piece_3.SwitchPos();
                _Piece_4.SwitchPos();
                break;
            default:
                _Piece_1.SwitchPos();
                break;
        }
    }

    public void CheckPosition() {
        if(_AnglePiece) {
            if(_ConnectingPiece.PieceIsConnected()) {
                if(_MultiPieceTotal == 1) {
                    if(_ConnectingPiece.PieceIsConnected()) {
                        _IsConnected = true;
                    }
                }

                if(_MultiPieceTotal == 2) {
                    if(_LeftAnchorPoint && _TopAnchorPoint) {
                        if(_Piece_1.GetCurrentPos() == "Left" && _Piece_2.GetCurrentPos() == "Top") {
                            _IsConnected = true;
                        } else if(_Piece_1.GetCurrentPos() == "Top" && _Piece_2.GetCurrentPos() == "Left") {
                            _IsConnected = true;
                        } else {
                            _IsConnected = false;
                        }
                    } else if(_TopAnchorPoint && _RightAnchorPoint) {
                        if(_Piece_1.GetCurrentPos() == "Top" && _Piece_2.GetCurrentPos() == "Right" || _Piece_1.GetCurrentPos() == "Right" && _Piece_2.GetCurrentPos() == "Top") 
                            _IsConnected = true;
                        else 
                            _IsConnected = false;
                    } else if(_RightAnchorPoint && _BottomAnchorPoint) {
                        if(_Piece_1.GetCurrentPos() == "Right" && _Piece_2.GetCurrentPos() == "Bottom" || _Piece_1.GetCurrentPos() == "Bottom" && _Piece_2.GetCurrentPos() == "Right") 
                            _IsConnected = true;
                        else 
                            _IsConnected = false;
                    } else if(_LeftAnchorPoint && _BottomAnchorPoint) {
                        if(_Piece_1.GetCurrentPos() == "Left" && _Piece_2.GetCurrentPos() == "Bottom" || _Piece_1.GetCurrentPos() == "Bottom" && _Piece_2.GetCurrentPos() == "Left")
                            _IsConnected = true;
                        else 
                            _IsConnected = false;
                    }
                }

                if(_MultiPieceTotal == 3) {
                    if(_LeftAnchorPoint && _TopAnchorPoint && _RightAnchorPoint) {
                        // TODO
                    } else if(_TopAnchorPoint && _RightAnchorPoint && _BottomAnchorPoint) {
                        // TODO
                    } else if(_RightAnchorPoint && _BottomAnchorPoint && _LeftAnchorPoint) {
                        // TODO
                    } else if(_BottomAnchorPoint && _LeftAnchorPoint && _TopAnchorPoint) {
                        // TODO
                    }
                }

                if(_MultiPieceTotal == 4) {
                    if(_ConnectingPiece.PieceIsConnected()) {
                        _IsConnected = true;
                    } else {
                        _IsConnected = false;
                    }
                } 
            } else {
                _IsConnected = false;
            }
        }

        if(_StraightPiece && !_IsRootPiece && _ConnectingPiece.PieceIsConnected()) {
            if(_CurrentZRotation == _ConnectedRotation) {
                _IsConnected = true;
            } else {
                _IsConnected = false;
            }
        }
    }

    private void SetPieceColor() {
        if(_IsConnected) {
            _ImageCentralNode.color = Color.green;
            switch(_MultiPieceTotal) {
                case 1:
                    _ImagePiece_1.color = Color.green;
                    break;
                case 2:
                    _ImagePiece_1.color = Color.green;
                    _ImagePiece_2.color = Color.green;
                    break;
                case 3:
                    _ImagePiece_1.color = Color.green;
                    _ImagePiece_2.color = Color.green;
                    _ImagePiece_3.color = Color.green;
                    break;
                case 4:
                    _ImagePiece_1.color = Color.green;
                    _ImagePiece_2.color = Color.green;
                    _ImagePiece_3.color = Color.green;
                    _ImagePiece_4.color = Color.green;
                    break;
            }
        } else {
            _ImageCentralNode.color = Color.red;
            switch(_MultiPieceTotal) {
                case 1:
                    _ImagePiece_1.color = Color.red;
                    break;
                case 2:
                    _ImagePiece_1.color = Color.red;
                    _ImagePiece_2.color = Color.red;
                    break;
                case 3:
                    _ImagePiece_1.color = Color.red;
                    _ImagePiece_2.color = Color.red;
                    _ImagePiece_3.color = Color.red;
                    break;
                case 4:
                    _ImagePiece_1.color = Color.red;
                    _ImagePiece_2.color = Color.red;
                    _ImagePiece_3.color = Color.red;
                    _ImagePiece_4.color = Color.red;
                    break;
            }
        }
    }


    public Vector3 GetRightPos() => _RightAnglePoint;
    public Vector3 GetLeftPos() => _LeftAnglePoint;
    public Vector3 GetTopPos() => _TopAnglePoint;
    public Vector3 GetBottomPos() => _BottomAnglePoint;

    public bool PieceIsConnected() => _IsConnected;

}
