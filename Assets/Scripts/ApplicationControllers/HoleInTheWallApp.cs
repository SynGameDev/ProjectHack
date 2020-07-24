using System.Collections;
using UnityEngine;

public class HoleInTheWallApp : MonoBehaviour
{
    public static HoleInTheWallApp Instance;

    private AceXTerminalController _Terminal;

    public bool Installed;

    private void Awake()
    {
        CreateInstance();
        FindTerminal();
    }

    private void Start()
    {
        StartCoroutine(StartApp());
    }

    private void Update()
    {
        CheckTerminalStillOpen();
    }

    private IEnumerator StartApp()
    {
        _Terminal.DisplayInput("Starting Hole In The Wall Setup...", false);
        yield return new WaitForSeconds(1f);
        if (GameController.Instance.GetActiveTerminal().BackDoorInstalled)
        {
            _Terminal.DisplayInput("Backdoor has already been installed on this terminal", false);
        }
        else
        {
            _Terminal.DisplayInput("Installing Back Door...", false);

            SceneController.Instance.OpenMaze();
        }

        Installed = true;
    }

    public IEnumerator RemoveApp()
    {
        _Terminal.DisplayInput("Removing Application", false);
        yield return new WaitForSeconds(1f);
        if (Installed)
        {
            GameController.Instance.GetActiveTerminal().BackDoorInstalled = false;
            Installed = false;
        }
    }

    public void InstalledApp()
    {
        GameController.Instance.GetActiveTerminal().BackDoorInstalled = true;
        _Terminal.DisplayInput("Backdoor Install Completed", false);
    }

    public IEnumerator ErrorInstalling()
    {
        _Terminal.DisplayInput("Error Installing Backdoor....", false);
        yield return new WaitForSeconds(2);
        Debug.Log("Disconnect");
        GameController.Instance.GetActiveTerminal().BlockedIPs.Add(GameController.Instance.GetPlayerData().PlayerIP);
        SceneController.Instance.CloseUserDesktop();
    }

    private void CreateInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void FindTerminal() => _Terminal = GameObject.FindGameObjectWithTag("AceXTerminal").GetComponent<AceXTerminalController>();

    private void CheckTerminalStillOpen()
    {
        if (_Terminal == null)
        {
            Destroy(this.gameObject);
        }
    }
}