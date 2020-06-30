using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeController : MonoBehaviour
{
    [Header("Mazes")]
    [SerializeField] private List<Image> EasyMaze = new List<Image>();
    [SerializeField] private List<Image> MedMaze = new List<Image>();
    [SerializeField] private List<Image> HardMaze = new List<Image>();

    private bool _Started;
    [SerializeField] private GameObject _Cube;
    [SerializeField] private GameObject _StartBtn;


    private Image Puzzle;

    private void Update() {
        if(_Started) {
            _Cube.transform.position = Input.mousePosition;
        }
    }

    private void SelectMaze() {
        var anti = GameController.Instance.GetActiveTerminal().AntiVirusLevel;

        switch(anti) {
            case 0:
                PuzzleCompleted();
                break;
            case 1:
                Puzzle = EasyMaze[Random.Range(0, EasyMaze.Count)];
                break;
            case 2:
                Puzzle = MedMaze[Random.Range(0, MedMaze.Count)];
                break;
            case 3:
                Puzzle = HardMaze[Random.Range(0, HardMaze.Count)];
                break;
        }

        _Started = false;
    }

    public void StartMaze() {
        _Started = true;
        _StartBtn.SetActive(false);
    }



    private void PuzzleCompleted() {

    }
}
