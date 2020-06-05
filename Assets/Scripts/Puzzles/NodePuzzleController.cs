using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodePuzzleController : MonoBehaviour {
    

    [SerializeField] private GameObject PuzzleStatusText;

    [Header("Timer Settings")]
    [SerializeField] private TextMeshProUGUI _TimerText;
    [SerializeField] private float _SecondsToCompleted;
    private float _CurrentTimer;

    [SerializeField] private PuzzlePieceController _EndNode;



    private void Awake() {
        PuzzleStatusText.SetActive(false);
    }

    private void Update() {
        CheckIfCompleted();
        TimerControl();
    }

    private void TimerControl() {
        _SecondsToCompleted -= 1 * Time.deltaTime;
        if(_SecondsToCompleted <= 0) {
            PuzzleCompleted("Failed");
        }

        _TimerText.text = _SecondsToCompleted.ToString("F0");
    }

    private void CheckIfCompleted() {
        if(_EndNode.PieceIsConnected()) {
            StartCoroutine(PuzzleCompleted("Completed"));
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
}