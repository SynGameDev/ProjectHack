using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    private ScriptableObject _ViewingApplication;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI _ShowText;
    private List<GameObject> _CurrentlyShowingApp = new List<GameObject>();

    [Header("Sub Applications")]
    [SerializeField] private GameObject _ViewingApplicationObject;

    [Header("Action Controller")]
    private string _ApplicationAction;

    private void Awake() {
        //GameObject.FindGameObjectWithTag("SoftwareCenterViewingWindow").SetActive(false);
    }

    private void Start() {
        FindInstalledApplications();
        GetAllApps();
        ShowAvailableApps();
    }
    

    private void Update() {

    }


    public void SwitchDisplay() {
        foreach(var app in _CurrentlyShowingApp)  {
            Destroy(app);
        }

        _CurrentlyShowingApp.Clear();
        if(_ShowInstalledApps) {
            _ShowInstalledApps = false;
            _ShowText.text = "Show Installed Apps";
            ShowAvailableApps();
        } else {
            _ShowInstalledApps = true;
            _ShowText.text = "Show Available Apps";
            ShowInstalledApps();
        }
    }

    private void ShowAvailableApps() {
        foreach(var app in ApplicationObjects) {
            if(!InstalledApplications.Contains(app)) {

                var Application = app as ApplicationScriptableObject;
                if(!Application.CrackedApplication) {
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
    }

    private void ShowInstalledApps() {
        foreach(var app in InstalledApplications) {
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

    private void CreateAppItem(ScriptableObject app, Transform row) {
        var go = Instantiate(_AppContainerPrefab);
        var App = app as ApplicationScriptableObject;
        go.name = App.ApplicationName;

        go.transform.SetParent(row);
        go.transform.localScale = new Vector3(2, 2, 2);

        go.AddComponent<SoftwareCenterAppControl>();
        go.GetComponent<SoftwareCenterAppControl>()._App = app;

        go.GetComponent<AppCenterApp>().AppData = App;
        go.GetComponent<AppCenterApp>().AppName.text = App.ApplicationName;
        go.GetComponent<AppCenterApp>().AppIcon.sprite = App.ApplicationIcon;
        _CurrentlyShowingApp.Add(go);

    }

    private void FindInstalledApplications() {
        var CurrentContract = GameController.Instance.GetActiveContract();

        foreach(var App in CurrentContract.InstalledApplication) {
            InstalledApplications.Add(App);
        }
    }

    private void GetAllApps() {
        foreach(var app in ApplicationDatabase.Instance.GetSoftwareApps()    ) {
            ApplicationObjects.Add(app);
        }
    }

    // Setters
    public void SetViewingApp(ScriptableObject _App) => _ViewingApplication = _App;
    public void SetActionText(string action) => _ApplicationAction = action;

    // Getters
    public ScriptableObject GetViewingApp() => _ViewingApplication;
    public GameObject GetViewingAppWindow() => _ViewingApplicationObject;
    public string GetApplicationAction() => _ApplicationAction;

    public bool CheckIfInstalled(ScriptableObject App) {
        if(InstalledApplications.Contains(App)) 
            return true;

        return false;
    }
}
