using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayUserDesktop : MonoBehaviour
{
    

    [Header("Item Rows")]
    [SerializeField] private Transform _Row_1;
    [SerializeField] private Transform _Row_2;
    [SerializeField] private Transform _Row_3;
    [SerializeField] private Transform _Row_4;

    [Header("Row Stats")]
    [SerializeField] private int MaxItemsInRow;
    private int _RowCount_1;
    private int _RowCount_2;
    private int _RowCount_3;
    private int _RowCount_4;

    private List<GameObject> _AppsOnDesktop = new List<GameObject>();               // List of apps display on the desktop since last update

    [SerializeField] private GameObject _DesktopAppPrefab;

    private void Start() {
        DisplayApps();
    }   

    private void DisplayApps() {
        ContractInfo contract = GameController.Instance.GetActiveContract();            // Get the current contract

        // Loop through each application and assign it to a row on the desktop
        foreach(var app in contract.InstalledApplication) {
            if(_RowCount_1 < MaxItemsInRow) {
                CreateApp(ApplicationDatabase.Instance.GetApplication(app), _Row_1);
                _RowCount_1 += 1;
            } else if(_RowCount_2 < MaxItemsInRow) {
                CreateApp(ApplicationDatabase.Instance.GetApplication(app), _Row_2);
                _RowCount_2 += 1;
            } else if(_RowCount_3 < MaxItemsInRow) {
                CreateApp(ApplicationDatabase.Instance.GetApplication(app), _Row_3);
                _RowCount_3 += 1;
            } else {
                CreateApp(ApplicationDatabase.Instance.GetApplication(app), _Row_4);
                _RowCount_4 += 1;
            }
        }
    }


    public void UpdateDesktop() {
        // Remove all apps from the desktop
        foreach(var app in _AppsOnDesktop) {
            Destroy(app);
        }

        _AppsOnDesktop.Clear();

        DisplayApps();              // Add the desktop items
    }

    private void CreateApp(ScriptableObject app, Transform row) {

        var App = app as ApplicationScriptableObject;               // Get the Object
        var go = Instantiate(_DesktopAppPrefab);                // Create the item

        go.name = App.AppData.ApplicationName;                  // Set the name of the object
        go.transform.SetParent(row);                    // Set the position of the object
        go.transform.localScale = new Vector3(2, 2, 2);         // Init the scale of the object

        go.AddComponent<OpenApplications>();                    // Add script to open object
        go.GetComponent<OpenApplications>().SetAppToOpen("Software Center");                // Assign the app that needs to open it

        var AppCenter = go.GetComponent<AppCenterApp>();                    // Get the App Cetner Item
        AppCenter.AppData = App;                            // Set the scriptable object
        AppCenter.AppName.text = App.AppData.ApplicationName;           // Set the name
        AppCenter.AppIcon.sprite = App.AppData.ApplicationIcon;         // Set the icon

        _AppsOnDesktop.Add(go);                 // Add to desktop list

    }
}
