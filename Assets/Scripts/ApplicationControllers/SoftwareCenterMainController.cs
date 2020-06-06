using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftwareCenterMainController : MonoBehaviour
{
    [Header("Software List")]
    [SerializeField] private List<ScriptableObject> ApplicationObjects = new List<ScriptableObject>();
    [SerializeField] private List<ScriptableObject> InstalledApplications = new List<ScriptableObject>();
    [SerializeField] private GameObject _AppContainerPrefab;

    [Header("Row Counts")]
    [SerializeField] private int Row_1_Count;
    [SerializeField] private int Row_2_Count;
    [SerializeField] private int Row_3_Count;
    [SerializeField] private int Row_4_Count;
    [SerializeField] private int Row_5_Count;
    [SerializeField] private int MaxItemsInRow;

    [Header("Item Containers")]
    [SerializeField] private Transform _Row_1;
    [SerializeField] private Transform _Row_2;
    [SerializeField] private Transform _Row_3;
    [SerializeField] private Transform _Row_4;
    [SerializeField] private Transform _Row_5;

    [Header("Application Status")]
    [SerializeField] private bool _ShowInstalledApps;

    private void Start() {
        FindInstalledApplications();
        GetAllApps();
        ShowAvailableApps();
    }
    

    private void Update() {

    }


    public void SwitchDisplay() {
        if(_ShowInstalledApps) {
            _ShowInstalledApps = false;
            ShowAvailableApps();
        } else {
            _ShowInstalledApps = true;
            ShowInstalledApps();
        }
    }

    private void ShowAvailableApps() {
        foreach(var app in ApplicationObjects) {
            if(!InstalledApplications.Contains(app)) {

                var Application = app as ApplicationScriptableObject;
                if(Row_1_Count < MaxItemsInRow) {
                    CreateAppItem(Application, _Row_1);
                    Row_1_Count += 1;
                } else if(Row_2_Count < MaxItemsInRow) {
                    CreateAppItem(Application, _Row_2);
                    Row_2_Count += 1;
                } else if(Row_3_Count < MaxItemsInRow) {
                    CreateAppItem(Application, _Row_3);
                    Row_3_Count += 1;
                } else if(Row_4_Count < MaxItemsInRow) {
                    CreateAppItem(Application, _Row_4);
                    Row_4_Count += 1;
                } else if(Row_5_Count < MaxItemsInRow) {
                    CreateAppItem(Application, _Row_5);
                    Row_5_Count += 1;
                }
            }
        }
    }

    private void ShowInstalledApps() {
        foreach(var app in InstalledApplications) {
            var Application = app as ApplicationScriptableObject;

            if(Row_1_Count < MaxItemsInRow) {
                CreateAppItem(Application, _Row_1);
            } else if(Row_2_Count < MaxItemsInRow) {
                CreateAppItem(Application, _Row_2);
            } else if(Row_3_Count < MaxItemsInRow) {
                CreateAppItem(Application, _Row_3);
            } else if(Row_4_Count < MaxItemsInRow) {
                CreateAppItem(Application, _Row_4);
            } else if(Row_5_Count < MaxItemsInRow) {
                CreateAppItem(Application, _Row_5);
            }
        }
    }

    private void CreateAppItem(ScriptableObject app, Transform row) {
        var go = Instantiate(_AppContainerPrefab);
        var App = app as ApplicationScriptableObject;
        go.name = App.ApplicationName;

        go.transform.SetParent(row);
        go.transform.localScale = new Vector3(2, 2, 2);

        go.GetComponent<AppCenterApp>().AppData = App;
        go.GetComponent<AppCenterApp>().AppName.text = App.ApplicationName;
        go.GetComponent<AppCenterApp>().AppIcon.sprite = App.ApplicationIcon;

    }

    private void FindInstalledApplications() {
        var CurrentContract = GameController.Instance.GetActiveContract();

        foreach(var App in CurrentContract.InstalledApplication) {
            InstalledApplications.Add(App);
        }
    }

    private void GetAllApps() {
        foreach(var app in DatabaseController.Instance.GetSoftwareApps()    ) {
            ApplicationObjects.Add(app);
        }
    }
}
