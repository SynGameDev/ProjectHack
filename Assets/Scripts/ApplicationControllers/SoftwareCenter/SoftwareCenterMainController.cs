using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoftwareCenterMainController : MonoBehaviour
{
    [Header("Software List")]
    [SerializeField] private List<ApplicationClass> ApplicationObjects = new List<ApplicationClass>();

    [SerializeField] private List<ApplicationClass> InstalledApplications = new List<ApplicationClass>();
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

    private ApplicationClass _ViewingApplication;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI _ShowText;

    private List<GameObject> _CurrentlyShowingApp = new List<GameObject>();

    [Header("Sub Applications")]
    [SerializeField] private GameObject _ViewingApplicationObject;

    [Header("Action Controller")]
    private string _ApplicationAction;

    private void Awake()
    {
        //GameObject.FindGameObjectWithTag("SoftwareCenterViewingWindow").SetActive(false);
    }

    private void Start()
    {
        FindInstalledApplications();
        GetAllApps();
        ShowAvailableApps();
    }

    private void Update()
    {
    }

    /// <summary>
    /// This method will change whether you're viewing a the installed applications or the applications that can be purchased
    /// </summary>
    public void SwitchDisplay()
    {
        // Loop through each app that is currently being displayed and destroy
        foreach (var app in _CurrentlyShowingApp)
        {
            Destroy(app);
        }

        _CurrentlyShowingApp.Clear();                   // Empty the list of displayed items

        if (_ShowInstalledApps)
        {                        // If the player is viewing installed applications
            _ShowInstalledApps = false;                 // ... Switch it to Not installed Applications
            _ShowText.text = "Show Installed Apps";             // ... Set the text of the toggle button
            ShowAvailableApps();                        // ... Show Apps that can be downloaded
        }
        else
        {                    // Other wise
            _ShowInstalledApps = true;                  // ... Show installed items
            _ShowText.text = "Show Available Apps";         // ... toggle the text
            ShowInstalledApps();                        // ... Display the installed items
        }
    }

    /// <summary>
    /// This method will loop through each app the user doesn't have installed and display it on the desktop
    /// </summary>
    private void ShowAvailableApps()
    {
        foreach (var app in ApplicationObjects)
        {
            if (!InstalledApplications.Contains(app))
            {
                var Application = app;
                if (!Application.CrackedApplication)
                {
                    if (Row_1_Count < MaxItemsInRow)
                    {
                        CreateAppItem(Application, _Row_1);
                        Row_1_Count += 1;
                    }
                    else if (Row_2_Count < MaxItemsInRow)
                    {
                        CreateAppItem(Application, _Row_2);
                        Row_2_Count += 1;
                    }
                    else if (Row_3_Count < MaxItemsInRow)
                    {
                        CreateAppItem(Application, _Row_3);
                        Row_3_Count += 1;
                    }
                    else if (Row_4_Count < MaxItemsInRow)
                    {
                        CreateAppItem(Application, _Row_4);
                        Row_4_Count += 1;
                    }
                    else if (Row_5_Count < MaxItemsInRow)
                    {
                        CreateAppItem(Application, _Row_5);
                        Row_5_Count += 1;
                    }
                }
            }
        }
    }

    /// <summary>
    /// This method will loop through each app the user has installed on the desktop
    /// </summary>
    private void ShowInstalledApps()
    {
        foreach (var app in InstalledApplications)
        {
            var Application = app;

            if (Row_1_Count < MaxItemsInRow)
            {
                CreateAppItem(Application, _Row_1);
                Row_1_Count += 1;
            }
            else if (Row_2_Count < MaxItemsInRow)
            {
                CreateAppItem(Application, _Row_2);
                Row_2_Count += 1;
            }
            else if (Row_3_Count < MaxItemsInRow)
            {
                CreateAppItem(Application, _Row_3);
                Row_3_Count += 1;
            }
            else if (Row_4_Count < MaxItemsInRow)
            {
                CreateAppItem(Application, _Row_4);
                Row_4_Count += 1;
            }
            else if (Row_5_Count < MaxItemsInRow)
            {
                CreateAppItem(Application, _Row_5);
                Row_5_Count += 1;
            }
        }
    }

    /// <summary>
    /// This method will create the application in software center
    /// </summary>
    /// <param name="app">Application to show</param>
    /// <param name="row">Where to display the item</param>
    private void CreateAppItem(ApplicationClass app, Transform row)
    {
        var go = Instantiate(_AppContainerPrefab);              // Create the object
        var App = app;
        go.name = App.ApplicationName;                  // Set the name of the object

        go.transform.SetParent(row);                            // Set the row to place the object
        go.transform.localScale = new Vector3(2, 2, 2);         // Set the scale of the object

        // Add & Get the component
        go.AddComponent<SoftwareCenterAppControl>();
        go.GetComponent<SoftwareCenterAppControl>()._App = app;

        // Setup the app center script
        go.GetComponent<AppCenterApp>().AppData = App;
        go.GetComponent<AppCenterApp>().AppName.text = App.ApplicationName;
        go.GetComponent<AppCenterApp>().AppIcon.sprite = App.ApplicationIcon;

        _CurrentlyShowingApp.Add(go);                   // Add the application to the currently showing list
    }

    private void FindInstalledApplications()
    {
        var CurrentTerminal = GameController.Instance.GetActiveTerminal();


        // Loop through each Installed item to find if the application is installed.
        foreach (var App in CurrentTerminal.InstalledApplication)
        {
            InstalledApplications.Add(App);
        }
    }

    private void GetAllApps()
    {
        foreach (var app in ApplicationDatabase.Instance.GetSoftwareApps())
        {
            ApplicationObjects.Add(app);
        }
    }

    // Setters
    public void SetViewingApp(ApplicationClass _App)
    {
        _ViewingApplication = _App;
        _ViewingApplicationObject.SetActive(true);              // MAKE SURE OBJECT IS BEING VIEWED
        _ViewingApplicationObject.GetComponentInChildren<ViewingSoftwareApp>().SetViewingApp(_App);
        _ViewingApplicationObject.GetComponentInChildren<ViewingSoftwareApp>().SetupDisplay();
    }

    public void SetActionText(string action) => _ApplicationAction = action;

    // Getters
    public ApplicationClass GetViewingApp() => _ViewingApplication;

    public GameObject GetViewingAppWindow() => _ViewingApplicationObject;

    public string GetApplicationAction() => _ApplicationAction;

    public bool CheckIfInstalled(ApplicationClass App)
    {
        if (InstalledApplications.Contains(App))
            return true;

        return false;
    }
}