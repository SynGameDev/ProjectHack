using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class EmailController : MonoBehaviour
{
    public static EmailController Instance;

    public List<EmailDatabaseClass> EmailAccounts = new List<EmailDatabaseClass>();
    public List<AceTechAccount> Accounts = new List<AceTechAccount>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(this.gameObject);
        }

        GetUsers();
    }

    private void Start()
    {
        
    }

    private void GetUsers()
    {
        var json = new JsonSerializer();
        string path = "C:/Dirty Rat Toolkit/Email Database/Email.drdb";
        EmailDB acc = new EmailDB();
        List<Email> Emaildata = new List<Email>();
        using(StreamReader file = File.OpenText(path))
        {
            acc = (EmailDB)json.Deserialize(file, typeof(EmailDB));
            Emaildata = acc.Emails;
        }

        foreach(var user in acc.UserDB)
        {
            EmailAccounts.Add(CreateUserInfo(user, Emaildata));
        }
    }

    private EmailDatabaseClass CreateUserInfo(EmailAccount user, List<Email> EmailData)
    {
        EmailDatabaseClass NewUser = new EmailDatabaseClass();
        NewUser.AccountInfo = user;
        string UserEmail = NewUser.AccountInfo.Username + NewUser.AccountInfo.Server;
        
        foreach(var email in EmailData)
        {
            
            if(email.ToUser == UserEmail)
            {
                
                NewUser.ReceivedEmailed.Add(email);
            } else if(email.FromUser == UserEmail && email.ToUser != null)
            {
                NewUser.SentEmails.Add(email);
            } else if(email.FromUser == UserEmail && email.ToUser == null)
            {
                NewUser.DraftEmails.Add(email);
            }
        }

        return NewUser;
    }

}

public class EmailDB
{
    public List<EmailAccount> UserDB = new List<EmailAccount>();
    public List<Email> Emails = new List<Email>();
}
