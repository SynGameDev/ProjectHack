using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleInTheWallApp : MonoBehaviour {
    public static HoleInTheWallApp Instance;

    private AceXTerminalController _Terminal;

    public bool Installed;

    private void Awake() {
        CreateInstance();
        FindTerminal();
    }

    private void Start() {
        StartCoroutine(StartApp());
    }

    private void Update() {
        CheckTerminalStillOpen();
    }

    private IEnumerator StartApp() {
        _Terminal.DisplayInput("Starting Hole In The Wall Setup...");
        yield return new WaitForSeconds(1f);
        if(GameController.Instance.GetActiveContract().Terminal.BackDoorInstalled) {
            _Terminal.DisplayInput("Backdoor has already been installed on this terminal");
        } else {
            _Terminal.DisplayInput("Installing Back Door...");

            switch(GameController.Instance.GetActiveContract().Terminal.AntiVirusLevel) {
                case 0:
                    GameController.Instance.GetActiveContract.Terminal.BackDoorInstalled = true;
                    _Terminal.DisplayInput("Back Door Install Completed");
                    break;
                // TODO: Open Puzzle Level
            }
        }

        Installed = true;
    }

    private IEnumerator RemoveApp() {
        _Terminal.DisplayInput("Removing Application");
        yield return new WaitForSeconds(1f);
        if(Installed) {
            GameController.Instance.GetActiveContract().BackDoorInstalled = false;
            Installed = false;
        }
    }

    

    private void CreateInstance() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void FindTerminal() => _Terminal = GameObject.FindGameObjectWithTag("AceXTerminal");

    private void CheckTerminalStillOpen() {
        if(_Terminal == null) {
            Destroy(this.gameObject);
        }
    }
}