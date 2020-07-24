using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.Remoting.Messaging;

public class AceMailController : MonoBehaviour
{
    public static AceMailController Instance;

    private string _EmailView;
    private List<GameObject> _EmailsInScene = new List<GameObject>();
    [SerializeField] private TMP_Dropdown _EmailViewDropdown;
    [SerializeField] private GameObject _MailField;
    [SerializeField] private Transform _MailListContainer;
    private Email _ViewingEmail;

    [Header("Pages")]
    [SerializeField] private GameObject _LoginScreen;
    [SerializeField] private GameObject _MainScreen;

    [Header("Email Content")]
    [SerializeField] private TextMeshProUGUI _Subject;
    [SerializeField] private TextMeshProUGUI _From;
    [SerializeField] private TextMeshProUGUI _To;
    [SerializeField] private TextMeshProUGUI _EmailContent;

    [Header("Login Screen")]
    [SerializeField] private TMP_InputField _UsernameInput;
    [SerializeField] private TMP_InputField _PasswordInput;
    [SerializeField] private TextMeshProUGUI _ErrorMessage;

    private void Awake()
    {

        // Create an Instance
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }

        // CheckLogin();
    }

    private void Start()
    {
       // CheckLogin();

        //Debug.Log(GameController.Instance.GetActiveTerminal().EmailAccount.AccountInfo.FirstName);
    }


    public void OnLoginClick()
    {
        var user = _UsernameInput.text;
        var pass = _PasswordInput.text;

        foreach(var item in EmailController.Instance.EmailAccounts)
        {
            if(item.AccountInfo.Username + item.AccountInfo.Server == user && item.AccountInfo.Password == pass)
            {
                // GameController.Instance.GetActiveTerminal().EmailAccount = item; BUG: Make this locate a EmailDatabase
               // CheckLogin();
                break;
            } else
            {
                _ErrorMessage.gameObject.SetActive(true);
                _ErrorMessage.text = "Incorrect Username And/Or Password";
            }
        }
    }

    

    private void Update()
    {
        if(GameController.Instance.GetActiveTerminal().EmailAccount != null)
        {
            if (_ViewingEmail != null)
            {
                _Subject.text = _ViewingEmail.Subject;
                _From.text = _ViewingEmail.FromUser;
                _To.text = _ViewingEmail.ToUser;
                _EmailContent.text = _ViewingEmail.Content;
            }
            else
            {
                _Subject.text = "";
                _From.text = "";
                _To.text = "";
                _EmailContent.text = "";
            }
        }

        if(GameController.Instance.GetActiveTerminal().EmailAccount == null)
        {
            _LoginScreen.SetActive(true);
            _MainScreen.SetActive(false);
        } else
        {
            _LoginScreen.SetActive(false);
            _MainScreen.SetActive(true);
        }
    }

    private void ChangeInputField()
    {
        if(GameController.Instance.GetActiveTerminal().EmailAccount == null)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _PasswordInput.ActivateInputField();
            }

            if(Input.GetKeyDown(KeyCode.Tab) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                _UsernameInput.ActivateInputField();
            }
        }
    }

    /*
    public void OnEmailChange()
    {
        ClearEmails();
        _EmailView = _EmailViewDropdown.options[_EmailViewDropdown.value].text;
        // Validate Which Dropdown item is selected, and generate the emails based on the value of the email dropdown
        switch(_EmailView)
        {
            case "Received Emails":
                GetReceivedEmails();
                break;
            case "Sent Emails":
                GetSentEmails();
                break;
            case "Draft Emails":
                GetDraftEmails();
                break;
            default:
                GetReceivedEmails();
                break;
        }
    }
    
    */
    private void ClearEmails()
    {
        foreach(var email in _EmailsInScene)
        {
            Destroy(email.gameObject);
        }

        _EmailsInScene.Clear();
    }

    /*
    private void GetReceivedEmails()
    {
        var Terminal = GameController.Instance.GetActiveTerminal();
        foreach (var email in Terminal.EmailAccount.ReceivedEmailed)
        {
            var go = Instantiate(_MailField);
            go.GetComponent<EmailButton>().SetEmailData(email);
            go.transform.localScale = Vector3.one;
            go.transform.SetParent(_MailListContainer);

            _EmailsInScene.Add(go);
        }
    }
    

    private void GetSentEmails()
    {
        var Terminal = GameController.Instance.GetActiveTerminal();
        foreach (var email in Terminal.EmailAccount.SentEmails)
        {
            var go = Instantiate(_MailField);
            go.GetComponent<EmailButton>().SetEmailData(email);
            go.transform.localScale = Vector3.one;
            go.transform.SetParent(_MailListContainer);
             
            _EmailsInScene.Add(go);
        }
    }

    private void GetDraftEmails()
    {
        var Terminal = GameController.Instance.GetActiveTerminal();
        foreach (var email in Terminal.EmailAccount.DraftEmails)
        {
            var go = Instantiate(_MailField);
            go.GetComponent<EmailButton>().SetEmailData(email);
            go.transform.localScale = Vector3.one;
            go.transform.SetParent(_MailListContainer);

            _EmailsInScene.Add(go);
        }
    }
    
    */

    public void SetViewingEmail(Email eml) => _ViewingEmail = eml;
    public Email GetViewingEmail() => _ViewingEmail; 
}
