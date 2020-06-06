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

    private List<GameObject> _AppsOnDesktop = new List<GameObject>();

    [SerializeField] private GameObject _DesktopAppPrefab;

    private void Start() {
        DisplayApps();
    }   

    private void DisplayApps() {
        ContractInfo contract = GameController.Instance.GetActiveContract();

        foreach(var app in contract.InstalledApplication) {
            if(_RowCount_1 < MaxItemsInRow) {
                CreateApp(app, _Row_1);
                _RowCount_1 += 1;
            } else if(_RowCount_2 < MaxItemsInRow) {
                CreateApp(app, _Row_2);
                _RowCount_2 += 1;
            } else if(_RowCount_3 < MaxItemsInRow) {
                CreateApp(app, _Row_3);
                _RowCount_3 += 1;
            } else {
                CreateApp(app, _Row_4);
                _RowCount_4 += 1;
            }
        }
    }


    public void UpdateDesktop() {
        foreach(var app in _AppsOnDesktop) {
            Destroy(app);
        }

        _AppsOnDesktop.Clear();

        DisplayApps();
    }

    private void CreateApp(ScriptableObject app, Transform row) {

        var App = app as ApplicationScriptableObject;
        var go = Instantiate(_DesktopAppPrefab);

        go.name = App.ApplicationName;
        go.transform.SetParent(row);
        go.transform.localScale = new Vector3(2, 2, 2);

        go.AddComponent<OpenApplications>();
        go.GetComponent<OpenApplications>().SetAppToOpen("Software Center");

        var AppCenter = go.GetComponent<AppCenterApp>();
        AppCenter.AppData = App;
        AppCenter.AppName.text = App.ApplicationName;
        AppCenter.AppIcon.sprite = App.ApplicationIcon;

        _AppsOnDesktop.Add(go);

    }
}
