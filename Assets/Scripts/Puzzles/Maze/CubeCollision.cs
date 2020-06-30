using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeCollision : MonoBehaviour
{

    [SerializeField] private GameObject StatusPanel;
    public float WaitTime;

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.name == "EndPos") {
            HoleInTheWallApp.Instance.InstalledApp();
            StartCoroutine(MazeStatus("Completed"));
        }

        if(col.CompareTag("Wall")) {
            StartCoroutine(HoleInTheWallApp.Instance.ErrorInstalling());
            StartCoroutine(MazeStatus("Failed"));
        }
    }

    private IEnumerator MazeStatus(string status) {
        StatusPanel.SetActive(true);
        if(status == "Completed") {
            
            StatusPanel.GetComponentInChildren<TextMeshProUGUI>().text = "MAZE COMPLETED";
            StatusPanel.GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
        } else {
            StatusPanel.GetComponentInChildren<TextMeshProUGUI>().text = "MAZE FAILED";
            StatusPanel.GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
        }

        yield return new WaitForSeconds(WaitTime);
        SceneController.Instance.CloseMaze();
    }
}
